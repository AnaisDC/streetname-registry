namespace StreetNameRegistry.Api.CrabImport.CrabImport.Requests
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using Swashbuckle.AspNetCore.Filters;

    public class RegisterCrabImportRequest
    {
        /// <summary>Type van het CRAB item.</summary>
        [Required]
        public string Type { get; set; }

        /// <summary>Het CRAB item.</summary>
        [Required]
        public string CrabItem { get; set; }
    }

    public class RegisterCrabImportRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new RegisterCrabImportRequest
            {
                Type = "StreetNameRegistry.StreetName.Commands.ImportStreetNameFromCrab",
                CrabItem = "{}"
            };
        }
    }

    public static class RegisterCrabImportRequestMapping
    {
        public static dynamic Map(RegisterCrabImportRequest message)
        {
            var assembly = typeof(StreetName.StreetName).Assembly;
            var type = assembly.GetType(message.Type);

            return JsonConvert.DeserializeObject(message.CrabItem, type);
        }
    }
}
