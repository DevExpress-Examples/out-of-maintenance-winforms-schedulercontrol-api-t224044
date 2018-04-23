Imports DevExpress.XtraScheduler
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SchedulerAPISample.CodeExamples
	Friend Class TimeRuler
		Private Shared Sub TimeMarkerAction(ByVal scheduler As SchedulerControl)
'			#Region "#TimeMarker"
			scheduler.DayView.TimeRulers.Add(New DevExpress.XtraScheduler.TimeRuler())

			' Display the time marker if the view contains a current date.
			scheduler.DayView.TimeMarkerVisibility = TimeMarkerVisibility.TodayView
			' Display the time indicator in the current date's column only.
			scheduler.DayView.TimeIndicatorDisplayOptions.Visibility = TimeIndicatorVisibility.CurrentDate
			' Show the time indicator on top when it overlaps an appointment.
			scheduler.DayView.TimeIndicatorDisplayOptions.ShowOverAppointment = True
			' Hide the time marker in the second time ruler.
			scheduler.DayView.TimeRulers(1).TimeMarkerVisibility = TimeMarkerVisibility.Never

			scheduler.ActiveViewType = SchedulerViewType.Day
			scheduler.DayView.DayCount = 3
			scheduler.DayView.TopRowTime = Date.Now.AddHours(-1).TimeOfDay
			scheduler.Start = Date.Today.AddDays(-1)
'			#End Region ' #TimeMarker
		End Sub
	End Class
End Namespace
