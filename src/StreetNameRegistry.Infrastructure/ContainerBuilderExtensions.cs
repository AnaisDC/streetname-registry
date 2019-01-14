namespace StreetNameRegistry.Infrastructure
{
    using Be.Vlaanderen.Basisregisters.AggregateSource.SqlStreamStore.Autofac;
    using Be.Vlaanderen.Basisregisters.DataDog.Tracing.SqlStreamStore;
    using Autofac;
    using Microsoft.Extensions.Configuration;

    public static class ContainerBuilderExtensions
    {
        public static void RegisterEventstreamModule(this ContainerBuilder builder, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Events");
            if (string.IsNullOrWhiteSpace(connectionString))
                return;

            builder.RegisterModule(new SqlStreamStoreModule(connectionString, Schema.Default));
            builder.RegisterModule(new TraceSqlStreamStoreModule(configuration["DataDog:ServiceName"]));
        }
    }
}
