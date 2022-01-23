using SharperioBackend.Application.Tables.Commands.CreateTable;
using SharperioBackend.Application.Tables.Commands.DeleteTable;
using SharperioBackend.Application.Tables.Commands.UpdateTable;
using SharperioBackend.Application.Tables.Queries.GetTable;
using SharperioBackend.Application.Tables.Queries.GetTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableDto = SharperioBackend.Application.Tables.Queries.GetTable.TableDto;

namespace SharperioBackend.WebUI.Controllers;

[Authorize]
public class TableController : ApiControllerBase
{
    [HttpGet()]
    public async Task<ActionResult<TablesDto>> GetList()
    {
        return await Mediator.Send(new GetTablesQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TableDto>> Get(Guid id)
    {
        return await Mediator.Send(new GetTableQuery { Id = id });
    }

    [HttpPost()]
    public async Task<ActionResult<Guid>> Create(CreateTableCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateTableCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteTableCommand { Id = id });

        return NoContent();
    }
}
