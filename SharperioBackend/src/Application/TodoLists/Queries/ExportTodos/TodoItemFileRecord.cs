using SharperioBackend.Application.Common.Mappings;
using SharperioBackend.Domain.Entities;

namespace SharperioBackend.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
