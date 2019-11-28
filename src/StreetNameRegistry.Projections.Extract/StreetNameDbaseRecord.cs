namespace StreetNameRegistry.Projections.Extract
{
    using Be.Vlaanderen.Basisregisters.Shaperon;

    public class StreetNameDbaseRecord : DbaseRecord
    {
        public static readonly StreetNameDbaseSchema Schema = new StreetNameDbaseSchema();

        public DbaseString id { get; }
        public DbaseInt32 straatnmid { get; }
        public DbaseString versieid { get; }
        public DbaseString gemeenteid { get; }
        public DbaseString straatnm { get; }
        public DbaseString homoniemtv { get; }
        public DbaseString status { get; }

        public StreetNameDbaseRecord()
        {
            id = new DbaseString(Schema.id);
            straatnmid = new DbaseInt32(Schema.straatnmid);
            versieid = new DbaseString(Schema.versieid);
            gemeenteid = new DbaseString(Schema.gemeenteid);
            straatnm = new DbaseString(Schema.straatnm);
            homoniemtv = new DbaseString(Schema.homoniemtv);
            status = new DbaseString(Schema.status);

            Values = new DbaseFieldValue[]
            {
                id,
                straatnmid,
                versieid,
                gemeenteid,
                straatnm,
                homoniemtv,
                status
            };
        }
    }
}
