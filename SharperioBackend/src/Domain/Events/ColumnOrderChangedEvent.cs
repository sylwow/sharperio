namespace SharperioBackend.Domain.Events;

public class ColumnOrderChangedEvent : DomainEvent
{
    public Guid TableId { get; set; }
    public int ColumnId { get; set; }
    public int NewIndex { get; set; }
}
