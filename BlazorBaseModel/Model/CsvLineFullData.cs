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

            // PersonneDto
            lineData.PersonneDto.Nom = lineOfCsv[0];
            lineData.PersonneDto.Prenom = lineOfCsv[1];
            lineData.PersonneDto.Email = lineOfCsv[2];
            lineData.PersonneDto.Tel = lineOfCsv[3];
            DateTime date = DateTime.MinValue;
            if (DateTime.TryParse(lineOfCsv[4], out date))
            {
                lineData.PersonneDto.DateNaissance = date;
            }

            // AdresseDto
            lineData.AdresseDto.Ville = lineOfCsv[5];
            lineData.AdresseDto.CodePostal = lineOfCsv[6];

            // CandidatureDto

            return lineData;
        }
    }
}