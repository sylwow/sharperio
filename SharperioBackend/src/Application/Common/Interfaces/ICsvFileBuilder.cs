using SharperioBackend.Application.TodoLists.Queries.ExportTodos;

namespace SharperioBackend.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
