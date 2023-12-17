using Microsoft.AspNetCore.Mvc;

using TodoList.BLL.Interfaces;
using TodoList.Shared.Models.Todo;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ILogger<TodoListController> _logger;
        private readonly ITodoService _todoService;

        public TodoListController(ILogger<TodoListController> logger, ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoViewModel>>> GetAll(CancellationToken cancellationToken)
        {
            var todos = await _todoService.GetAll(cancellationToken);
            return Ok(todos);
        }

        [HttpPost]
        public async Task<ActionResult<TodoViewModel>> Add([FromBody] TodoCreateModel createModel, CancellationToken cancellationToken)
        {
            var todo = await _todoService.Add(createModel, cancellationToken);
            return Ok(todo);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<TodoViewModel>> Update(Guid id, [FromBody] TodoUpdateModel updateModel, CancellationToken cancellationToken)
        {
            var todo = await _todoService.Update(id, updateModel, cancellationToken);
            return Ok(todo);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _todoService.Delete(id, cancellationToken);
            return Ok();
        }
    }
}
