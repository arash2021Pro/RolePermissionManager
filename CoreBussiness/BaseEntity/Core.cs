namespace CoreBussiness.BaseEntity;

public class Core
{
    public Core()
    {
        CreationTimeOffset = DateTimeOffset.Now;
    }
    public int Id { get; set; }
    public DateTimeOffset ?CreationTimeOffset { get; set; }
    public DateTimeOffset?ModificationTime { get; set; }
    public bool IsDeleted { get; set; } = false;
}