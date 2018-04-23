Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports DevExpress.XtraScheduler.Services
Imports System

Namespace SchedulerAPISample.CodeExamples
    Friend Class Recurrence
        Private Shared Sub WeeklyRecurrenceAction(ByVal scheduler As SchedulerControl)
'            #Region "#WeeklyRecurrence"
            scheduler.Storage.Appointments.Clear()
            Dim apt As Appointment = scheduler.Storage.CreateAppointment(AppointmentType.Pattern)
            apt.Start = Date.Today.AddHours(3)
            apt.End = apt.Start.AddHours(2)
            apt.Subject = "My Subject"
            apt.Location = "My Location"
            apt.Description = "My Description"

            apt.RecurrenceInfo.Type = RecurrenceType.Weekly
            apt.RecurrenceInfo.Start = apt.Start
            apt.RecurrenceInfo.Periodicity = 2
            apt.RecurrenceInfo.WeekDays = WeekDays.Monday Or WeekDays.Wednesday
            apt.RecurrenceInfo.Range = RecurrenceRange.OccurrenceCount
            apt.RecurrenceInfo.OccurrenceCount = 15

            Dim s As String = DevExpress.XtraScheduler.iCalendar.iCalendarHelper.ExtractRecurrenceRule(apt.RecurrenceInfo)
            apt.Description = "RRULE:" & s
            scheduler.Storage.Appointments.Add(apt)
            apt.Description += apt.RecurrenceInfo.ToXml()
'            #End Region ' #WeeklyRecurrence
        End Sub
    End Class
End Namespace
