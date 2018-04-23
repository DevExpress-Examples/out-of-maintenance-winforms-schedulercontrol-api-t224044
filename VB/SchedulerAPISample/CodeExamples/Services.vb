Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports DevExpress.XtraScheduler.Services
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SchedulerAPISample.CodeExamples
    Friend Class Services
        Private Shared Sub AppointmentFormatStringServiceAction(ByVal scheduler As SchedulerControl)
'            #Region "#AppointmentFormatStringService"
            Dim oldService As IAppointmentFormatStringService = scheduler.GetService(Of IAppointmentFormatStringService)()
            If oldService IsNot Nothing Then
                Dim newService As New MyAppointmentFormatStringService(oldService)
                scheduler.RemoveService(GetType(IAppointmentFormatStringService))
                scheduler.AddService(GetType(IAppointmentFormatStringService), newService)
            End If
            scheduler.ActiveView.LayoutChanged()
'            #End Region ' #AppointmentFormatStringService
        End Sub

        #Region "#@AppointmentFormatStringService"
        Public Class MyAppointmentFormatStringService
            Inherits AppointmentFormatStringServiceWrapper

            Public Sub New(ByVal service As IAppointmentFormatStringService)
                MyBase.New(service)
            End Sub
            Public Overrides Function GetVerticalAppointmentStartFormat(ByVal aptViewInfo As IAppointmentViewInfo) As String
                Return "{0: HHmm:ss - 'VerticalAppointmentStart'}"
            End Function
            Public Overrides Function GetVerticalAppointmentEndFormat(ByVal aptViewInfo As IAppointmentViewInfo) As String
                Return "{0: HHmm:ss - 'VerticalAppointmentEnd'}"
            End Function
            Public Overrides Function GetHorizontalAppointmentEndFormat(ByVal aptViewInfo As IAppointmentViewInfo) As String
                Return "{0: HHmm - 'HorizontalAppointmentEnd'}"
            End Function
            Public Overrides Function GetHorizontalAppointmentStartFormat(ByVal aptViewInfo As IAppointmentViewInfo) As String
                Return "{0: HHmm - 'HorizontalAppointmentStart'}"
            End Function
            Public Overrides Function GetContinueItemStartFormat(ByVal aptViewInfo As IAppointmentViewInfo) As String
                Return "{0: HHmm MMM dd - 'ContinueItemStart'}"
            End Function
            Public Overrides Function GetContinueItemEndFormat(ByVal aptViewInfo As IAppointmentViewInfo) As String
                Return "{0: HHmm MMM dd - 'ContinueItemEnd'}"
            End Function
        End Class
        #End Region ' #@AppointmentFormatStringService
    End Class
End Namespace
