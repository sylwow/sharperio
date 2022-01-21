using CleanArchitecture.Application.Columns.Commands.UpdateItem;
using CleanArchitecture.Application.Items.Commands.CreateItem;
using CleanArchitecture.Application.Items.Commands.DeleteItem;
using CleanArchitecture.Application.Items.Queries.GetItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

[Authorize]
public class ItemController : ApiControllerBase
{
    [HttpGet("id")]
    public async Task<ActionResult<ItemDto>> Get(int id)
    {
        return await Mediator.Send(new GetItemQuery { Id = id });
    }

    [HttpPost()]
    public async Task<ActionResult<int>> Create(CreateItemCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateItemCommand command)
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
        await Mediator.Send(new DeleteItemCommand { Id = id });

        return NoContent();
    }
}
