namespace StreetNameRegistry.Api.Legacy.StreetName.Query
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Be.Vlaanderen.Basisregisters.GrAr.Common;
    using Be.Vlaanderen.Basisregisters.GrAr.Legacy;
    using Be.Vlaanderen.Basisregisters.GrAr.Legacy.Gemeente;
    using Be.Vlaanderen.Basisregisters.GrAr.Legacy.Straatnaam;
    using Convertors;
    using Infrastructure.Options;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Projections.Legacy;
    using Projections.Syndication;
    using Projections.Syndication.Municipality;
    using Responses;

    public class StreetNameDetailQuery
    {
        private readonly LegacyContext _legacyContext;
        private readonly SyndicationContext _syndicationContext;
        private readonly ResponseOptions _responseOptions;

        public StreetNameDetailQuery(
            LegacyContext legacyContext,
            SyndicationContext syndicationContext,
            IOptions<ResponseOptions> responseOptionsProvider)
        {
            _legacyContext = legacyContext;
            _syndicationContext = syndicationContext;
            _responseOptions = responseOptionsProvider.Value;
        }

        public async Task<StreetNameResponse> FilterAsync(int osloId, CancellationToken ct = default)
        {
            var streetName = await _legacyContext
                .StreetNameDetail
                .AsNoTracking()
                .Where(x => !x.Removed)
                .SingleOrDefaultAsync(item => item.OsloId == osloId, ct);

            if (streetName == null)
                throw new ApiException("Onbestaande straatnaam.", StatusCodes.Status404NotFound);

            var municipality = await _syndicationContext
                .MunicipalitySyndicationItems
                .AsNoTracking()
                .OrderByDescending(m => m.Position)
                .FirstOrDefaultAsync(m => m.NisCode == streetName.NisCode, ct);

            var municipalityDefaultName = GetDefaultMunicipalityName(municipality);
            var gemeente = new StraatnaamDetailGemeente
            {
                ObjectId = streetName.NisCode,
                Detail = string.Format(_responseOptions.GemeenteDetailUrl, streetName.NisCode),
                Gemeentenaam = new Gemeentenaam(new GeografischeNaam(municipalityDefaultName.Value, municipalityDefaultName.Key))
            };

            return new StreetNameResponse(
                _responseOptions.Naamruimte,
                streetName.OsloId.Value,
                streetName.Status.ConvertFromStreetNameStatus(),
                gemeente,
                streetName.VersionTimestamp.ToBelgianDateTimeOffset(),
                streetName.NameDutch,
                streetName.NameFrench,
                streetName.NameGerman,
                streetName.NameEnglish,
                streetName.HomonymAdditionDutch,
                streetName.HomonymAdditionFrench,
                streetName.HomonymAdditionGerman,
                streetName.HomonymAdditionEnglish);
        }

        private static KeyValuePair<Taal, string> GetDefaultMunicipalityName(MunicipalitySyndicationItem municipality)
        {
            var names = new []
            {
                new KeyValuePair<Taal,string>(Taal.NL, municipality.NameDutch),
                new KeyValuePair<Taal,string>(Taal.FR, municipality.NameFrench),
                new KeyValuePair<Taal,string>(Taal.DE, municipality.NameGerman),
                new KeyValuePair<Taal,string>(Taal.EN, municipality.NameEnglish)
            };

            return names.FirstOrDefault(n => !string.IsNullOrEmpty(n.Value));
        }
    }
}
