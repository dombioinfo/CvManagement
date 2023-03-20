namespace BlazorBase.Service
{
    public class ConfigurationService : GenericObjectService<object>
    {
        public ConfigurationService(
            IHttpClientFactory clientFactory, IMapper mapper
            ) : base(clientFactory, mapper) { }

        public Task ImportFullDataAsync(FileDto uploadedFileWithData)
        {
            try
            {
                string fullFileName = Path.Combine(uploadedFileWithData.RelativePath, uploadedFileWithData.FileName);
                using (StreamReader reader = new StreamReader(fullFileName))
                {
                    int countLine = 0;
                    while (!reader.EndOfStream)
                    {
                        string? line = reader.ReadLine();
                        string[]? values = line?.Split(';');
                        if (countLine == 0) //header
                        {
                            if (line.Contains("Nom candidat;Prénom candidat;Email candidat;Téléphone candidat;Date de naissance candidat;Ville candidat;Département candidat;Nom structure employeur;Type employeur;Métiers;Source de la candidature;Nom organisation prescripteur;Nom utilisateur prescripteur;Date de la candidature;Statut de la candidature;Dates de début d’embauche;Dates de fin d’embauche;Motifs de refus;Éligibilité IAE validée;Numéro PASS IAE;Début PASS IAE;Fin PASS IAE"))
                            {
                                continue;
                            }
                            else
                            {
                                throw new Exception("L'entête du fichier ne correspond pas au format attendu");
                            }
                        }
                        CsvLineFullData data = CsvLineFullData.MapFrom(values ?? new string[0]);
                        PersonneDto personneDto = data.PersonneDto;

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new Task(null);
        }
    }
}