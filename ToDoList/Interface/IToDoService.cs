using ToDoList.Request;
using ToDoList.Response;

namespace ToDoList.Interface
{
    public interface IToDoService
    {
        Task<AddToDoResponse> AddNewTaskAsync(AddTaskRequest request);

        Task<GetListToDoResponse> GetAllToDoListAsync();

        Task<GetByIDToDoResponse> GetTaskByIdAsync(int id);

        Task<UpdateByIDToDoResponse> UpdateTaskByIdAsync(UpdateTaskRequest request);

        Task<DeleteByIDToDoResponse> DeleteTaskByIdAsync(int id);
    }
}