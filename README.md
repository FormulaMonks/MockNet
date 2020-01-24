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

Lets look at are more real life example:

``` csharp
using MockNet.Http;
using Xunit;

...

public class Todo {
    public int UserId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Completed { get; set; }
}

...

public class TodoService {
    private System.Net.Http.HttpClient _httpClient;
    
    public TodoService(System.Net.Http.HttpClient httpClient) {
        this._httpClient = httpClient;
    }
    
    public async Task<Todo> GetAsync(int id) {
        var response = await this._httpClient.GetAsync($"/todos/{id}");
        
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Todo>(json);
        }

        throw new Exception("NotFound");
    }
}

...

[Fact]
public async Task GetAsync_should_return_expected_todoAsync() {
    var mock = new MockHttpClient();
    var service = new TodoService(mock.Object);

    var expected = new Todo {
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
```

# About Theorem

![Theorem](https://cl.ly/8b0a99ca064a/logo.png)

This software is lovingly maintained and funded by Theorem.
At Theorem, we specialize in solving difficult computer science problems for startups and the enterprise.

At Theorem we believe in and support open source software.
* Check out more of our open source software at Theorem Labs.
* Learn more about [our work](https://theorem.co/portfolio).
* [Hire us](https://theorem.co/contact-us) to work on your project.
* [Want to join the team?](http://theorem.co/careers)

*Theorem and the Theorem logo are trademarks or registered trademarks of Theorem, LLC.*
