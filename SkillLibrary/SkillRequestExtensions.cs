using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkillLibrary
{
    public static class SkillRequestExtensions
    {
        public static bool IsIntentRequest(this SkillRequest skillRequest) => skillRequest.GetRequestType() == typeof(IntentRequest);
        public static bool IsLaunchRequest(this SkillRequest skillRequest) => skillRequest.GetRequestType() == typeof(LaunchRequest);
        public static bool IsSessionEndedRequest(this SkillRequest skillRequest) => skillRequest.GetRequestType() == typeof(SessionEndedRequest);
        public static bool IsPermissionRequest(this SkillRequest skillRequest) => skillRequest.GetRequestType() == typeof(PermissionSkillEventRequest);
    }
}
