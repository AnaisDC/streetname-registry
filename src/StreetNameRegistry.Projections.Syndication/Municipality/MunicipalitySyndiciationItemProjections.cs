namespace StreetNameRegistry.Projections.Syndication.Municipality
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Be.Vlaanderen.Basisregisters.GrAr.Legacy;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.Syndication;

    public class MunicipalitySyndiciationItemProjections : AtomEntryProjectionHandlerModule<MunicipalityEvent, Gemeente, SyndicationContext>
    {
        public MunicipalitySyndiciationItemProjections()
        {
            When(MunicipalityEvent.MunicipalityWasRegistered, AddSyndicationItemEntry);
            When(MunicipalityEvent.MunicipalityNisCodeWasDefined, AddSyndicationItemEntry);
            When(MunicipalityEvent.MunicipalityNisCodeWasCorrected, AddSyndicationItemEntry);
            When(MunicipalityEvent.MunicipalityWasNamed, AddSyndicationItemEntry);
            When(MunicipalityEvent.MunicipalityNameWasCleared, AddSyndicationItemEntry);
            When(MunicipalityEvent.MunicipalityNameWasCorrected, AddSyndicationItemEntry);
            When(MunicipalityEvent.MunicipalityNameWasCorrectedToCleared, AddSyndicationItemEntry);
        }

        private static async Task AddSyndicationItemEntry(AtomEntry<Gemeente> entry, SyndicationContext context, CancellationToken ct)
        {
            var municipalitySyndicationItem = new MunicipalitySyndicationItem
            {
                MunicipalityId = entry.Content.Id,
                NisCode = entry.Content.Identificator?.ObjectId,
                Version = entry.Content.Identificator?.Versie.Value,
                Position = long.Parse(entry.FeedEntry.Id)
            };

            UpdateNamesByGemeentenamen(municipalitySyndicationItem, entry.Content.Gemeentenamen);

            await context
                .MunicipalitySyndicationItems
                .AddAsync(municipalitySyndicationItem, ct);
        }

        private static void UpdateNamesByGemeentenamen(MunicipalitySyndicationItem syndicationItem, IReadOnlyCollection<GeografischeNaam> gemeentenamen)
        {
            if (gemeentenamen == null || !gemeentenamen.Any())
                return;

            foreach (var naam in gemeentenamen)
            {
                switch (naam.Taal)
                {
                    default:
                    case Taal.NL:
                        syndicationItem.NameDutch = naam.Spelling;
                        break;
                    case Taal.FR:
                        syndicationItem.NameFrench = naam.Spelling;
                        break;
                    case Taal.DE:
                        syndicationItem.NameGerman = naam.Spelling;
                        break;
                    case Taal.EN:
                        syndicationItem.NameEnglish = naam.Spelling;
                        break;
                }
            }
        }
    }
}
