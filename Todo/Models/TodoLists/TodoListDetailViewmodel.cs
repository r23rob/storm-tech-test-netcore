using System.Collections.Generic;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }

        public bool ShowCompletedItems { get; set; } = true;

        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items, bool showCompletedItems)
        {
            Items = items;
            ShowCompletedItems = showCompletedItems;
            TodoListId = todoListId;
            Title = title;
        }
    }
}