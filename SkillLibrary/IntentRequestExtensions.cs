using Alexa.NET.Request.Type;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkillLibrary
{
    public static class IntentRequestExtensions
    {
        public static readonly string GetGroceries = "GetGroceryItemsIntent";
        public static readonly string AddGroceries = "AddGroceryItemIntent";
        public static readonly string FinishedShopping = "ArchiveAllCardsIntent";

        public static bool IsGetGroceries(this IntentRequest intentRequest) => intentRequest.Intent.Name.Equals(GetGroceries);
        public static bool IsAddGroceries(this IntentRequest intentRequest) => intentRequest.Intent.Name.Equals(AddGroceries);
        public static bool IsFinishedShopping(this IntentRequest intentRequest) => intentRequest.Intent.Name.Equals(FinishedShopping);
    }
}
