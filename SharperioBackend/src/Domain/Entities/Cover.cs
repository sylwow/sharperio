using System.Drawing;

namespace SharperioBackend.Domain.Entities;

public class Cover : AuditableEntity
{
    public int Id { get; set; }
    public bool IsImage { get; set; }
    public string Color { get; set; }
    public string ImageUrl { get; set; }
}
