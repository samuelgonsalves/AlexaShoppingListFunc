using Alexa.NET;
using Alexa.NET.Reminders;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using SkillLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (input.Context.System.User.Permissions == null)
            {
                return ResponseBuilder.TellWithAskForPermissionConsentCard("To set reminders, you need to allow Trello Grocery to create and edit reminders for this skill. Would you like to do that?",
                                                                            new[] { "alexa::alerts:reminders:skill:readwrite" }, input.Session
                                                                          );
            }
            else
            {
                // Has permissions, ask if user wants to create a reminder?

                var sess = input.Session;

                if (!await RemindersExtensions.AnyActiveRemindersAsync(input) 
                    && sess.Attributes == null)
                {
                    sess.Attributes = new Dictionary<string, object>();
                    sess.Attributes["reminderRequest"] = true;

                    return ResponseBuilder.Ask("Would you like to be reminded to get your groceries weekly?", new Reprompt("I'm sorry, I didn't catch that. Could you repeat it?"), sess);
                }
            }

            if (input.IsIntentRequest())
            {
                var intentRequest = input.Request as IntentRequest;

                // TODO: Figure out how to find list without hardcoding listId
                if (intentRequest.IsAddGroceries())
                {
                    var item = intentRequest.Intent.Slots["GroceryItem"].SlotValue;

                    if (item.SlotType == SlotValueType.List)
                    {
                        foreach (var v in item.Values)
                        {
                            await _trelloApiClient.Cards.CreateCardAsync(new Trello.Responses.Card { ListId = "5ed532f0b58ba36765935ff3", Name = v.Value.CapitalizeFirstLetterOfEachWord() });
                        }

                        return ResponseBuilder.Tell($"I've added {item.Values.Count()} items to your grocery board.");
                    }

                    await _trelloApiClient.Cards.CreateCardAsync(new Trello.Responses.Card { ListId = "5ed532f0b58ba36765935ff3", Name = item.Value.CapitalizeFirstLetterOfEachWord() });

                    return ResponseBuilder.Tell($"I've added {item} to your grocery board");
                }
                else if (intentRequest.IsGetGroceries())
                {
                    var cardsOnGroceryList = await _trelloApiClient.Lists.GetCards("5ed532f0b58ba36765935ff3");
                    return ResponseBuilder.Tell(SkillResponseSentences.GetGroceryItems(cardsOnGroceryList));
                }
                else if (intentRequest.IsFinishedShopping())
                {
                    // Archive all cards on the Already Have list
                    // TODO: Figure out how to archive list without hardcoding listId
                    await _trelloApiClient.Lists.ArchiveAllCards("5ed65088014d693574335aec");

                    return SkillResponseExtensions.GetFinishedShoppingIntentResponse();
                }
                else if (intentRequest.Yes())
                {
                    if (input.Session.Attributes == null)
                        throw new Exception("No attributes specified for YES intent");

                    if (input.Session.Attributes["reminderRequest"].Equals(true))
                    {
                        var response = await RemindersExtensions.CreateReminder(
                                   input,
                                   new Reminder
                                   {
                                       RequestTime = DateTime.UtcNow,
                                       AlertInformation = new AlertInformation(new[] { new SpokenContent("Get your groceries!", "en-GB") }),
                                       Trigger = new RelativeTrigger(120), // 2 minutes
                                       PushNotification = PushNotification.Enabled
                                   }
                               );

                        return ResponseBuilder.Tell("I've added a reminder.");
                    }
                    
                }
            }
            else if (input.IsLaunchRequest())
            {
                return SkillResponseExtensions.GetLaunchResponse();
            }
            else if (input.IsSessionEndedRequest())
            {
                return SkillResponseExtensions.GetSessionEndedResponse();
            }

            return ResponseBuilder.Tell("Skill default response");
        }
    }
}