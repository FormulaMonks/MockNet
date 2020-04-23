using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace Theorem.MockNet.Http.Tests.RealLifeTests
{
    public class TodoServiceTests
    {
        private TodoService service;
        private MockHttpClient mock;

        public TodoServiceTests()
        {
            this.mock = new MockHttpClient();
            this.service = new TodoService(mock.Object);
        }

        [Fact]
        public async Task GetAsync_should_return_expected_todoAsync()
        {
            var expected = new Todo(1, 456, "go shopping");

            mock.SetupGet("/todos/1").ReturnsAsync(201, expected);

            var actual = await service.GetAsync(1);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.UserId, actual.UserId);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Completed, actual.Completed);
        }

        [Fact]
        public async Task GetAsync_should_throw_exception()
        {
            mock.SetupGet("/todos/1").ReturnsAsync(404);

            await Assert.ThrowsAsync<Exception>(() => service.GetAsync(1));
        }

        [Fact]
        public async Task CreateAsync_should_create_and_return_new_todo()
        {
            var todo = new Todo(0, 456, "go shopping");
            var expected = new Todo(2, 456, "go shopping");

            var responseHeaders = new HttpResponseHeaders();
            responseHeaders.Add("todo-id", expected.Id.ToString());

            mock.SetupPost<Todo>("/todos",
                content: x => x.UserId == todo.UserId &&
                    x.Title == todo.Title &&
                    x.Completed == todo.Completed).ReturnsAsync(201, responseHeaders);

            mock.SetupGet($"/todos/{expected.Id}").ReturnsAsync(expected);

            var actual = await service.CreateAsync(todo);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.UserId, actual.UserId);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Completed, actual.Completed);
        }

        [Fact]
        public async Task CreateAsync_should_throw_exception_when_invalidstatus_code_is_returned()
        {
            mock.SetupPost("/todos").ReturnsAsync(404);

            await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(new Todo()));
        }

        [Fact]
        public async Task CreateAsync_should_throw_exception_when_missing_header_value()
        {
            mock.SetupPost("/todos").ReturnsAsync(200);

            await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(new Todo()));
        }

        [Fact]
        public async Task CreateAsync_should_throw_exception_if_header_isnot_an_int()
        {
            var responseHeaders = new HttpResponseHeaders();

            responseHeaders.Add("todo-id", "invalid id");

            mock.SetupPost<Todo>("/todos").ReturnsAsync(201, responseHeaders);

            await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(new Todo()));
        }
    }
}