using CleanArchitecture.Application.Items.Commands.CreateItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

[Authorize]
public class ItemController : ApiControllerBase
{
    /*[HttpGet()]
    public async Task<ActionResult<ColumnDto?>> Get([FromQuery] GetColumnQuery query)
    {
        return await Mediator.Send(query);
    }*/

    [HttpPost("{ColumnId}")]
    public async Task<ActionResult<int>> Create([FromRoute] int columnId, CreateItemCommand command)
    {
        return await Mediator.Send(command);
    }
}
