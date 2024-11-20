using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

[TestFixture]
public class API_TEST_HttpClient
{
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    }

    [Test]
    public async Task GetTest()
    {
        HttpResponseMessage response = await _client.GetAsync("posts/1");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "GET response status code is not OK.");

        string responseBody = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(responseBody.Contains("\"id\": 1"), "GET response does not contain expected data.");
    }

    [Test]
    public async Task PostTest()
    {
        var postData = new
        {
            title = "foo",
            body = "bar",
            userId = 1
        };
        StringContent content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync("posts", content);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, "POST response status code is not Created.");

        string responseBody = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(responseBody.Contains("\"title\": \"foo\""), "POST response does not contain expected title.");
    }

    [Test]
    public async Task PutTest()
    {
        var putData = new
        {
            id = 1,
            title = "updated title",
            body = "updated body",
            userId = 1
        };
        StringContent content = new StringContent(JsonSerializer.Serialize(putData), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PutAsync("posts/1", content);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "PUT response status code is not OK.");

        string responseBody = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(responseBody.Contains("\"title\": \"updated title\""), "PUT response does not contain updated title.");
    }

    [Test]
    public async Task DeleteTest()
    {
        HttpResponseMessage response = await _client.DeleteAsync("posts/1");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "DELETE response status code is not OK.");
    }

    [TearDown]
    public void Cleanup()
    {
        _client.Dispose();
    }
}
