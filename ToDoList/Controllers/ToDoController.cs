using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Interface;
using ToDoList.Request;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService toDoService;

        public ToDoController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        //add new task
        [HttpPost]
        public async Task<IActionResult> AddNewTaskAsync([FromBody] AddTaskRequest request)
        {
            var response = await toDoService.AddNewTaskAsync(request);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToDoListAsync()
        {
            var response = await toDoService.GetAllToDoListAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskByIdAsync(int id)
        {
            var response = await toDoService.GetTaskByIdAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

        //update task by id
        [HttpPut]
        public async Task<IActionResult> UpdateTaskByIdAsync([FromBody] UpdateTaskRequest request)
        {
            var response = await toDoService.UpdateTaskByIdAsync(request);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

        //delete task by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskByIdAsync(int id)
        {
            var response = await toDoService.DeleteTaskByIdAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }
    }
}