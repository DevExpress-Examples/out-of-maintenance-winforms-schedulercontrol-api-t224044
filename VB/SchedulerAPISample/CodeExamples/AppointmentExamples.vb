Imports DevExpress.XtraScheduler
Imports System

Namespace SchedulerAPISample.CodeExamples
    Friend Class AppointmentExamples

        Private Shared Sub CreateAppointmentFromSelection(ByVal scheduler As SchedulerControl)
            '			#Region "#CreateAppointmentFromSelection"
            ' Delete all appointments.
            scheduler.Storage.Appointments.Clear()
            ' Select time interval
            scheduler.ActiveView.SetSelection(New TimeInterval(Date.Now, New TimeSpan(2, 40, 0)), scheduler.ActiveView.GetResources()(1))
            ' Group by resource.
            scheduler.GroupType = SchedulerGroupType.Resource

            ' Create a new appointment.
            Dim apt As Appointment = scheduler.Storage.CreateAppointment(AppointmentType.Normal)
            ' Set the appointment's time interval to the selected time interval.
            apt.Start = scheduler.SelectedInterval.Start
            apt.End = scheduler.SelectedInterval.End
            ' Set the appointment's resource to the resource which contains
            ' the currently selected time interval.
            apt.ResourceId = scheduler.SelectedResource.Id
            ' Add the new appointment to the appointment collection.
            scheduler.Storage.Appointments.Add(apt)
            '			#End Region ' #CreateAppointmentFromSelection

        End Sub
    End Class
End Namespace
