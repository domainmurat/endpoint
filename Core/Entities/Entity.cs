namespace endpoint.Core.Entities
{
    public abstract class Entity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }

    public class Entity : Entity<int>
    {

    }
}
