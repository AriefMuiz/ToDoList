using Microsoft.EntityFrameworkCore;
using ToDoDB.Models;
using ToDoList.Interface;
using ToDoList.Request;
using ToDoList.Response;

namespace ToDoList.Services
{
    public class ToDoServices : IToDoService
    {
        private readonly ToDoDBContext ToDoDBContext;

        public ToDoServices(ToDoDBContext ToDoDBContext)
        {
            this.ToDoDBContext = ToDoDBContext;
        }

        //add new task task
        public async Task<AddToDoResponse> AddNewTaskAsync(AddTaskRequest request)
        {
            //check if null
            if (request == null)
            {
                return new AddToDoResponse
                {
                    Success = false,
                    Error = "Request is null",
                    ErrorCode = "F00"
                };
            }
            var task = new ToDoDB.Models.Task
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                IsCompleted = false
            };

            await ToDoDBContext.Tasks.AddAsync(task);
            await ToDoDBContext.SaveChangesAsync();

            return new AddToDoResponse { Success = true };
        }

        //get all task
        public async Task<GetListToDoResponse> GetAllToDoListAsync()
        {
            var tasks = await ToDoDBContext.Tasks.ToListAsync();
            var todoItems = tasks.Select(task => new GetByIDToDoResponse
            {
                TaskID = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted
            }).ToList();

            if (todoItems.Count == 0)
            {
                return new GetListToDoResponse
                {
                    Success = false,
                    Error = "No Task Available",
                    ErrorCode = "F01"
                };
            }

            return new GetListToDoResponse { Success = true, ToDoList = todoItems };
        }

        //get task by id
        public async Task<GetByIDToDoResponse> GetTaskByIdAsync(int id)
        {
            //check if id is valid
            if (id <= 0)
            {
                return new GetByIDToDoResponse
                {
                    Success = false,
                    Error = "Invalid Task ID",
                    ErrorCode = "F03"
                };
            }

            var task = await ToDoDBContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return new GetByIDToDoResponse
                {
                    Success = false,
                    Error = "Task with id: " + id + " Not Found",
                    ErrorCode = "F02"
                };
            }

            return new GetByIDToDoResponse
            {
                Success = true,
                TaskID = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted
            };
        }

        // update task by id
        public async Task<UpdateByIDToDoResponse> UpdateTaskByIdAsync(UpdateTaskRequest request)
        {
            // check if request is null
            if (request == null)
            {
                return new UpdateByIDToDoResponse
                {
                    Success = false,
                    Error = "Request is null",
                    ErrorCode = "F00"
                };
            }

            var task = await ToDoDBContext.Tasks.FindAsync(request.TaskId);
            if (task == null)
            {
                return new UpdateByIDToDoResponse
                {
                    Success = false,
                    Error = "Task Not Found",
                    ErrorCode = "F02"
                };
            }

            // Update task properties based on the request
            if (!string.IsNullOrEmpty(request.Title))
            {
                task.Title = request.Title;
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                task.Description = request.Description;
            }

            if (request.DueDate.HasValue)
            {
                task.DueDate = request.DueDate.Value;
            }

            task.IsCompleted = request.IsCompleted;

            await ToDoDBContext.SaveChangesAsync();

            return new UpdateByIDToDoResponse { Success = true };
        }

        // delete by id
        public async Task<DeleteByIDToDoResponse> DeleteTaskByIdAsync(int id)
        {
            //check if id is valid
            if (id <= 0)
            {
                return new DeleteByIDToDoResponse
                {
                    Success = false,
                    Error = "Invalid Task ID",
                    ErrorCode = "F03"
                };
            }

            var task = await ToDoDBContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return new DeleteByIDToDoResponse
                {
                    Success = false,
                    Error = "Task Not Found",
                    ErrorCode = "F02"
                };
            }

            //do try catch
            try
            {
                ToDoDBContext.Tasks.Remove(task);
                await ToDoDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new DeleteByIDToDoResponse
                {
                    Success = false,
                    Error = ex.Message,
                    ErrorCode = "F04"
                };
            }

            return new DeleteByIDToDoResponse { Success = true };
        }
    }
}