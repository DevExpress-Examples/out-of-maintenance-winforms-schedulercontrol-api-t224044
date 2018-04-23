Imports DevExpress.XtraScheduler
Imports System.Drawing

Namespace SchedulerAPISample.CodeExamples
    Friend Class AppointmentViewInfoCustomizingSamples
        Private Shared Sub AppointmentAppearance(ByVal scheduler As SchedulerControl)
            '			#Region "#AppointmentAppearance"
            AddHandler scheduler.AppointmentViewInfoCustomizing, AddressOf scheduler_AppointmentViewInfoCustomizing
            scheduler.ActiveView.LayoutChanged()
            '			#End Region ' #AppointmentAppearance
        End Sub

#Region "#@AppointmentAppearance"
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
#End Region ' #@AppointmentAppearance

        Private Shared Sub StatusDisplayType(ByVal scheduler As SchedulerControl)
            '			#Region "#StatusDisplayType"
            Dim apt As Appointment = scheduler.ActiveView.GetAppointments()(0)
            apt.End = apt.End.AddDays(1)
            apt.Description = "Time"
            scheduler.ActiveViewType = SchedulerViewType.Day
            scheduler.DayView.AppointmentDisplayOptions.AllDayAppointmentsStatusDisplayType = AppointmentStatusDisplayType.Bounds
            scheduler.DayView.AppointmentDisplayOptions.ShowAllDayAppointmentStatusVertically = False
            AddHandler scheduler.AppointmentViewInfoCustomizing, AddressOf scheduler_AppointmentViewInfoCustomizing_1
            scheduler.ActiveView.LayoutChanged()
            '			#End Region ' #StatusDisplayType
        End Sub
#Region "#@StatusDisplayType"
        Public Shared Sub scheduler_AppointmentViewInfoCustomizing_1(ByVal sender As Object, ByVal e As AppointmentViewInfoCustomizingEventArgs)
            If e.ViewInfo.Description.Contains("Time") Then
                e.ViewInfo.StatusDisplayType = AppointmentStatusDisplayType.Time
            End If
        End Sub
#End Region ' #@StatusDisplayType
    End Class

End Namespace
