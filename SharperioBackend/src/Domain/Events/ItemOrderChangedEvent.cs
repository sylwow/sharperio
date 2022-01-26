
namespace SharperioBackend.Domain.Events;

public class ItemOrderChangedEvent : DomainEvent
{
    public Guid TableId { get; set; }
    public int ItemId { get; set; }
    public int Index { get; set; }
    public int PreviousColumnId { get; set; }
    public int NewColumnId { get; set; }
}
