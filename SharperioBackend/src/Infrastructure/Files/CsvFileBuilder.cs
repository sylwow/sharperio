using System.Globalization;
using SharperioBackend.Application.Common.Interfaces;
using SharperioBackend.Application.TodoLists.Queries.ExportTodos;
using SharperioBackend.Infrastructure.Files.Maps;
using CsvHelper;

namespace SharperioBackend.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
