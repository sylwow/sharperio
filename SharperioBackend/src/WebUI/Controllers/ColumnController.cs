using SharperioBackend.Application.Columns.Commands.CreateColumn;
using SharperioBackend.Application.Columns.Commands.DeleteColumn;
using SharperioBackend.Application.Columns.Commands.UpdateColumn;
using SharperioBackend.Application.Columns.Commands.UpdateColumnOrder;
using SharperioBackend.Application.Columns.Queries.GetColumn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SharperioBackend.WebUI.Controllers;

[Authorize]
public class ColumnController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ColumnDto>> Get(int id)
    {
        return await Mediator.Send(new GetColumnQuery { Id = id });
    }

    [HttpPost()]
    public async Task<ActionResult<int>> Create(CreateColumnCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateColumnCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{id}/order")]
    public async Task<ActionResult> UpdateOrder(int id, UpdateColumnOrderCommand command)
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
        await Mediator.Send(new DeleteColumnCommand { Id = id });

        return NoContent();
    }
}
