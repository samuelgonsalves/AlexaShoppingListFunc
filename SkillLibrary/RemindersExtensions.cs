using Alexa.NET.Reminders;
using Alexa.NET.Request;
using Alexa.NET.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillLibrary
{
    public static class RemindersExtensions
    {
        public static async Task<bool> AnyActiveRemindersAsync(SkillRequest skillRequest)
        {
            var client = new RemindersClient(skillRequest);

            return (await client.Get())?.Alerts?.Any(a => a.Status == ReminderStatus.On) ?? false;
        }

        public static async Task<ReminderChangedResponse> CreateReminder(SkillRequest skillRequest, Reminder reminder)
        {
            var client = new RemindersClient(skillRequest);

            return await client.Create(reminder);
        }
    }
}
