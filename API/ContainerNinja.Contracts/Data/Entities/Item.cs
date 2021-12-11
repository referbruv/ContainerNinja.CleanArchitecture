namespace ContainerNinja.Contracts.Data.Entities
{
    public class Item : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Categories { get; set; }
        public string ColorCode { get; set; }
    }
}