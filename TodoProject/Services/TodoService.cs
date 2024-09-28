using System.Collections.Concurrent;
using TodoProject.Model;
using TodoProject.Repos;

namespace TodoProject.Services
{
    public class TodoService : ITodoService
    {
        // In-memory data storage
        private readonly ConcurrentDictionary<Guid, TodoItem> _todoItems = new();

        public IEnumerable<TodoItem> GetAll() => _todoItems.Values;

        public TodoItem? GetById(Guid id) =>
            _todoItems.TryGetValue(id, out var item) ? item : null;

        public TodoItem Create(TodoItem item)
        {
            item.Id = Guid.NewGuid();
            _todoItems[item.Id] = item;
            return item;
        }

        public bool Update(TodoItem item)
        {
            if (!_todoItems.ContainsKey(item.Id))
                return false;

            _todoItems[item.Id] = item;
            return true;
        }

        public bool Delete(Guid id) => _todoItems.TryRemove(id, out _);
    }
}
