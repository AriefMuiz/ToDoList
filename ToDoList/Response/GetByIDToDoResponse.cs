using ToDoList.Responses;

namespace ToDoList.Response
{
    public class GetByIDToDoResponse : BaseResponse
    {
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}