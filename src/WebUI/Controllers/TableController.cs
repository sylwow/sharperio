using CleanArchitecture.Application.Tables.Commands.CreateTable;
using CleanArchitecture.Application.Tables.Commands.DeleteTable;
using CleanArchitecture.Application.Tables.Commands.UpdateTable;
using CleanArchitecture.Application.Tables.Queries.GetTable;
using CleanArchitecture.Application.Tables.Queries.GetTableList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TableDto = CleanArchitecture.Application.Tables.Queries.GetTable.TableDto;

namespace CleanArchitecture.WebUI.Controllers;

[Authorize]
public class TableController : ApiControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<TableDtoList>> GetList()
    {
        return await Mediator.Send(new GetTableListQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TableDto>> Get(Guid id)
    {
        return await Mediator.Send(new GetTableQuery { Id = id });
    }

    [HttpPost]
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
