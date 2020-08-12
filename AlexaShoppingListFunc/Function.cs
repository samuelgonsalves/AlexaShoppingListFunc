using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using SkillLibrary;
using System;
using System.Threading.Tasks;
using Trello.Clients;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace AlexaShoppingListFunc
{
    public class Function
    {
        private static TrelloApiClient _trelloApiClient = new TrelloApiClient(Environment.GetEnvironmentVariable("ApiKey"), 
                                                                              Environment.GetEnvironmentVariable("ApiToken"));
        /// <summary>
        /// The main entry point for the custom runtime.
        /// </summary>
        /// <param name="args"></param>
        private static async Task Main(string[] args)
        {
            Func<SkillRequest, ILambdaContext, Task<SkillResponse>> func = FunctionHandlerAsync;
            using (var handlerWrapper = HandlerWrapper.GetHandlerWrapper(func, new Amazon.Lambda.Serialization.Json.JsonSerializer()))
            using (var bootstrap = new LambdaBootstrap(handlerWrapper))
            {
                await bootstrap.RunAsync();
            }
        }

        public static async Task<SkillResponse> FunctionHandlerAsync(SkillRequest input, ILambdaContext context)
        {
            if (input.IsIntentRequest())
            {
                var intentRequest = input.Request as IntentRequest;

                // TODO: Figure out how to find list without hardcoding listId
                if (intentRequest.Intent.Name.Equals("GroceryBoardIntent"))
                {
                    var item = intentRequest.Intent.Slots["GroceryItem"].Value;

                    await _trelloApiClient.Cards.CreateCardAsync(new Trello.Responses.Card { ListId = "5ed532f0b58ba36765935ff3", Name = item.CapitalizeFirstLetterOfEachWord() });

                    return ResponseBuilder.Tell($"I am adding {item} to grocery board");
                }
                else if (intentRequest.Intent.Name.Equals("GetGroceryItemsIntent"))
                {
                    var cardsOnGroceryList = await _trelloApiClient.Lists.GetCards("5ed532f0b58ba36765935ff3");
                    return ResponseBuilder.Tell(SkillResponseSentences.GetGroceryItems(cardsOnGroceryList));
                }
            }
            else if (input.IsLaunchRequest())
            {
                return ResponseBuilder.Ask("I can add grocery items to your Trello grocery board", new Reprompt("Come again?"));
            }
            else if (input.IsSessionEndedRequest())
            {
                var defaultSkillResponse = new SkillResponse
                {
                    Response = new ResponseBody
                    {
                        ShouldEndSession = true,
                        OutputSpeech = new PlainTextOutputSpeech { Text = "Session Ended is called!" }
                    }
                };

                return defaultSkillResponse;
            }

            return ResponseBuilder.Tell("Skill default response");
        }
    }
}