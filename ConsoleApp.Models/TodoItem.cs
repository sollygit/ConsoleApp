namespace ConsoleApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string OwnerId { get; set; }

        public void Deconstruct(out int id, out string name, out bool isComplete, out string ownerId)
        {
            id = Id;
            name = Name;
            isComplete = IsComplete;
            ownerId = OwnerId;
        }
    }
}