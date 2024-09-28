using TodoProject.Model;

namespace TodoProject.Repos
{
    public interface ITodoService
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem? GetById(Guid id);
        TodoItem Create(TodoItem item);
        bool Update(TodoItem item);
        bool Delete(Guid id);
    }
}
