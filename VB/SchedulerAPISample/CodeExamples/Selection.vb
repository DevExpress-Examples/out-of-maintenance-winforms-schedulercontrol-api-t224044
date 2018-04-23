Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports System
Imports System.Drawing

Namespace SchedulerAPISample.CodeExamples
    Friend Class Selection

        Private Shared Sub SelectIntervalWithResourceUnspecified(ByVal scheduler As SchedulerControl)
'            #Region "#SelectIntervalWithResourceUnspecified"
            AddHandler scheduler.CustomDrawTimeCell, AddressOf scheduler_CustomDrawTimeCell_1
            'scheduler.Storage.Appointments.Clear();
            scheduler.ActiveView.SetSelection(New TimeInterval(Date.Now, New TimeSpan(2, 40, 0)), ResourceEmpty.Resource)
'            #End Region ' #SelectIntervalWithResourceUnspecified
        End Sub

        #Region "#@SelectIntervalWithResourceUnspecified"
        Public Shared Sub scheduler_CustomDrawTimeCell_1(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.CustomDrawObjectEventArgs)
            Dim solidBrush As Brush = New SolidBrush(Color.Lime)
            Dim cell As SelectableIntervalViewInfo = TryCast(e.ObjectInfo, SelectableIntervalViewInfo)
            If cell IsNot Nothing Then
                If cell.Selected Then
                    e.Graphics.FillRectangle(solidBrush, e.Bounds)
                    e.Handled = True
                End If
            End If
        End Sub
        #End Region ' #@SelectIntervalWithResourceUnspecified

        Private Shared Sub SelectIntervalWithSpecifiedResource(ByVal scheduler As SchedulerControl)
'            #Region "#SelectIntervalWithSpecifiedResource"
            AddHandler scheduler.CustomDrawTimeCell, AddressOf scheduler_CustomDrawTimeCell_2
            'scheduler.Storage.Appointments.Clear();
            scheduler.ActiveView.GroupType = SchedulerGroupType.Resource
            scheduler.ActiveView.SetSelection(New TimeInterval(Date.Now, New TimeSpan(2, 40, 0)), scheduler.ActiveView.GetResources()(1))
'            #End Region ' #SelectIntervalWithSpecifiedResource
        End Sub

        #Region "#@SelectIntervalWithSpecifiedResource"
        Public Shared Sub scheduler_CustomDrawTimeCell_2(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.CustomDrawObjectEventArgs)
            Dim solidBrush As Brush = New SolidBrush(Color.Lime)
            Dim cell As SelectableIntervalViewInfo = TryCast(e.ObjectInfo, SelectableIntervalViewInfo)
            If cell IsNot Nothing Then
                If cell.Selected Then
                    e.Graphics.FillRectangle(solidBrush, e.Bounds)
                    e.Handled = True
                End If
            End If
        End Sub
        #End Region ' #@SelectIntervalWithSpecifiedResource

        Private Shared Sub SelectIntervalUsingService(ByVal scheduler As SchedulerControl)
'            #Region "#SelectIntervalUsingService"
            AddHandler scheduler.CustomDrawTimeCell, AddressOf scheduler_CustomDrawTimeCell_3
            scheduler.Services.Selection.SelectedInterval = New TimeInterval(Date.Now, New TimeSpan(2, 40, 0))
'            #End Region ' #SelectIntervalUsingService
        End Sub

        #Region "#@SelectIntervalUsingService"
        Public Shared Sub scheduler_CustomDrawTimeCell_3(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.CustomDrawObjectEventArgs)
            Dim solidBrush As Brush = New SolidBrush(Color.Lime)
            Dim cell As SelectableIntervalViewInfo = TryCast(e.ObjectInfo, SelectableIntervalViewInfo)
            If cell IsNot Nothing Then
                If cell.Selected Then
                    e.Graphics.FillRectangle(solidBrush, e.Bounds)
                    e.Handled = True
                End If
            End If
        End Sub
        #End Region ' #@SelectIntervalUsingService


        Private Shared Sub SelectSingleAppointment(ByVal scheduler As SchedulerControl)
'            #Region "#SelectSingleAppointment"
            AddHandler scheduler.CustomDrawAppointmentBackground, AddressOf scheduler_CustomDrawAppointmentBackground_1
            scheduler.ActiveView.GroupType = SchedulerGroupType.Resource
            scheduler.ActiveView.ChangeAppointmentSelection(scheduler.ActiveView.GetAppointments()(1))
'            #End Region ' #SelectSingleAppointment
        End Sub
        #Region "#@SelectSingleAppointment"
        Public Shared Sub scheduler_CustomDrawAppointmentBackground_1(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            Dim aptViewInfo As AppointmentViewInfo = TryCast(e.ObjectInfo, AppointmentViewInfo)
            If aptViewInfo Is Nothing Then
                Return
            End If
            If aptViewInfo.Selected Then
                e.DrawDefault()
                Dim r As Rectangle = e.Bounds
                Dim brRect As Brush = aptViewInfo.Status.GetBrush()
                e.Graphics.DrawRectangle(New Pen(Color.Lime, 4), r)
                e.Handled = True
            End If
        End Sub
        #End Region ' #@SelectSingleAppointment


        Private Shared Sub SelectMultipleAppointments(ByVal scheduler As SchedulerControl)
'            #Region "#SelectMultipleAppointments"
            AddHandler scheduler.CustomDrawAppointmentBackground, AddressOf scheduler_CustomDrawAppointmentBackground_2
            scheduler.ActiveView.GroupType = SchedulerGroupType.Resource
            scheduler.ActiveView.AddAppointmentSelection(scheduler.ActiveView.GetAppointments()(1))
            scheduler.ActiveView.AddAppointmentSelection(scheduler.ActiveView.GetAppointments()(2))
            scheduler.ActiveView.AddAppointmentSelection(scheduler.ActiveView.GetAppointments()(3))
'            #End Region ' #SelectMultipleAppointments
        End Sub
        #Region "#@SelectMultipleAppointments"
        Public Shared Sub scheduler_CustomDrawAppointmentBackground_2(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            Dim aptViewInfo As AppointmentViewInfo = TryCast(e.ObjectInfo, AppointmentViewInfo)
            If aptViewInfo Is Nothing Then
                Return
            End If
            If aptViewInfo.Selected Then
                e.DrawDefault()
                Dim r As Rectangle = e.Bounds
                Dim brRect As Brush = aptViewInfo.Status.GetBrush()
                e.Graphics.DrawRectangle(New Pen(Color.Lime, 4), r)
                e.Handled = True
            End If
        End Sub
        #End Region ' #@SelectMultipleAppointments

        Private Shared Sub SelectMultipleAppointmentsUsingService(ByVal scheduler As SchedulerControl)
'            #Region "#SelectAppointmentsUsingService"
            AddHandler scheduler.CustomDrawAppointmentBackground, AddressOf scheduler_CustomDrawAppointmentBackground_3
            scheduler.ActiveView.GroupType = SchedulerGroupType.Resource
            scheduler.Services.AppointmentSelection.SetSelectedAppointments(scheduler.ActiveView.GetAppointments())
'            #End Region ' #SelectAppointmentsUsingService
        End Sub
        #Region "#@SelectAppointmentsUsingService"
        Public Shared Sub scheduler_CustomDrawAppointmentBackground_3(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            Dim aptViewInfo As AppointmentViewInfo = TryCast(e.ObjectInfo, AppointmentViewInfo)
            If aptViewInfo Is Nothing Then
                Return
            End If
            If aptViewInfo.Selected Then
                e.DrawDefault()
                Dim r As Rectangle = e.Bounds
                Dim brRect As Brush = aptViewInfo.Status.GetBrush()
                e.Graphics.DrawRectangle(New Pen(Color.Lime, 4), r)
                e.Handled = True
            End If
        End Sub
        #End Region ' #@SelectAppointmentsUsingService
    End Class
End Namespace
