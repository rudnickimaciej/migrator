namespace Migrator.Commons
{
    public class TFieldModel
    {
        public int ID { get; set; }
        public string EntityName { get; set; }
        public FieldType Type { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string NetType { get; set; }
        public SQLType SqlType { get; set; }
    }
    public enum FieldType
    {
        SIMPLE,
        REFERENCE,
        SIMPLE_LIST,
        REFERENCE_LIST,
        UNDEFINED
    }

    public enum SQLType
    {
        SMALLINT,
        INT,
        BIGINT,
        FLOAT,
        DECIMAL,
        VARCHAR,
        DATETIME,
        BIT
    }
}
