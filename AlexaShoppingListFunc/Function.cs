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
using System.Threading.Tasks;
using Trello.Clients;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace AlexaShoppingListFunc
{
    public class Function
    {
        private static TrelloApiClient _trelloApiClient = new TrelloApiClient(Environment.GetEnvironmentVariable("ApiKey"),
                                                                              Environment.GetEnvironmentVariable("ApiToken"));

        private static string ToBuyListId => "5ed532f0b58ba36765935ff3";
        private static string BoughtListId => "5ed65088014d693574335aec";

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
            try
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

                        return SkillResponseExtensions.GetPermissionsRequestResponse(sess);
                    }
                }

                if (input.IsIntentRequest())
                {
                    var intentRequest = input.Request as IntentRequest;

                    if (intentRequest.IsAddGroceries())
                    {
                        var item = intentRequest.Intent.Slots["GroceryItem"].SlotValue;

                        if (item.SlotType == SlotValueType.List)
                        {
                            foreach (var v in item.Values)
                            {
                                await _trelloApiClient.Cards.CreateCardAsync(new Trello.Responses.Card { ListId = ToBuyListId, Name = v.Value.CapitalizeFirstLetterOfEachWord() });
                            }

                            return ResponseBuilder.Tell($"I've added {item.Values.Count()} items to your grocery board.");
                        }

                        await _trelloApiClient.Cards.CreateCardAsync(new Trello.Responses.Card { ListId = ToBuyListId, Name = item.Value.CapitalizeFirstLetterOfEachWord() });

                        return ResponseBuilder.Tell($"I've added {item} to your grocery board");
                    }
                    else if (intentRequest.IsGetGroceries())
                    {
                        var cardsOnGroceryList = await _trelloApiClient.Lists.GetCards(ToBuyListId);
                        return ResponseBuilder.Tell(SkillResponseSentences.GetGroceryItems(cardsOnGroceryList));
                    }
                    else if (intentRequest.IsFinishedShopping())
                    {
                        // Archive all cards on the Already Have list
                        // TODO: Figure out how to archive list without hardcoding listId
                        await _trelloApiClient.Lists.ArchiveAllCards(BoughtListId);

                        return SkillResponseExtensions.GetFinishedShoppingIntentResponse();
                    }
                    else if (intentRequest.Yes())
                    {
                        if (input.Session.Attributes == null)
                            throw new Exception("No attributes specified for YES intent");

                        if (input.Session.Attributes["reminderRequest"].Equals(true))
                        {
                            await RemindersExtensions.SetAWeeklyReminder(input);

                            return SkillResponseExtensions.GetReminderCreatedResponse();
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
            }
            catch(Exception ex)
            {
                context.Logger.Log(ex.Message);
            }

            return ResponseBuilder.Tell("Skill default response");
        }
    }
}