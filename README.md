![.NET Core](https://github.com/citrusbyte/MockNet/workflows/.NET%20Core/badge.svg?branch=master)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

# MockNet.Http

The package provides a friendly mocking framework to unit test the
System.Net.Http namespace. Works with any .NET unit testing and mocking library.

## Examples:

A quick and simple example. This sample mocks a call to the
`https://jsonplaceholder.typeicode.com/todos/1` resource to return a status code
of 201 Created.

``` csharp
using MockNet.Http;
using Xunit; // used here for Asserting.

...

var mock = new MockHttpClient();

mock.SetupGet("/todos/1").ReturnsAsync(201);

var result = await mock.Object.GetAsync("/todos/1");

Assert.Equal(201, (int)result.StatusCode);
```

Lets look at a more real life example:

Todo model

``` csharp
public class Todo
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Completed { get; set; }
}
```

Service to consume the api

``` csharp
using System.Net.Http;

public class TodoService
{
    private HtHttpClient _httpClient;

    public TodoService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public async Task<Todo> GetAsync(int id)
    {
        var response = await this._httpClient.GetAsync($"/todos/{id}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Todo>(json);
        }

        throw new Exception("NotFound");
    }
}
```

And the unit test for the `TodoService`

``` csharp
using MockNet.Http;
using Xunit;

[Fact]
public async Task GetAsync_should_return_expected_todoAsync()
{
    var mock = new MockHttpClient();
    var service = new TodoService(mock.Object);

    var expected = new Todo
    {
        Id = 123,
        UserId = 456,
        Title = "go shopping",
        Completed = false,
    };

    var resultingContent = new StringContent(JsonConvert.SerializeObject(expected));

    mock.SetupGet("/todos/1").ReturnsAsync(201, resultingContent);

    var actual = await service.GetAsync(1);

    Assert.Equal(expected.Id, actual.Id);
    Assert.Equal(expected.UserId, actual.UserId);
    Assert.Equal(expected.Title, actual.Title);
    Assert.Equal(expected.Completed, actual.Completed);
}

[Fact]
public async Task GetAsync_should_throw_exception()
{
    var mock = new MockHttpClient();
    var service = new TodoService(mock.object);

    mock.SetupGet("/todos/1").ReturnsAsync(404);

    await Assert.Throws<Exception>(() => service.GetAsync(1));
}
```

For more examples on how to test other HttpClient aspects including testing headers and content body being sent to the
endpoint as well as headers being returned please see [tests/MockNet.Tests/Playground.cs](https://github.com/citrusbyte/MockNet/blob/master/tests/MockNet.Tests/Playground.cs)


# About Theorem

![Theorem](https://cl.ly/8b0a99ca064a/logo.png)

This software is lovingly maintained and funded by [Theorem](https://theorem.co).
At [Theorem](https://theorem.co), we specialize in solving difficult computer science problems for startups and the enterprise.

At Theorem we believe in and support open source software.
* Check out more of our open source software at Theorem Labs.
* Learn more about [our work](https://theorem.co/portfolio).
* [Hire us](https://theorem.co/contact-us) to work on your project.
* [Want to join the team?](http://theorem.co/careers)

*Theorem and the Theorem logo are trademarks or registered trademarks of Theorem, LLC.*
