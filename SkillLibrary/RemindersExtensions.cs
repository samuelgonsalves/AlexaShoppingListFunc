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

        public static async Task SetAWeeklyReminder(SkillRequest input)
        {
            var reminder = new Reminder
            {
                RequestTime = DateTime.UtcNow,
                AlertInformation = new AlertInformation(new[] { new SpokenContent("Get your groceries!", "en-GB") }),
                Trigger = new AbsoluteTrigger(Convert.ToDateTime(DateTime.Now.AddMinutes(1).ToString("yyyy-MM-ddTHH:mm:ss.fff")),
                                              new Recurrence("WEEKLY", new List<string> { "SA" })),
                PushNotification = PushNotification.Enabled
            };

            await CreateReminderAsync(input, reminder);
        }

        private static async Task<ReminderChangedResponse> CreateReminderAsync(SkillRequest skillRequest, Reminder reminder)
        {
            var client = new RemindersClient(skillRequest);

            return await client.Create(reminder);
        }
    }
}
