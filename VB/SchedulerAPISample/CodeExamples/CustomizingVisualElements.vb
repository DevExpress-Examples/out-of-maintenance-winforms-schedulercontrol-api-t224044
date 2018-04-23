Imports Microsoft.VisualBasic
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports System
Imports System.Drawing
Imports System.Linq

Namespace SchedulerAPISample.CodeExamples
    Friend Class CustomizingVisualElements

        Private Shared Sub InitAppointmentDisplayTextEvent(ByVal scheduler As SchedulerControl)
            '			#Region "#InitAppointmentDisplayTextEvent"
            AddHandler scheduler.InitAppointmentDisplayText, AddressOf scheduler_InitAppointmentDisplayText
            scheduler.ActiveView.LayoutChanged()
            '			#End Region ' #InitAppointmentDisplayTextEvent
        End Sub

#Region "#@InitAppointmentDisplayTextEvent"
        Public Shared Sub scheduler_InitAppointmentDisplayText(ByVal sender As Object, ByVal e As AppointmentDisplayTextEventArgs)
            ' Display custom text in Day and WorkWeek views only (VerticalAppointmentViewInfo).
            If TypeOf e.ViewInfo Is VerticalAppointmentViewInfo Then
                e.Text = e.Appointment.Subject & ControlChars.CrLf
                e.Text &= "------" & ControlChars.CrLf
                If e.Description <> String.Empty Then
                    e.Description = String.Empty
                    e.Text &= "Description is hidden"
                End If
            End If
        End Sub
#End Region ' #@InitAppointmentDisplayTextEvent

        Private Shared Sub InitAppointmentImagesEvent(ByVal scheduler As SchedulerControl)
            '			#Region "#InitAppointmentImagesEvent"
            AddHandler scheduler.InitAppointmentImages, AddressOf scheduler_InitAppointmentImages
            scheduler.ActiveView.LayoutChanged()
            '			#End Region ' #InitAppointmentImagesEvent
        End Sub

#Region "#@InitAppointmentImagesEvent"
        Public Shared Sub scheduler_InitAppointmentImages(ByVal sender As Object, ByVal e As AppointmentImagesEventArgs)
            Dim info As New AppointmentImageInfo()
            info.Image = Image.FromFile("image.png")
            e.ImageInfoList.Add(info)
        End Sub
#End Region ' #@InitAppointmentImagesEvent


        Private Shared Sub LayoutViewInfoCustomizingEvent(ByVal scheduler As SchedulerControl)
            '			#Region "#LayoutViewInfoCustomizingEvent"
            AddHandler scheduler.LayoutViewInfoCustomizing, AddressOf scheduler_LayoutViewInfoCustomizing

            scheduler.ActiveView.LayoutChanged()
            '			#End Region ' #LayoutViewInfoCustomizingEvent
        End Sub

#Region "#@LayoutViewInfoCustomizingEvent"
        Public Shared Sub scheduler_LayoutViewInfoCustomizing(ByVal sender As Object, ByVal e As LayoutViewInfoCustomizingEventArgs)
            Dim s As String = e.ViewInfo.GetType().ToString().Substring("DevExpress.XtraScheduler.Drawing.".Length)
            If e.Kind = LayoutElementKind.DateHeader Then
                Dim header As SchedulerHeader = TryCast(e.ViewInfo, SchedulerHeader)
                If header IsNot Nothing Then
                    header.Caption = s
                End If
            End If
            If e.Kind = LayoutElementKind.Cell Then
                Dim cell As SchedulerViewCellBase = TryCast(e.ViewInfo, SchedulerViewCellBase)
                If cell IsNot Nothing Then
                    cell.Appearance.BackColor = Color.LightYellow
                End If
                Dim cellWeek As SingleWeekCellBase = TryCast(e.ViewInfo, SingleWeekCellBase)
                If cellWeek IsNot Nothing Then
                    cellWeek.Appearance.BackColor = Color.LightCyan
                    cellWeek.Header.Caption = s
                End If
            End If
        End Sub
#End Region ' #@LayoutViewInfoCustomizingEvent

        Private Shared Sub CustomizeDateNavigationBarCaptionEvent(ByVal scheduler As SchedulerControl)
            '			#Region "#CustomizeDateNavigationBarCaptionEvent"
            AddHandler scheduler.CustomizeDateNavigationBarCaption, AddressOf scheduler_CustomizeDateNavigationBarCaption
            scheduler.ActiveView.LayoutChanged()
            '			#End Region ' #CustomizeDateNavigationBarCaptionEvent
        End Sub

        Shared Sub CustomizingResourceHeaders(ByVal scheduler As SchedulerControl)
            '			#Region "#CustomizingResourceHeaders"
            scheduler.GroupType = SchedulerGroupType.Resource
            scheduler.ActiveViewType = SchedulerViewType.Timeline
            AddHandler scheduler.LayoutViewInfoCustomizing, AddressOf scheduler_LayoutViewInfoCustomizingResourceHeaders
            scheduler.ActiveView.LayoutChanged()
            '			#End Region ' #CustomizingResourceHeaders
        End Sub

#Region "#@CustomizingResourceHeaders"
        Public Shared Sub scheduler_LayoutViewInfoCustomizingResourceHeaders(ByVal sender As Object, ByVal e As LayoutViewInfoCustomizingEventArgs)
            If e.Kind = LayoutElementKind.ResourceHeader Then
                Dim header As ResourceHeader = TryCast(e.ViewInfo, ResourceHeader)
                header.Caption = header.Resource.Caption & ControlChars.CrLf & "New long text line"
                e.ShouldRecalculateLayout = True
            End If
        End Sub
#End Region ' #@CustomizingResourceHeaders


#Region "#@CustomizeDateNavigationBarCaptionEvent"
        Public Shared Sub scheduler_CustomizeDateNavigationBarCaption(ByVal sender As Object, ByVal e As CustomizeDateNavigationBarCaptionEventArgs)
            e.Caption = String.Format("Displaying dates from {0:D} to {1:D}", e.Interval.Start.Date, e.Interval.End.Date)
        End Sub
#End Region ' #@CustomizeDateNavigationBarCaptionEvent

        Shared Sub CustomizeMesssageBoxCaptionEvent(ByVal scheduler As SchedulerControl)
            '			#Region "#CustomizeMesssageBoxCaptionEvent"
            AddHandler scheduler.CustomizeMessageBoxCaption, AddressOf scheduler_CustomizeMessageBoxCaption
            scheduler.ActiveView.LayoutChanged()
            '			#End Region ' #CustomizeMesssageBoxCaptionEvent
        End Sub

#Region "#@CustomizeMesssageBoxCaptionEvent"
        Public Shared Sub scheduler_CustomizeMessageBoxCaption(ByVal sender As Object, ByVal e As CustomizeMessageBoxCaptionEventArgs)
            If e.CaptionId = DevExpress.XtraScheduler.Localization.SchedulerStringId.Msg_SaveBeforeClose Then
                e.Caption = "Appointment modification"
            End If
        End Sub
#End Region ' #@CustomizeMesssageBoxCaptionEvent


    End Class
End Namespace
