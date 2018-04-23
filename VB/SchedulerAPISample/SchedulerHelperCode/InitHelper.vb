Imports DevExpress.XtraScheduler
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SchedulerAPISample
    Public Class InitHelper
        Public Shared RandomInstance As New Random()

        Public Shared Sub InitResources(ByVal resources As BindingList(Of CustomResource))
            resources.Add(CreateCustomResource(1, "Peter Dolan", Color.PowderBlue))
            resources.Add(CreateCustomResource(2, "Ryan Fisher", Color.PaleVioletRed))
            resources.Add(CreateCustomResource(3, "Andrew Miller", Color.PeachPuff))
        End Sub

        Public Shared Function CreateCustomResource(ByVal res_id As Integer, ByVal caption As String, ByVal ResColor As Color) As CustomResource
            Dim cr As New CustomResource()
            cr.ResID = res_id
            cr.Name = caption
            Return cr
        End Function



        Public Shared Sub InitAppointments(ByVal appointments As BindingList(Of CustomAppointment), ByVal resources As BindingList(Of CustomResource))
            GenerateEvents(appointments, resources)
        End Sub


        Public Shared Sub GenerateEvents(ByVal eventList As BindingList(Of CustomAppointment), ByVal resources As BindingList(Of CustomResource))
            Dim count As Integer = resources.Count

            For i As Integer = 0 To count - 1
                Dim resource As CustomResource = resources(i)
                Dim subjPrefix As String = resource.Name & "'s "
                eventList.Add(CreateEvent(subjPrefix & "meeting", resource.ResID, 2, 5))
                eventList.Add(CreateEvent(subjPrefix & "travel", resource.ResID, 3, 6))
                eventList.Add(CreateEvent(subjPrefix & "special", resource.ResID, 0, 10))
            Next i
        End Sub
        Public Shared Function CreateEvent(ByVal subject As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer) As CustomAppointment
            Dim apt As New CustomAppointment()
            apt.Subject = subject
            apt.OwnerId = resourceId
            Dim rnd As Random = RandomInstance
            Dim rangeInMinutes As Integer = 60 * 24
            apt.StartTime = Date.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
            apt.EndTime = apt.StartTime.Add(TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4)))
            apt.Status = status
            apt.Label = label
            Return apt
        End Function
    End Class
End Namespace
