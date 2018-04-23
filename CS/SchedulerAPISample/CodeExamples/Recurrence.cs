using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraScheduler.Services;
using System;

namespace SchedulerAPISample.CodeExamples {
    class Recurrence {
        static void WeeklyRecurrenceAction(SchedulerControl scheduler) {
            #region #WeeklyRecurrence
            scheduler.Storage.Appointments.Clear();
            Appointment apt = scheduler.Storage.CreateAppointment(AppointmentType.Pattern);
            apt.Start = DateTime.Today.AddHours(3);
            apt.End = apt.Start.AddMinutes(15);
            apt.Subject = "My Subject";
            apt.Location = "My Location";
            apt.Description = "My Description";

            apt.RecurrenceInfo.Type = RecurrenceType.Weekly;
            apt.RecurrenceInfo.Start = apt.Start;
            apt.RecurrenceInfo.Periodicity = 2;
            apt.RecurrenceInfo.WeekDays = WeekDays.Monday | WeekDays.Wednesday;
            apt.RecurrenceInfo.Range = RecurrenceRange.OccurrenceCount;
            apt.RecurrenceInfo.OccurrenceCount = 15;

            string s = DevExpress.XtraScheduler.iCalendar.iCalendarHelper.ExtractRecurrenceRule(apt.RecurrenceInfo);
            apt.Description = "RRULE:" + s;
            scheduler.Storage.Appointments.Add(apt);
            apt.Description += apt.RecurrenceInfo.ToXml();
            #endregion #WeeklyRecurrence
        }
    }
}
