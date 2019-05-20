namespace StreetNameRegistry.Api.Legacy.StreetName.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Be.Vlaanderen.Basisregisters.GrAr.Legacy;
    using Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gemeente;
    using Be.Vlaanderen.Basisregisters.GrAr.Legacy.Straatnaam;
    using Infrastructure.Options;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Swashbuckle.AspNetCore.Filters;
    using ProblemDetails = Be.Vlaanderen.Basisregisters.BasicApiProblem.ProblemDetails;

    [DataContract(Name = "StraatnaamDetail", Namespace = "")]
    public class StreetNameResponse
    {
        /// <summary>
        /// De identificator van de straatnaam.
        /// </summary>
        [DataMember(Name = "Identificator", Order = 1)]
        public Identificator Identificator { get; set; }

        /// <summary>
        /// De gemeente aan dewelke de straatnaam is toegewezen.
        /// </summary>
        [DataMember(Name = "Gemeente", Order = 2)]
        public StraatnaamDetailGemeente Gemeente { get; set; }

        /// <summary>
        /// De straatnaam in verschillende talen.
        /// </summary>
        [DataMember(Name = "Straatnamen", Order = 3)]
        public List<GeografischeNaam> Straatnamen { get; set; }

        /// <summary>
        /// De homoniem-toevoegingen aan de straatnaam in verschillende talen.
        /// </summary>
        [DataMember(Name = "HomoniemToevoegingen", Order = 4)]
        public List<GeografischeNaam> HomoniemToevoegingen { get; set; }

        /// <summary>
        /// De huidige fase in de levensloop van een straatnaam.
        /// </summary>
        [DataMember(Name = "StraatnaamStatus", Order = 5)]
        public StraatnaamStatus StraatnaamStatus { get; set; }

        public StreetNameResponse(
            string naamruimte,
            int osloId,
            StraatnaamStatus status,
            StraatnaamDetailGemeente gemeente,
            DateTimeOffset version,
            string nameDutch = null,
            string nameFrench = null,
            string nameGerman = null,
            string nameEnglish = null,
            string homonymAdditionDutch = null,
            string homonymAdditionFrench = null,
            string homonymAdditionGerman = null,
            string homonymAdditionEnglish = null)
        {
            Identificator = new Identificator(naamruimte, osloId.ToString(), version);
            StraatnaamStatus = status;
            Gemeente = gemeente;

            var straatNamen = new List<GeografischeNaam>
            {
                new GeografischeNaam(nameDutch, Taal.NL),
                new GeografischeNaam(nameFrench, Taal.FR),
                new GeografischeNaam(nameGerman, Taal.DE),
                new GeografischeNaam(nameEnglish, Taal.EN)

            };

            Straatnamen = straatNamen.Where(x => !string.IsNullOrEmpty(x.Spelling)).ToList();

            var homoniemen = new List<GeografischeNaam>
            {
                new GeografischeNaam(homonymAdditionDutch, Taal.NL),
                new GeografischeNaam(homonymAdditionFrench, Taal.FR),
                new GeografischeNaam(homonymAdditionGerman, Taal.DE),
                new GeografischeNaam(homonymAdditionEnglish, Taal.EN)

            };

            HomoniemToevoegingen = homoniemen.Where(x => !string.IsNullOrEmpty(x.Spelling)).ToList();
        }
    }

    public class StreetNameResponseExamples : IExamplesProvider
    {
        private readonly ResponseOptions _responseOptions;

        public StreetNameResponseExamples(IOptions<ResponseOptions> responseOptionsProvider)
         => _responseOptions = responseOptionsProvider.Value;

        public object GetExamples()
        {
            var gemeente = new StraatnaamDetailGemeente
            {
                ObjectId = "31005",
                Detail = string.Format(_responseOptions.GemeenteDetailUrl, "31005"),
                Gemeentenaam = new Gemeentenaam(new GeografischeNaam("Brugge", Taal.NL))
            };

            var rnd = new Random();

            return new StreetNameResponse(
                _responseOptions.Naamruimte,
                rnd.Next(10000, 15000),
                StraatnaamStatus.InGebruik,
                gemeente,
                DateTimeOffset.Now,
                "Baliestraat");
        }
    }

    public class StreetNameNotFoundResponseExamples : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ProblemDetails
            {
                HttpStatus = StatusCodes.Status404NotFound,
                Title = ProblemDetails.DefaultTitle,
                Detail = "Onbestaande straatnaam.",
                ProblemInstanceUri = ProblemDetails.GetProblemNumber()
            };
        }
    }

    public class StreetNameGoneResponseExamples : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ProblemDetails
            {
                HttpStatus = StatusCodes.Status410Gone,
                Title = ProblemDetails.DefaultTitle,
                Detail = "Straatnaam verwijderd.",
                ProblemInstanceUri = ProblemDetails.GetProblemNumber()
            };
        }
    }
}
