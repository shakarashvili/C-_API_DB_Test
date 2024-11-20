using NUnit.Framework;
using RestSharp;
using System.Net;

[TestFixture]
public class ApiTestsWithRestSharp
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Structure", "NUnit1032:An IDisposable field/property should be Disposed in a TearDown method", Justification = "<Pending>")]
    private RestClient _client;

    [SetUp]
    public void Setup()
    {
        _client = new RestClient("https://jsonplaceholder.typicode.com/");
    }

    [Test]
    public void GetTest()
    {
        var request = new RestRequest("posts/1", Method.Get);
        RestResponse response = _client.Execute(request);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "GET response status code is not OK.");
        Assert.IsTrue(response.Content.Contains("\"id\": 1"), "GET response does not contain expected data.");
    }

    [Test]
    public void PostTest()
    {
        var request = new RestRequest("posts", Method.Post);
        request.AddJsonBody(new
        {
            title = "foo",
            body = "bar",
            userId = 1
        });

        RestResponse response = _client.Execute(request);

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, "POST response status code is not Created.");
        Assert.IsTrue(response.Content.Contains("\"title\": \"foo\""), "POST response does not contain expected title.");
    }

    [Test]
    public void PutTest()
    {
        var request = new RestRequest("posts/1", Method.Put);
        request.AddJsonBody(new
        {
            id = 1,
            title = "updated title",
            body = "updated body",
            userId = 1
        });

        RestResponse response = _client.Execute(request);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "PUT response status code is not OK.");
        Assert.IsTrue(response.Content.Contains("\"title\": \"updated title\""), "PUT response does not contain updated title.");
    }

    [Test]
    public void DeleteTest()
    {
        var request = new RestRequest("posts/1", Method.Delete);
        RestResponse response = _client.Execute(request);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "DELETE response status code is not OK.");
    }
}
