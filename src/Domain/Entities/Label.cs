using System.Drawing;

namespace CleanArchitecture.Domain.Entities;

public class Label : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Color { get; set; }
}
