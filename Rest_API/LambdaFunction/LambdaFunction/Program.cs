using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LambdaFunction
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var function = new Function();
      var request = new APIGatewayProxyRequest();
      var context = new TestLambdaContext();

      var response = await function.Handler(request, context);

      Console.WriteLine($"Status Code: {response.StatusCode}");
      Console.WriteLine($"Body: {response.Body}");
    }
  }

  public class Function
  {
    public async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest request, ILambdaContext context)
    {
      // TODO implement
      var response = new APIGatewayProxyResponse
      {
        StatusCode = 200,
        Body = JsonConvert.SerializeObject("Hello from Lambda!"),
        Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
      };

      return await Task.FromResult(response);
    }
  }

  public class TestLambdaContext : ILambdaContext
  {
    public string AwsRequestId => "test-request-id";
    public IClientContext ClientContext => null;
    public string FunctionName => "test-function";
    public string FunctionVersion => "1.0";
    public ICognitoIdentity Identity => null;
    public string InvokedFunctionArn => "test-arn";
    public ILambdaLogger Logger => new TestLambdaLogger();
    public string LogGroupName => "test-log-group";
    public string LogStreamName => "test-log-stream";
    public int MemoryLimitInMB => 128;
    public TimeSpan RemainingTime => TimeSpan.FromMinutes(5);
  }

  public class TestLambdaLogger : ILambdaLogger
  {
    public void Log(string message) => Console.WriteLine(message);
    public void LogLine(string message) => Console.WriteLine(message);
  }
}
