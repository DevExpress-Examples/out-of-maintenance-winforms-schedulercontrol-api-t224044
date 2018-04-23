Imports DevExpress.XtraScheduler
Imports System.Drawing

Namespace SchedulerAPISample.CodeExamples
    Friend Class AppointmentEvents
        Private Shared Sub AppointmentChangingEvent(ByVal scheduler As SchedulerControl)
'            #Region "#AppointmentChangingEvent"
            AddHandler scheduler.Storage.AppointmentChanging, AddressOf Storage_AppointmentChanging
            scheduler.ActiveView.LayoutChanged()
'            #End Region ' #AppointmentChangingEvent
        End Sub

        #Region "#@AppointmentChangingEvent"
        Private Shared Sub Storage_AppointmentChanging(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
            Dim busyKey As Object = DirectCast(sender, SchedulerStorage).Appointments.Statuses.GetByType(AppointmentStatusType.Busy).Id
            If busyKey IsNot Nothing Then
                e.Cancel = CType(e.Object, Appointment).StatusKey.Equals(busyKey)
            End If
        End Sub
        #End Region ' #@AppointmentChangingEvent
    End Class
End Namespace
