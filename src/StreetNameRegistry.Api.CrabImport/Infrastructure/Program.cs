namespace StreetNameRegistry.Api.CrabImport.Infrastructure
{
    using Be.Vlaanderen.Basisregisters.Api;
    using Microsoft.AspNetCore.Hosting;

    public class Program
    {
        private static readonly DevelopmentCertificate DevelopmentCertificate = new DevelopmentCertificate(
            "api.dev.straatnaam.basisregisters.vlaanderen.be.pfx",
            "gemeenteregister!");

        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => new WebHostBuilder()
                .UseDefaultForApi<Startup>(
                    new ProgramOptions
                    {
                        Hosting =
                        {
                            HttpPort = 1092,
                            HttpsPort = 1446,
                            HttpsCertificate = DevelopmentCertificate.ToCertificate,
                        },
                        Runtime =
                        {
                            CommandLineArgs = args
                        }
                    });
    }
}
