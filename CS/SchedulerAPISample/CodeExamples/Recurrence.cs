using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraScheduler.Services;
using System;

namespace SchedulerAPISample.CodeExamples {
    class Recurrence {
        static void RecurrenceRuleAction(SchedulerControl scheduler) {
            #region #RecurrenceRule
            scheduler.DataStorage.Appointments.Clear();
            Appointment apt = scheduler.DataStorage.CreateAppointment(AppointmentType.Pattern);
            apt.Start = DateTime.Today.AddHours(3);
            apt.End = apt.Start.AddHours(2);
            apt.Subject = "TEST";

            apt.RecurrenceInfo.Type = RecurrenceType.Weekly;
            apt.RecurrenceInfo.Start = apt.Start;
            apt.RecurrenceInfo.Periodicity = 2;
            apt.RecurrenceInfo.WeekDays = WeekDays.Monday | WeekDays.Wednesday;
            apt.RecurrenceInfo.Range = RecurrenceRange.OccurrenceCount;
            apt.RecurrenceInfo.OccurrenceCount = 15;

            string s = DevExpress.XtraScheduler.iCalendar.iCalendarHelper.ExtractRecurrenceRule(apt.RecurrenceInfo);
            apt.Description = "RRULE:" + s + Environment.NewLine;
            scheduler.DataStorage.Appointments.Add(apt);
            apt.Description += apt.RecurrenceInfo.ToXml();
            #endregion #RecurrenceRule
        }

        static void RecurrenceFromXmlAction(SchedulerControl scheduler) {
            #region #RecurrenceFromXml
            string head = "<RecurrenceInfo ";
            string startText = String.Format("Start = '{0}' " , DateTime.Today.AddHours(3));
            string endText = String.Format("End = '{0}' ", DateTime.Today.AddHours(4));
            string weekDays = String.Format("Weekdays='{0}' ", 10);
            string id = String.Format("Id = '{0}' ", Guid.NewGuid());
            string occurrenceCount = String.Format("OccurrenceCount = '{0}' ", 15);
            string periodicity = String.Format("Periodicity = '{0}' ", 2);
            string range = String.Format("Range = '{0}' ", 1);
            string type = String.Format("Type = '{0}' ", 1);
            string version = String.Format("Version = '{0}' ", 1);
            string tail = " />";
            string recurrenceXmlString = (head + startText + endText + weekDays +
                id + occurrenceCount + periodicity + range + type + version + tail).Replace("'", "\"");
            scheduler.DataStorage.Appointments.Clear();
            Appointment apt = scheduler.DataStorage.CreateAppointment(AppointmentType.Pattern);
            apt.Start = DateTime.Today.AddHours(3);
            apt.End = apt.Start.AddHours(2);
            apt.Subject = "Recurrence From XML";

            // Set appointment recurrence from XML.
            apt.RecurrenceInfo.FromXml(recurrenceXmlString);
            // Get recurrence info from XML.
            IRecurrenceInfo rec = DevExpress.XtraScheduler.Xml.RecurrenceInfoXmlPersistenceHelper.ObjectFromXml(recurrenceXmlString);

            apt.Description = recurrenceXmlString + Environment.NewLine + String.Format("Type: {0}",rec.Type);
            scheduler.DataStorage.Appointments.Add(apt);
            #endregion #RecurrenceFromXml
        }
    }
}
