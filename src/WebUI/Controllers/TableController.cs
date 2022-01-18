using CleanArchitecture.Application.Tables.Commands.CreateTable;
using CleanArchitecture.Application.Tables.Queries.GetUserTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

[Authorize]
public class TableController : ApiControllerBase
{
    [HttpGet("user/list")]
    public async Task<ActionResult<List<UserTableDto>?>> GetUserTables([FromQuery] GetUserTablesQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateTableCommand command)
    {
        return await Mediator.Send(command);
    }
    /*
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateTodoItemCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("[action]")]
    public async Task<ActionResult> UpdateItemDetails(int id, UpdateTodoItemDetailCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTodoItemCommand { Id = id });

        return NoContent();
    }*/
}
