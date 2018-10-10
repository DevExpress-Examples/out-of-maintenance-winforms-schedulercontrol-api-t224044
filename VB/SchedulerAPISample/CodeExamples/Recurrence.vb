Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports DevExpress.XtraScheduler.Services
Imports System

Namespace SchedulerAPISample.CodeExamples
	Friend Class Recurrence
		Private Shared Sub RecurrenceRuleAction(ByVal scheduler As SchedulerControl)
'			#Region "#RecurrenceRule"
			scheduler.DataStorage.Appointments.Clear()
			Dim apt As Appointment = scheduler.DataStorage.CreateAppointment(AppointmentType.Pattern)
			apt.Start = Date.Today.AddHours(3)
			apt.End = apt.Start.AddHours(2)
			apt.Subject = "TEST"

			apt.RecurrenceInfo.Type = RecurrenceType.Weekly
			apt.RecurrenceInfo.Start = apt.Start
			apt.RecurrenceInfo.Periodicity = 2
			apt.RecurrenceInfo.WeekDays = WeekDays.Monday Or WeekDays.Wednesday
			apt.RecurrenceInfo.Range = RecurrenceRange.OccurrenceCount
			apt.RecurrenceInfo.OccurrenceCount = 15

			Dim s As String = DevExpress.XtraScheduler.iCalendar.iCalendarHelper.ExtractRecurrenceRule(apt.RecurrenceInfo)
			apt.Description = "RRULE:" & s & Environment.NewLine
			scheduler.DataStorage.Appointments.Add(apt)
			apt.Description += apt.RecurrenceInfo.ToXml()
'			#End Region ' #RecurrenceRule
		End Sub

		Shared Sub RecurrenceFromXmlAction(ByVal scheduler As SchedulerControl)
'			#Region "#RecurrenceFromXml"
			Dim head As String = "<RecurrenceInfo "
			Dim startText As String = String.Format("Start = '{0}' ", Date.Today.AddHours(3))
			Dim endText As String = String.Format("End = '{0}' ", Date.Today.AddHours(4))
			Dim weekDays As String = String.Format("Weekdays='{0}' ", 10)
			Dim id As String = String.Format("Id = '{0}' ", Guid.NewGuid())
			Dim occurrenceCount As String = String.Format("OccurrenceCount = '{0}' ", 15)
			Dim periodicity As String = String.Format("Periodicity = '{0}' ", 2)
			Dim range As String = String.Format("Range = '{0}' ", 1)
			Dim type As String = String.Format("Type = '{0}' ", 1)
			Dim version As String = String.Format("Version = '{0}' ", 1)
			Dim tail As String = " />"
			Dim recurrenceXmlString As String = (head & startText & endText & weekDays & id & occurrenceCount & periodicity & range & type & version & tail).Replace("'", """")
			scheduler.DataStorage.Appointments.Clear()
			Dim apt As Appointment = scheduler.DataStorage.CreateAppointment(AppointmentType.Pattern)
			apt.Start = Date.Today.AddHours(3)
			apt.End = apt.Start.AddHours(2)
			apt.Subject = "Recurrence From XML"

			' Set appointment recurrence from XML.
			apt.RecurrenceInfo.FromXml(recurrenceXmlString)
			' Get recurrence info from XML.
			Dim rec As IRecurrenceInfo = DevExpress.XtraScheduler.Xml.RecurrenceInfoXmlPersistenceHelper.ObjectFromXml(recurrenceXmlString)

			apt.Description = recurrenceXmlString & Environment.NewLine & String.Format("Type: {0}",rec.Type)
			scheduler.DataStorage.Appointments.Add(apt)
'			#End Region ' #RecurrenceFromXml
		End Sub

	End Class

End Namespace
