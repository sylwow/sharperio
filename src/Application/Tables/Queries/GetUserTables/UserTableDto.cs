using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Tables.Queries.GetUserTables;

public class UserTableDto : IMapFrom<Table>
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string Title { get; set; }
    public Cover Cover { get; set; }
    public bool IsPrivate { get; set; }
}
