using ToDoListWebApi.Domain.Enums;

namespace ToDoListWebApi.ToDoList.Queries.GetAllItemsWithFilters;

public class GetAllItemsWithFiltersQuery
{
    public Priority? Priority { get; set; } = null;
    public bool? IsCompleted { get; set; } = null;
    public DateTime? DeadLine { get; set; } = null;
}