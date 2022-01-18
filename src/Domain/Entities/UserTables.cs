namespace CleanArchitecture.Domain.Entities;

public class UserTables
{
    public int Id { get; set; }
    public string ExternalId { get; set; }
    public List<Table> Tables { get; set; }
}
