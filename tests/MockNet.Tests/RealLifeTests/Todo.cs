namespace Theorem.MockNet.Http.Tests.RealLifeTests
{
    public class Todo
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }

        public Todo() {}

        public Todo(int id, int userId, string title) => (Id, UserId, Title) = (id, userId, title);
    }
}
