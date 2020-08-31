using Alexa.NET;
using Alexa.NET.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkillLibrary
{
    public static class SkillResponseExtensions
    {
        public static SkillResponse GetLaunchResponse()
        {
            return ResponseBuilder
                .Ask("I can manage your Trello grocery board. Ask me to add an item or to tell you what's on your board.", 
                    new Reprompt("Come again?")
                );
        }

        public static SkillResponse GetSessionEndedResponse()
        {
            return new SkillResponse
            {
                Response = new ResponseBody
                {
                    ShouldEndSession = true,
                    OutputSpeech = new PlainTextOutputSpeech { Text = "Session Ended is called!" }
                }
            };
        }

        public static SkillResponse GetFinishedShoppingIntentResponse()
        {
            var speech = new SsmlOutputSpeech();
            speech.Ssml = "<speak><amazon:emotion name=\"excited\" intensity=\"medium\">I've updated your board. Way to go!</amazon:emotion></speak>";
            return ResponseBuilder.Tell(speech);
        }
    }
}
