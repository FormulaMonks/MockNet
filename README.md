![.NET Core](https://github.com/citrusbyte/MockNet/workflows/.NET%20Core/badge.svg?branch=master)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/nuget/v/MockNet.svg?style=flat)](https://www.nuget.org/packages/MockNet/)

# Theorem.MockNet.Http

The package provides a friendly mocking framework to unit test the
System.Net.Http namespace. 

## Main features:
- works with any .NET unit testing and mocking library
- convenient syntax to setup http request and response messages and their properties
- ability to validate request headers and content matching with lambda predicates
- ability to specify status code, headers and content to be returned from the HTTP call

## Examples:

A quick and simple example. This sample mocks a call to the
`https://jsonplaceholder.typeicode.com/todos/1` resource to return a status code
of 201 Created.

``` csharp
using Theorem.MockNet.Http;
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

    public Todo() {}

    public Todo(int id, int userId, string title) => (Id, UserId, Title) = (id, userId, title);
}
```

Service to consume the api

``` csharp
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SystemStringContent = System.Net.Http.StringContent;

public class TodoService
{
    private HttpClient _httpClient;

    public TodoService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public Task<Todo> GetAsync(int id) => GetAsync($"/todos/{id}");

    private async Task<Todo> GetAsync(string requestUri)
    {
        var response = await this._httpClient.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Todo not found");
        }

        var json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Todo>(json);
    }

    public async Task<Todo> CreateAsync(Todo todo)
    {
        var content = new SystemStringContent(JsonConvert.SerializeObject(todo), Encoding.UTF8, "application/json");

        var response = await this._httpClient.PostAsync("/todos", content);

        if (response.IsSuccessStatusCode &&
            response.Headers.Location is Uri uri)
        {
            return await GetAsync(uri.ToString());
        }

        throw new Exception("Error creating todo");
    }
}
```

And the unit test for the `TodoService`

``` csharp
using Theorem.MockNet.Http;
using Xunit;

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
    public async Task GetAsync_should_return_expected_todo()
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
    public async Task GetAsync_should_throw_exception_when_invalid_status_code_is_returned()
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
        responseHeaders.Add("Location", $"https://localhost/todos/{expected.Id}");

        // pass lambda predicate to validate request content
        // response from the HTTP Post method will have 201 status code and Location header
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
    public async Task CreateAsync_should_throw_exception_when_invalid_status_code_is_returned()
    {
        mock.SetupPost("/todos").ReturnsAsync(404);

        await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(new Todo()));
    }

    [Fact]
    public async Task CreateAsync_should_throw_exception_when_missing_location_header()
    {
        mock.SetupPost("/todos").ReturnsAsync(200);

        await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(new Todo()));
    }
}
```

For more examples on how to test other HttpClient aspects including testing headers and content body being sent to the
endpoint as well as headers being returned please see [tests/MockNet.Tests/Playground.cs](https://github.com/citrusbyte/MockNet/blob/master/tests/MockNet.Tests/Playground.cs)


# About TheoremOne

![TheoremOne](https://theoremone-assets.s3.amazonaws.com/TheoremOne-Logo.png)

This software is lovingly maintained and funded by [TheoremOne](https://theoremone.co).
At [TheoremOne](https://theoremone.co), we specialize in solving difficult computer science problems for startups and the enterprise.

At TheoremOne we believe in and support open source software.
* Check out more of our open source software at TheoremOne Labs.
* Learn more about [our work](https://theoremone.co/portfolio).
* [Hire us](https://theoremone.co/contact-us) to work on your project.
* [Want to join the team?](http://theoremone.co/careers)

*TheoremOne and the TheoremOne logo are trademarks or registered trademarks of TheoremOne, LLC.*
