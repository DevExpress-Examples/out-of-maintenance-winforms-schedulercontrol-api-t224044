Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports System
Imports System.Drawing
Imports System.Linq

Namespace SchedulerAPISample.CodeExamples
    Friend Class ViewInfoCustomization

        Private Shared Sub LayoutViewInfoCustomizingEvent(ByVal scheduler As SchedulerControl)
'            #Region "#LayoutViewInfoCustomizingEvent"
            AddHandler scheduler.LayoutViewInfoCustomizing, AddressOf scheduler_LayoutViewInfoCustomizing

            scheduler.ActiveView.LayoutChanged()
'            #End Region ' #LayoutViewInfoCustomizingEvent
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

    End Class
End Namespace
