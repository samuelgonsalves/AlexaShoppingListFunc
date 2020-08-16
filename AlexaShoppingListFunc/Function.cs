using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using SkillLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                if (intentRequest.Intent.Name.Equals("AddGroceryItemIntent"))
                {
                    var item = intentRequest.Intent.Slots["GroceryItem"].SlotValue;

                    if(item.SlotType == SlotValueType.List)
                    {
                        foreach(var v in item.Values)
                        {
                            await _trelloApiClient.Cards.CreateCardAsync(new Trello.Responses.Card { ListId = "5ed532f0b58ba36765935ff3", Name = v.Value.CapitalizeFirstLetterOfEachWord() });
                        }

                        return ResponseBuilder.Tell($"I've added {item.Values.Count()} items to your grocery board.");
                    }

                    await _trelloApiClient.Cards.CreateCardAsync(new Trello.Responses.Card { ListId = "5ed532f0b58ba36765935ff3", Name = item.Value.CapitalizeFirstLetterOfEachWord() });

                    return ResponseBuilder.Tell($"I've added {item} to your grocery board");
                }
                else if (intentRequest.Intent.Name.Equals("GetGroceryItemsIntent"))
                {
                    var cardsOnGroceryList = await _trelloApiClient.Lists.GetCards("5ed532f0b58ba36765935ff3");
                    return ResponseBuilder.Tell(SkillResponseSentences.GetGroceryItems(cardsOnGroceryList));
                }
                else if (intentRequest.Intent.Name.Equals("ArchiveAllCardsIntent"))
                {
                    // Archive all cards on the Already Have list
                    // TODO: Figure out how to archive list without hardcoding listId
                    await _trelloApiClient.Lists.ArchiveAllCards("5ed65088014d693574335aec");

                    var speech = new SsmlOutputSpeech();
                    speech.Ssml = "<speak><amazon:emotion name=\"excited\" intensity=\"medium\">I've updated your board. Way to go!</amazon:emotion></speak>";
                    return ResponseBuilder.Tell(speech);
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