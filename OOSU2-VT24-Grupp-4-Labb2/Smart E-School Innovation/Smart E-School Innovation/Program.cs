using Microsoft.Extensions.Configuration;
using OpenAI_API;

var configuration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

var key = configuration["OpenAI_Key"];

if (string.IsNullOrEmpty(key))
{
    Console.WriteLine("OpenAI nyckel saknas");
    return;
}
///HH

var api = new OpenAIAPI(key);

var result = await api.Chat.CreateChatCompletionAsync("Förklara vad en pitch är?");

Console.WriteLine(result);