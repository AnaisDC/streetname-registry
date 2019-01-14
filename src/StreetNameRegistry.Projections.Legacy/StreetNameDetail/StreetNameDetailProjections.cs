namespace StreetNameRegistry.Projections.Legacy.StreetNameDetail
{
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.Connector;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.SqlStreamStore;
    using NodaTime;
    using StreetName.Events;

    public class StreetNameDetailProjections : ConnectedProjection<LegacyContext>
    {
        public StreetNameDetailProjections()
        {
            When<Envelope<StreetNameWasRegistered>>(async (context, message, ct) =>
            {
                await context
                    .StreetNameDetail
                    .AddAsync(
                        new StreetNameDetail
                        {
                            StreetNameId = message.Message.StreetNameId,
                            NisCode = message.Message.NisCode,
                            VersionTimestamp = message.Message.Provenance.Timestamp,
                            Complete = false,
                            Removed = false
                        }, ct);
            });

            When<Envelope<StreetNameNameWasNamed>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        UpdateNameByLanguage(entity, message.Message.Language, message.Message.Name);
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameNameWasCorrected>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        UpdateNameByLanguage(entity, message.Message.Language, message.Message.Name);
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameNameWasCleared>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        UpdateNameByLanguage(entity, message.Message.Language, string.Empty);
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameNameWasCorrectedToCleared>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        UpdateNameByLanguage(entity, message.Message.Language, string.Empty);
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameHomonymAdditionWasDefined>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        UpdateHomonymAdditionByLanguage(entity, message.Message.Language, message.Message.HomonymAddition);
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameHomonymAdditionWasCorrected>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        UpdateHomonymAdditionByLanguage(entity, message.Message.Language, message.Message.HomonymAddition);
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameHomonymAdditionWasCleared>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        UpdateHomonymAdditionByLanguage(entity, message.Message.Language, string.Empty);
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameHomonymAdditionWasCorrectedToCleared>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        UpdateHomonymAdditionByLanguage(entity, message.Message.Language, string.Empty);
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameBecameComplete>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        entity.Complete = true;
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameBecameIncomplete>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        entity.Complete = false;
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameWasRemoved>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        entity.Removed = true;
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameOsloIdWasAssigned>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        entity.OsloId = message.Message.OsloId;
                    },
                    ct);
            });

            When<Envelope<StreetNameBecameCurrent>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        entity.Status = StreetNameStatus.Current;
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameWasProposed>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        entity.Status = StreetNameStatus.Proposed;
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameWasRetired>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        entity.Status = StreetNameStatus.Retired;
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameWasCorrectedToCurrent>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        entity.Status = StreetNameStatus.Current;
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameWasCorrectedToProposed>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        entity.Status = StreetNameStatus.Proposed;
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameWasCorrectedToRetired>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        entity.Status = StreetNameStatus.Retired;
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameStatusWasRemoved>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        entity.Status = null;
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });

            When<Envelope<StreetNameStatusWasCorrectedToRemoved>>(async (context, message, ct) =>
            {
                await context.FindAndUpdateStreetNameDetail(
                    message.Message.StreetNameId,
                    entity =>
                    {
                        entity.Status = null;
                        UpdateVersionTimestamp(entity, message.Message.Provenance.Timestamp);
                    },
                    ct);
            });
        }

        private static void UpdateNameByLanguage(StreetNameDetail entity, Language? language, string name)
        {
            switch (language)
            {
                case Language.Dutch:
                    entity.NameDutch = name;
                    break;

                case Language.French:
                    entity.NameFrench = name;
                    break;

                case Language.German:
                    entity.NameGerman = name;
                    break;

                case Language.English:
                    entity.NameEnglish = name;
                    break;
            }
        }

        private static void UpdateHomonymAdditionByLanguage(StreetNameDetail entity, Language? language, string homonymAddition)
        {
            if (entity == null)
                return;

            switch (language)
            {
                case Language.Dutch:
                    entity.HomonymAdditionDutch = homonymAddition;
                    break;

                case Language.French:
                    entity.HomonymAdditionFrench = homonymAddition;
                    break;

                case Language.German:
                    entity.HomonymAdditionGerman = homonymAddition;
                    break;

                case Language.English:
                    entity.HomonymAdditionEnglish = homonymAddition;
                    break;
            }
        }

        private static void UpdateVersionTimestamp(StreetNameDetail streetName, Instant versionTimestamp)
            => streetName.VersionTimestamp = versionTimestamp;
    }
}
