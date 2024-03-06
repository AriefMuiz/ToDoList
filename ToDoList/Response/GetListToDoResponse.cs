using ToDoList.Responses;

namespace ToDoList.Response
{
    public class GetListToDoResponse : BaseResponse
    {
        public List<GetByIDToDoResponse> ToDoList { get; set; }
    }
}