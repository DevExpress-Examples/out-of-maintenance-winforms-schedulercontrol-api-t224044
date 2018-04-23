Imports DevExpress.XtraScheduler
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SchedulerAPISample.CodeExamples
	Friend Class AppointmentAppearance
		Private Shared Sub AppointmentViewInfoCustomizingEvent(ByVal scheduler As SchedulerControl)
'			#Region "#AppointmentViewInfoCustomizingEvent"
			AddHandler scheduler.AppointmentViewInfoCustomizing, AddressOf scheduler_AppointmentViewInfoCustomizing
			scheduler.ActiveView.LayoutChanged()
'			#End Region ' #AppointmentViewInfoCustomizingEvent
		End Sub

		#Region "#@AppointmentViewInfoCustomizingEvent"
			Public Shared Sub scheduler_AppointmentViewInfoCustomizing(ByVal sender As Object, ByVal e As AppointmentViewInfoCustomizingEventArgs)
				Select Case e.ViewInfo.Status.Type
					Case AppointmentStatusType.Busy
						e.ViewInfo.Appearance.ForeColor = Color.Red
					Case AppointmentStatusType.Free
						e.ViewInfo.Appearance.ForeColor = Color.Blue
					Case AppointmentStatusType.OutOfOffice
						e.ViewInfo.Appearance.ForeColor = Color.Green
				End Select
			End Sub
		#End Region ' #@AppointmentViewInfoCustomizingEvent
	End Class
End Namespace
