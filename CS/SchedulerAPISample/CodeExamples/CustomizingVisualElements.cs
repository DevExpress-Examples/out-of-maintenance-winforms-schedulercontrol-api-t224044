using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using System;
using System.Drawing;
using System.Linq;

namespace SchedulerAPISample.CodeExamples {
    class CustomizingVisualElements {

        static void InitAppointmentDisplayTextEvent(SchedulerControl scheduler) {
            #region #InitAppointmentDisplayTextEvent
            scheduler.InitAppointmentDisplayText += scheduler_InitAppointmentDisplayText;
            scheduler.ActiveView.LayoutChanged();
            #endregion #InitAppointmentDisplayTextEvent
        }

        #region #@InitAppointmentDisplayTextEvent
        public static void scheduler_InitAppointmentDisplayText(object sender, AppointmentDisplayTextEventArgs e) {
            // Display custom text in Day and WorkWeek views only (VerticalAppointmentViewInfo).
            if (e.ViewInfo is VerticalAppointmentViewInfo) {
                e.Text = e.Appointment.Subject + "\r\n";
                e.Text += "------\r\n";
                if (e.Description != String.Empty) {
                    e.Description = string.Empty;
                    e.Text += "Description is hidden";
                }
            }
        }
        #endregion #@InitAppointmentDisplayTextEvent

        static void InitAppointmentImagesEvent(SchedulerControl scheduler) {
            #region #InitAppointmentImagesEvent
            scheduler.InitAppointmentImages += scheduler_InitAppointmentImages;
            scheduler.ActiveView.LayoutChanged();
            #endregion #InitAppointmentImagesEvent
        }

        #region #@InitAppointmentImagesEvent
        public static void scheduler_InitAppointmentImages(object sender, AppointmentImagesEventArgs e) {
            AppointmentImageInfo info = new AppointmentImageInfo();
            info.Image = Image.FromFile("image.png");
            e.ImageInfoList.Add(info);
        }
        #endregion #@InitAppointmentImagesEvent


        static void LayoutViewInfoCustomizingEvent(SchedulerControl scheduler) {
            #region #LayoutViewInfoCustomizingEvent
            scheduler.GroupType = SchedulerGroupType.Resource;
            scheduler.LayoutViewInfoCustomizing += scheduler_LayoutViewInfoCustomizing; ;
            scheduler.ActiveView.LayoutChanged();
            #endregion #LayoutViewInfoCustomizingEvent
        }

        #region #@LayoutViewInfoCustomizingEvent
        public static void scheduler_LayoutViewInfoCustomizing(object sender, LayoutViewInfoCustomizingEventArgs e) {
            string s = e.ViewInfo.GetType().ToString().Substring("DevExpress.XtraScheduler.Drawing.".Length);
            if (e.Kind == LayoutElementKind.DateHeader) {
                SchedulerHeader header = e.ViewInfo as SchedulerHeader;
                if (header != null) header.Caption = s;
            }
            if (e.Kind == LayoutElementKind.Cell) {
                SchedulerViewCellBase cell = e.ViewInfo as SchedulerViewCellBase;
                if (cell != null) cell.Appearance.BackColor = Color.LightYellow;
                SingleWeekCellBase cellWeek = e.ViewInfo as SingleWeekCellBase;
                if (cellWeek != null) {
                    cellWeek.Appearance.BackColor = Color.LightCyan;
                    cellWeek.Header.Caption = s;
                }
            }
        }
        #endregion #@LayoutViewInfoCustomizingEvent

        static void CustomizingResourceHeaders(SchedulerControl scheduler) {
            #region #CustomizingResourceHeaders
            scheduler.GroupType = SchedulerGroupType.Resource;
            scheduler.ActiveViewType = SchedulerViewType.Timeline;
            scheduler.LayoutViewInfoCustomizing += scheduler_LayoutViewInfoCustomizingResourceHeaders; ;
            scheduler.ActiveView.LayoutChanged();
            #endregion #CustomizingResourceHeaders
        }

        #region #@CustomizingResourceHeaders
        public static void scheduler_LayoutViewInfoCustomizingResourceHeaders(object sender, LayoutViewInfoCustomizingEventArgs e) {
            if (e.Kind == LayoutElementKind.ResourceHeader) {
                ResourceHeader header = e.ViewInfo as ResourceHeader;
                header.Caption = header.Resource.Caption + "\r\nNew long text line";
                e.ShouldRecalculateLayout = true;
            }
        }
        #endregion #@CustomizingResourceHeaders



        static void CustomizeDateNavigationBarCaptionEvent(SchedulerControl scheduler) {
            #region #CustomizeDateNavigationBarCaptionEvent
            scheduler.CustomizeDateNavigationBarCaption += scheduler_CustomizeDateNavigationBarCaption;
            scheduler.ActiveView.LayoutChanged();
            #endregion #CustomizeDateNavigationBarCaptionEvent
        }

        #region #@CustomizeDateNavigationBarCaptionEvent
        public static void scheduler_CustomizeDateNavigationBarCaption(object sender, CustomizeDateNavigationBarCaptionEventArgs e) {
            e.Caption = String.Format("Displaying dates from {0:D} to {1:D}", e.Interval.Start.Date, e.Interval.End.Date);
        }
        #endregion #@CustomizeDateNavigationBarCaptionEvent

        static void CustomizeMesssageBoxCaptionEvent(SchedulerControl scheduler) {
            #region #CustomizeMesssageBoxCaptionEvent
            scheduler.CustomizeMessageBoxCaption += scheduler_CustomizeMessageBoxCaption;
            scheduler.ActiveView.LayoutChanged();
            #endregion #CustomizeMesssageBoxCaptionEvent
        }

        #region #@CustomizeMesssageBoxCaptionEvent
        public static void scheduler_CustomizeMessageBoxCaption(object sender, CustomizeMessageBoxCaptionEventArgs e) {
            if (e.CaptionId == DevExpress.XtraScheduler.Localization.SchedulerStringId.Msg_SaveBeforeClose)
                e.Caption = "Appointment modification";
        }
        #endregion #@CustomizeMesssageBoxCaptionEvent


        static void AppointmentFlyoutShowingEvent(SchedulerControl scheduler) {
            #region #AppointmentFlyoutShowing
            scheduler.AppointmentFlyoutShowing += scheduler_AppointmentFlyoutShowing;
            scheduler.ActiveView.LayoutChanged();
            #endregion #AppointmentFlyoutShowing
        }

        #region #@AppointmentFlyoutShowing
        public static void scheduler_AppointmentFlyoutShowing(object sender, AppointmentFlyoutShowingEventArgs e) {
            System.Windows.Forms.Label myControl = new System.Windows.Forms.Label();
            myControl.BackColor = Color.LightGreen;
            myControl.Size = new Size(200, 100);
            myControl.Text = e.FlyoutData.Subject;
            myControl.Font = new Font("Arial", 20);
            e.Control = myControl;
        }
        #endregion #@AppointmentFlyoutShowing
    }
}
