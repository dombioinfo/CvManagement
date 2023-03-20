using System.Globalization;

namespace BlazorBaseModel.Model
{
    public class CsvLineFullData
    {
        public CsvLineFullData() { }
        public PersonneDto PersonneDto { get; set; } = default!;
        public AdresseDto AdresseDto { get; set; } = default!;
        public CandidatureDto CandidatureDto { get; set; } = default!;

        public static CsvLineFullData MapFrom(string[] lineOfCsv)
        {
            CultureInfo provider = new CultureInfo("fr-FR");
            CsvLineFullData lineData = new CsvLineFullData()
            {
                PersonneDto = new PersonneDto(),
                AdresseDto = new AdresseDto(),
                CandidatureDto = new CandidatureDto()
            };
            lineData.PersonneDto.Nom = lineOfCsv[0];
            lineData.PersonneDto.Prenom = lineOfCsv[1];
            lineData.PersonneDto.Email = lineOfCsv[2];
            lineData.PersonneDto.Tel = lineOfCsv[3];
            DateTime date = DateTime.MinValue;
            if (DateTime.TryParse(lineOfCsv[4], out date))
            {
                lineData.PersonneDto.DateNaissance = date;
            }
            // lineData.AdresseDto.Ville = lineOfCsv[5];
            // if (long.TryParse(lineOfCsv[6], out) {
            //     lineData.AdresseDto.CodePostal = 
            // }
            
            return lineData;
        }
    }
}