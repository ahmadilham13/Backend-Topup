namespace backend.Entities;

public interface ISoftDelete
{
    public DateTime? DeletedAt { get; set; }
}