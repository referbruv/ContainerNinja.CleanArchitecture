namespace ContainerNinja.Contracts.Data.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime Created { get; set; }
    }

    public abstract class AuditableEntity : BaseEntity
    {
        public virtual string CreatedBy { get; set; }
        public virtual string? ModifiedBy { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }
}
