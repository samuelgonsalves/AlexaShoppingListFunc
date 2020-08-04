using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using SkillLibrary;
using System;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace AlexaShoppingListFunc
{
    public class Function
    {
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

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        ///
        /// To use this handler to respond to an AWS event, reference the appropriate package from 
        /// https://github.com/aws/aws-lambda-dotnet#events
        /// and change the string input parameter to the desired event type.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<SkillResponse> FunctionHandlerAsync(SkillRequest input, ILambdaContext context)
        {
            if (input.IsIntentRequest())
            {
                var intentRequest = input.Request as IntentRequest;

                if (intentRequest.Intent.Name.Equals("GroceryBoardIntent"))
                {
                    //var trelloClient = new TrelloApiClient();

                    var item = intentRequest.Intent.Slots["GroceryItem"].Value;

                    



                    //await trelloClient.CreateCardAsync(item);

                    return ResponseBuilder.Tell($"I am adding {item} to grocery board");
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

            //switch(input.Request)
            //{
            //    case LaunchRequest launchRequest:
            //        LambdaLogger.Log("LaunchRequest");
            //        return ResponseBuilder.Ask("What would you like to add to your Trello Grocery board?", new Reprompt("What was that?"));
            //    case IntentRequest intentRequest:
            //        return ResponseBuilder.Tell(new PlainTextOutputSpeech { Text = "Hello Hello!" });



            //}


            //var response = ResponseBuilder.Tell(new PlainTextOutputSpeech { Text = "Hello Default!" });
            //return response;
        }
    }
}