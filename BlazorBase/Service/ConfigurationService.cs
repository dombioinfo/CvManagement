namespace BlazorBase.Service
{
    public class ConfigurationService : GenericObjectService<object>
    {
        private readonly PersonneService _personneService;
        private readonly AdresseService _adresseService;
        private readonly CandidatureService _candidatureService;
        private readonly ListeItemService _listeItemService;

        public ConfigurationService(
            IHttpClientFactory clientFactory, IMapper mapper,
            PersonneService personneService,
            AdresseService adresseService,
            CandidatureService candidatureService,
            ListeItemService listeItemService
            ) : base(clientFactory, mapper)
        {
            _personneService = personneService;
            _adresseService = adresseService;
            _candidatureService = candidatureService;
            _listeItemService = listeItemService;
        }

        public async Task ImportFullDataAsync(FileDto uploadedFileWithData)
        {
            string fullFileName = "";
            try
            {
                fullFileName = Path.Combine(uploadedFileWithData.RelativePath, uploadedFileWithData.FileName);
                using (StreamReader reader = new StreamReader(fullFileName))
                {
                    int countLine = 0;
                    while (!reader.EndOfStream)
                    {
                        string? line = reader.ReadLine();
                        if (countLine++ == 0) //header
                        {
                            if (line != null && line.Contains("Nom candidat;Prénom candidat;Email candidat;Téléphone candidat;Date de naissance candidat;Ville candidat;Département candidat;Nom structure employeur;Type employeur;Métiers;Source de la candidature;Nom organisation prescripteur;Nom utilisateur prescripteur;Date de la candidature;Statut de la candidature;Dates de début d’embauche;Dates de fin d’embauche;Motifs de refus;Éligibilité IAE validée;Numéro PASS IAE;Début PASS IAE;Fin PASS IAE"))
                            {
                                continue;
                            }
                            else
                            {
                                throw new Exception("L'entête du fichier ne correspond pas au format attendu");
                            }
                        }
                        string[]? values = line?.Split(';');
                        values = values?.Select(x => x.Trim('"')).ToArray();

                        CsvLineFullData data = CsvLineFullData.MapFrom(values ?? new string[0]);
                        PersonneDto personneDtoFromCsv = data.PersonneDto;
                        CandidatureDto candidatureDtoFromCsv = data.CandidatureDto;
                        AdresseDto adresseDtoFromCsv = data.AdresseDto;

                        PersonneDto critereForSearch = new PersonneDto()
                        {
                            Email = personneDtoFromCsv.Email
                        };
                        
                        #region Personne
                        PersonneDto? personneDto = await _personneService.GetPersonneAsync(critereForSearch);
                        if (personneDto == null) 
                        {
                            personneDto = new PersonneDto();
                            personneDto.Id = await _personneService.CreatePersonneAsync(personneDtoFromCsv);
                        }
                        else
                        {
                            await _personneService.UpdatePersonneAsync(personneDto.Id, personneDtoFromCsv);
                        }
                        #endregion

                        #region Adresse
                        List<AdresseDto> adresseDtoList = await _adresseService.GetAdressesByPersonneAsync(personneDto.Id);
                        AdresseDto? adresseDto = adresseDtoList.FirstOrDefault(x => x.Rue == adresseDtoFromCsv.Rue && x.CodePostal == adresseDtoFromCsv.CodePostal);
                        adresseDtoFromCsv.PersonneId = personneDto.Id;
                        if (adresseDto == null)
                        {
                            adresseDto = new AdresseDto();
                            adresseDto.Id = await _adresseService.CreateAdresseAsync(adresseDtoFromCsv);
                        }
                        else
                        {
                            await _adresseService.UpdateAdresseAsync(adresseDto.Id, adresseDtoFromCsv);
                        }
                        #endregion

                        #region Candidature
                        List<ListeItemDto> listeItemDtos = await _listeItemService.GetListeItemsByListeTypeAsync("METIER");
                        ListeItemDto? listeItemDto = listeItemDtos.FirstOrDefault(x => x.DefaultLibelle == candidatureDtoFromCsv.Metier?.DefaultLibelle);
                        if (listeItemDto != null)
                        {
                            candidatureDtoFromCsv.MetierId = listeItemDto.Id;
                        }

                        List<CandidatureDto> candidatureDtoList = await _candidatureService.GetCandidaturesByPersonneAsync(personneDto.Id);
                        CandidatureDto? candidatureDto = candidatureDtoList.FirstOrDefault(x => x.DateCandidature == candidatureDtoFromCsv.DateCandidature);
                        candidatureDtoFromCsv.PersonneId = personneDto.Id;
                        if (candidatureDto == null)
                        {
                            candidatureDto = new CandidatureDto();
                            candidatureDto.Id = await _candidatureService.CreateCandidatureAsync(candidatureDtoFromCsv);
                        }
                        else
                        {
                            await _candidatureService.UpdateCandidatureAsync(candidatureDto.Id, candidatureDtoFromCsv);
                        }
                        #endregion
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Nom du fichier: '{fullFileName}'");
                Console.WriteLine(e.Message);
            }
        }
    }
}