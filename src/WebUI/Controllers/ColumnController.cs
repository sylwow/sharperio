using CleanArchitecture.Application.Columns.Commands.CreateColumn;
using CleanArchitecture.Application.Columns.Commands.UpdateColumnOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

[Authorize]
public class ColumnController : ApiControllerBase
{
    /*[HttpGet()]
    public async Task<ActionResult<ColumnDto?>> Get([FromQuery] GetColumnQuery query)
    {
        return await Mediator.Send(query);
    }*/

    [HttpPost("{TableId}")]
    public async Task<ActionResult<int>> Create([FromRoute] Guid tableId, CreateColumnCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPatch("{id}/order")]
    public async Task<ActionResult> Create([FromRoute] int id, UpdateColumnOrderCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}
