Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports DevExpress.XtraScheduler.Native
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq

Namespace SchedulerAPISample.CodeExamples
    Friend Class AppointmentConflicts
        Private Shared Sub AllowAppointmentConflictsEvent(ByVal scheduler As SchedulerControl)
'            #Region "#AllowAppointmentConflictsEvent"
            ' Concurrent appointments with the same resource are not allowed.
            scheduler.OptionsCustomization.AllowAppointmentConflicts = AppointmentConflictsMode.Custom
            AddHandler scheduler.AllowAppointmentConflicts, AddressOf Scheduler_AllowAppointmentConflicts

            scheduler.DataStorage.Appointments.Clear()
            scheduler.GroupType = SchedulerGroupType.Resource
            Dim apt1 As Appointment = scheduler.DataStorage.Appointments.CreateAppointment(AppointmentType.Normal, Date.Now, Date.Now.AddHours(2))
            apt1.ResourceId = scheduler.DataStorage.Resources(0).Id
            scheduler.DataStorage.Appointments.Add(apt1)
            Dim apt2 As Appointment = scheduler.DataStorage.Appointments.CreateAppointment(AppointmentType.Normal, Date.Now, Date.Now.AddHours(2))
            apt2.ResourceId = scheduler.DataStorage.Resources(1).Id
            scheduler.DataStorage.Appointments.Add(apt2)
            '            #End Region ' #AllowAppointmentConflictsEvent
        End Sub
#Region "#@AllowAppointmentConflictsEvent"
        Private Shared Sub Scheduler_AllowAppointmentConflicts(ByVal sender As Object, ByVal e As AppointmentConflictEventArgs)
            e.Conflicts.Clear()
            FillConflictedAppointmentsCollection(e.Conflicts, e.Interval, DirectCast(sender, SchedulerControl).Storage.Appointments.Items, e.AppointmentClone)
        End Sub
        Private Shared Sub FillConflictedAppointmentsCollection(ByVal conflicts As AppointmentBaseCollection, ByVal interval As TimeInterval, ByVal collection As AppointmentBaseCollection, ByVal currApt As Appointment)
            For i As Integer = 0 To collection.Count - 1
                Dim apt As Appointment = collection(i)
                If (New TimeInterval(apt.Start, apt.End)).IntersectsWith(interval) And Not (apt.Start = interval.End OrElse apt.End = interval.Start) Then
                    If apt.ResourceId Is currApt.ResourceId Then
                        conflicts.Add(apt)
                    End If
                End If
                If apt.Type = AppointmentType.Pattern Then
                    FillConflictedAppointmentsCollection(conflicts, interval, apt.GetExceptions(), currApt)
                End If
            Next i
        End Sub
#End Region ' #@AllowAppointmentConflictsEvent

        Private Shared Sub ConflictCalculator(ByVal scheduler As SchedulerControl)
            '            #Region "#ConflictCalculator"
            AddHandler scheduler.CustomDrawAppointmentBackground, AddressOf scheduler_CustomDrawAppointmentBackground
            scheduler.ActiveView.LayoutChanged()
            '            #End Region ' #ConflictCalculator
        End Sub

#Region "#@ConflictCalculator"
        Private Shared Sub scheduler_CustomDrawAppointmentBackground(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            Dim scheduler As SchedulerControl = TryCast(sender, SchedulerControl)
            Dim viewInfo As AppointmentViewInfo = (TryCast(e.ObjectInfo, DevExpress.XtraScheduler.Drawing.AppointmentViewInfo))
            Dim apt As Appointment = viewInfo.Appointment
            Dim allAppointments As AppointmentBaseCollection = scheduler.ActiveView.GetAppointments()
            Dim aCalculator As New AppointmentConflictsCalculator(allAppointments)
            Dim visibleInterval As TimeInterval = scheduler.ActiveView.GetVisibleIntervals().Interval
            Dim isConflict As Boolean = aCalculator.CalculateConflicts(apt, visibleInterval).Count <> 0
            ' Paint conflict appointments with a red and white hatch brush.
            If isConflict Then
                Dim rect As Rectangle = e.Bounds
                Dim brush As Brush = e.Cache.GetSolidBrush(scheduler.DataStorage.Appointments.Labels.GetById(apt.LabelKey).GetColor())
                e.Graphics.FillRectangle(brush, rect)
                rect.Inflate(-3, -3)
                Dim hatchBrush As New HatchBrush(HatchStyle.WideUpwardDiagonal, Color.Red, Color.White)
                e.Graphics.FillRectangle(hatchBrush, rect)
                e.Handled = True
            End If
        End Sub
        #End Region ' #@ConflictCalculator
    End Class
End Namespace
