using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using System;
using System.Drawing;
using System.Linq;

namespace SchedulerAPISample.CodeExamples
{
    class CustomDraw
    {

        static void CustomDrawAppointmentEvent(SchedulerControl scheduler)
        {
            #region #CustomDrawAppointmentEvent
            scheduler.CustomDrawAppointment += scheduler_CustomDrawAppointment;
            scheduler.ActiveView.LayoutChanged();
            #endregion #CustomDrawAppointmentEvent
        }

        #region #@CustomDrawAppointmentEvent
        public static void scheduler_CustomDrawAppointment(object sender, CustomDrawObjectEventArgs e)
        {
            if (((SchedulerControl)sender).ActiveView is DayView)
            {
                AppointmentViewInfo viewInfo = e.ObjectInfo as AppointmentViewInfo;
                // Get DevExpress images.
                Image im = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/add_16x16.png");

                Rectangle imageBounds = new Rectangle(viewInfo.InnerBounds.X, viewInfo.InnerBounds.Y, im.Width, im.Height);
                Rectangle mainContentBounds = new Rectangle(viewInfo.InnerBounds.X, viewInfo.InnerBounds.Y + im.Width + 1,
                    viewInfo.InnerBounds.Width, viewInfo.InnerBounds.Height - im.Height - 1);
                // Draw image in the appointment.
                e.Cache.Graphics.DrawImage(im, imageBounds);

                int statusDelta = 0;
                for (int i = 0; i < viewInfo.StatusItems.Count; i++)
                {
                    AppointmentViewInfoStatusItem statusItem = viewInfo.StatusItems[i] as AppointmentViewInfoStatusItem;
                    // Fill the status bar.
                    e.Cache.FillRectangle(statusItem.BackgroundViewInfo.Brush, statusItem.BackgroundViewInfo.Bounds);
                    e.Cache.FillRectangle(statusItem.ForegroundViewInfo.Brush, statusItem.ForegroundViewInfo.Bounds);
                    // Draw the satus bar rectangle.
                    e.Cache.DrawRectangle(new Pen(statusItem.ForegroundViewInfo.BorderColor), statusItem.BackgroundViewInfo.Bounds);
                    e.Cache.DrawRectangle(new Pen(statusItem.ForegroundViewInfo.BorderColor), statusItem.ForegroundViewInfo.Bounds);
                    statusDelta = Math.Max(statusDelta, statusItem.Bounds.Width);
                }
                // Draw the appointment caption text.
                e.Cache.DrawString(viewInfo.DisplayText.Trim(), viewInfo.Appearance.Font,
                    viewInfo.Appearance.GetForeBrush(e.Cache), mainContentBounds, StringFormat.GenericDefault);
                SizeF subjSize = e.Graphics.MeasureString(viewInfo.DisplayText.Trim(), viewInfo.Appearance.Font, mainContentBounds.Width);
                int lineYposition = (int)subjSize.Height;

                Rectangle descriptionLocation = new Rectangle(mainContentBounds.X, mainContentBounds.Y + lineYposition,
                    mainContentBounds.Width, mainContentBounds.Height - lineYposition);
                if (viewInfo.Appointment.Description.Trim() != "")
                {
                    // Draw the line above the appointment description.
                    e.Cache.Graphics.DrawLine(viewInfo.Appearance.GetForePen(e.Cache), descriptionLocation.Location,
                        new Point(descriptionLocation.X + descriptionLocation.Width, descriptionLocation.Y));
                    // Draw the appointment description text.
                    e.Cache.DrawString(viewInfo.Appointment.Description.Trim(), viewInfo.Appearance.Font,
                        viewInfo.Appearance.GetForeBrush(e.Cache), descriptionLocation, StringFormat.GenericTypographic);
                }
                e.Handled = true;
            }
        }
        #endregion #@CustomDrawAppointmentEvent

        static void CustomDrawAppointmentBackgroundEvent(SchedulerControl scheduler)
        {
            #region #CustomDrawAppointmentBackgroundEvent
            scheduler.CustomDrawAppointmentBackground += scheduler_CustomDrawAppointmentBackground;
            scheduler.ActiveView.LayoutChanged();
            #endregion #CustomDrawAppointmentBackgroundEvent
        }

        #region #@CustomDrawAppointmentBackgroundEvent
        public static void scheduler_CustomDrawAppointmentBackground(object sender, CustomDrawObjectEventArgs e)
        {
            AppointmentViewInfo viewInfo = e.ObjectInfo as AppointmentViewInfo;
            // Specify the ratio of a completed task to the entire task.
            double completenessRatio = 0.25 * ((int)(viewInfo.Appointment.ResourceId) % 4);
            // Draw an appointment as usual.
            e.DrawDefault();
            // Draw a background rectangle.
            Rectangle bounds = CalculateEntireAppointmentBounds(viewInfo);
            DrawBackGroundCore(e.Cache, bounds, completenessRatio);
            // Indicate that no default drawing is required.
            e.Handled = true;
        }
        static Rectangle CalculateEntireAppointmentBounds(AppointmentViewInfo viewInfo)
        {
            int leftOffset = 0;
            int rightOffset = 0;
            double scale = viewInfo.Bounds.Width / viewInfo.Interval.Duration.TotalMilliseconds;
            if (!viewInfo.HasLeftBorder)
            {
                double hidden = (viewInfo.Interval.Start - viewInfo.AppointmentInterval.Start).TotalMilliseconds;
                leftOffset = (int)(hidden * scale);
            }
            if (!viewInfo.HasRightBorder)
            {
                double hidden = (viewInfo.AppointmentInterval.End - viewInfo.Interval.End).TotalMilliseconds;
                rightOffset = (int)(hidden * scale);
            }
            Rectangle bounds = viewInfo.InnerBounds;
            return Rectangle.FromLTRB(bounds.Left - leftOffset, bounds.Y, bounds.Right + rightOffset, bounds.Bottom);
        }
        static void DrawBackGroundCore(DevExpress.Utils.Drawing.GraphicsCache cache, Rectangle bounds, double completenessRatio)
        {
            Brush brush1 = new SolidBrush(Color.LightGray);
            cache.FillRectangle(brush1, new Rectangle(bounds.X, bounds.Y, (int)(bounds.Width * completenessRatio), bounds.Height));
        }
        #endregion #@CustomDrawAppointmentBackgroundEvent

        static void CustomDrawDayHeaderEvent(SchedulerControl scheduler)
        {
            #region #CustomDrawDayHeaderEvent
            scheduler.CustomDrawDayHeader += scheduler_CustomDrawDayHeader;
            scheduler.ActiveViewType = SchedulerViewType.FullWeek;
            scheduler.ActiveView.LayoutChanged();
            #endregion #CustomDrawDayHeaderEvent
        }
        #region #@CustomDrawDayHeaderEvent
        public static void scheduler_CustomDrawDayHeader(object sender, CustomDrawObjectEventArgs e)
        {
            DayHeader header = e.ObjectInfo as DayHeader;
            // Draw the outer rectangle.
            e.Cache.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds,
                Color.LightBlue, Color.Blue, System.Drawing.Drawing2D.LinearGradientMode.Vertical), e.Bounds);
            Rectangle innerRect = Rectangle.Inflate(e.Bounds, -2, -2);
            // Draw the inner rectangle.
            e.Cache.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds,
                Color.Blue, Color.LightSkyBlue, System.Drawing.Drawing2D.LinearGradientMode.Vertical), innerRect);
            // Draw the header caption.
            e.Cache.DrawString(header.Caption, header.Appearance.HeaderCaption.Font,
                new SolidBrush(Color.White), innerRect,
                header.Appearance.HeaderCaption.GetStringFormat());
            e.Handled = true;

        }
        #endregion #@CustomDrawDayHeaderEvent

        static void CustomDrawDayOfWeekHeaderEvent(SchedulerControl scheduler)
        {
            #region #CustomDrawDayOfWeekHeaderEvent
            scheduler.CustomDrawDayOfWeekHeader += scheduler_CustomDrawDayOfWeekHeader;
            scheduler.ActiveViewType = SchedulerViewType.Month;
            scheduler.ActiveView.LayoutChanged();
            #endregion #CustomDrawDayOfWeekHeaderEvent
        }
        #region #@CustomDrawDayOfWeekHeaderEvent
        public static void scheduler_CustomDrawDayOfWeekHeader(object sender, CustomDrawObjectEventArgs e)
        {
            DayOfWeekHeader header = e.ObjectInfo as DayOfWeekHeader;
            // Draw the outer rectangle.
            e.Cache.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds,
                Color.LightGreen, Color.Green, System.Drawing.Drawing2D.LinearGradientMode.Vertical), e.Bounds);
            Rectangle innerRect = Rectangle.Inflate(e.Bounds, -2, -2);
            // Draw the inner rectangle.
            e.Cache.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds,
                Color.Green, Color.LightGreen, System.Drawing.Drawing2D.LinearGradientMode.Vertical), innerRect);
            // Draw the header caption.
            e.Cache.DrawString(header.Caption, header.Appearance.HeaderCaption.Font,
                new SolidBrush(Color.White), innerRect,
                header.Appearance.HeaderCaption.GetStringFormat());
            e.Handled = true;

        }
        #endregion #@CustomDrawDayOfWeekHeaderEvent

        static void CustomDrawDayViewAllDayAreaEvent(SchedulerControl scheduler)
        {
            #region #CustomDrawDayViewAllDayAreaEvent
            scheduler.CustomDrawDayViewAllDayArea += scheduler_CustomDrawDayViewAllDayArea;
            scheduler.ActiveViewType = SchedulerViewType.Day;
            scheduler.GroupType = SchedulerGroupType.Resource;
            scheduler.ActiveView.LayoutChanged();
            #endregion #CustomDrawDayViewAllDayAreaEvent
        }
        #region #@CustomDrawDayViewAllDayAreaEvent
        public static void scheduler_CustomDrawDayViewAllDayArea(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e)
        {
            AllDayAreaCell cell = (AllDayAreaCell)e.ObjectInfo;
            Resource resource = cell.Resource;
            TimeInterval interval = cell.Interval;
            AppointmentBaseCollection apts = ((SchedulerControl)sender).Storage.GetAppointments(interval);
            // Specify what precentage of the appointment duration should be painted.
            float percent = CalcCurrentWorkTimeLoad(apts, interval, resource);
            Brush brush;
            // Select the brush color.
            if (percent == 0.0)
                brush = Brushes.LightYellow;
            else if (percent < 0.5)
                brush = Brushes.LightBlue;
            else brush = Brushes.LightCoral;
            // Paint the area with the selected color.
            e.Cache.FillRectangle(brush, e.Bounds);
            // Draw the percentage text. 
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            e.Cache.DrawString(string.Format("{0:P}", percent), cell.Appearance.Font, Brushes.Black, e.Bounds, format);
            e.Handled = true;
        }
        private static float CalcCurrentWorkTimeLoad(AppointmentBaseCollection apts, TimeInterval interval, Resource resource)
        {
            AppointmentBaseCollection aptsByResource = new AppointmentBaseCollection();
            var aptQuery = apts.Where(a => a.ResourceId.Equals(resource.Id));
            aptsByResource.AddRange(aptQuery.ToList());

            DevExpress.XtraScheduler.Tools.IntervalLoadRatioCalculator calc =
                new DevExpress.XtraScheduler.Tools.IntervalLoadRatioCalculator(interval, aptsByResource);
            return calc.Calculate();
        }
        #endregion #@CustomDrawDayViewAllDayAreaEvent

        static void CustomDrawDayViewTimeRulerEvent(SchedulerControl scheduler)
        {
            #region #CustomDrawDayViewTimeRulerEvent
            scheduler.CustomDrawDayViewTimeRuler += scheduler_CustomDrawDayViewTimeRuler;
            scheduler.ActiveView.LayoutChanged();
            #endregion #CustomDrawDayViewTimeRulerEvent
        }

        #region #@CustomDrawDayViewTimeRulerEvent
        public static void scheduler_CustomDrawDayViewTimeRuler(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e)
        {
            TimeRulerViewInfo info = e.ObjectInfo as TimeRulerViewInfo;
            // Clear all captions.
            foreach (var item in info.Items)
            {
                ViewInfoTextItem viewInfoText = item as ViewInfoTextItem;
                if (viewInfoText != null)
                {
                    viewInfoText.Text = string.Empty;
                }
            }
            // Draw the TimeRuler as usual, but with empty captions.
            e.DrawDefault();
            // Draw the image in the header.
            Image im = Image.FromFile("image.png");
            Rectangle imageBounds = new Rectangle(info.HeaderBounds.X, info.HeaderBounds.Y, im.Width, im.Height);
            e.Cache.Graphics.DrawImage(im, imageBounds);
            // Cancel default painting procedure.
            e.Handled = true;
        }
        #endregion #@CustomDrawDayViewTimeRulerEvent

        static void CustomDrawGroupSeparator(SchedulerControl scheduler)
        {
            #region #CustomDrawGroupSeparatorEvent
            scheduler.CustomDrawGroupSeparator += scheduler_CustomDrawGroupSeparator;
            scheduler.GroupType = SchedulerGroupType.Resource;
            scheduler.ActiveView.LayoutChanged();
            #endregion #CustomDrawGroupSeparatorEvent
        }

        #region #@CustomDrawGroupSeparatorEvent
        public static void scheduler_CustomDrawGroupSeparator(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e)
        {
            e.DrawDefault();
            Image im = Image.FromFile("image.png");
            Rectangle imageBounds = new Rectangle(e.Bounds.X - (im.Width / 2), e.Bounds.Y, im.Width, im.Height);
            e.Cache.Graphics.DrawImage(im, imageBounds);
            e.Handled = true;
        }
        #endregion #@CustomDrawGroupSeparatorEvent

        static void CustomDrawNavigationButton(SchedulerControl scheduler)
        {
            #region #CustomDrawNavigationButtonEvent
            scheduler.CustomDrawNavigationButton += scheduler_CustomDrawNavigationButton;
            scheduler.Services.DateTimeNavigation.NavigateBackward();
            #endregion #CustomDrawNavigationButtonEvent
        }

        #region #@CustomDrawNavigationButtonEvent
        public static void scheduler_CustomDrawNavigationButton(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e)
        {
            NavigationButtonNext navButton = e.ObjectInfo as NavigationButtonNext;
            SchedulerControl scheduler = sender as SchedulerControl;
            SchedulerStorage storage = scheduler.Storage as SchedulerStorage;
            // Do not count by resources.
            if (scheduler.GroupType != SchedulerGroupType.None) return;

            if (navButton != null && scheduler != null && storage != null)
            {
                // Count appointments within the interval used by the Next navigation button.
                AppointmentBaseCollection apts = scheduler.Storage.Appointments.Items;
                TimeSpan aptSearchInterval = scheduler.OptionsView.NavigationButtons.AppointmentSearchInterval;
                DateTime lastVisibleTime = scheduler.ActiveView.GetVisibleIntervals().Last().End;
                int aptCount = apts.Where(a => (a.Start > lastVisibleTime) && (a.Start < lastVisibleTime.Add(aptSearchInterval))).Count();
                navButton.DisplayTextItem.Text = String.Format("Next {0} appointments", aptCount);
            }
        }
        #endregion #@CustomDrawNavigationButtonEvent

        static void CustomDrawResourceHeader(SchedulerControl scheduler)
        {
            #region #CustomDrawResourceHeaderEvent
            // Add information to resources.
            IResourceStorageBase resStorage = scheduler.Storage.Resources;
            resStorage.CustomFieldMappings.Add(new ResourceCustomFieldMapping("PostCode", "mPostCode"));
            resStorage.CustomFieldMappings.Add(new ResourceCustomFieldMapping("Address", "mAddress"));
            resStorage[0].CustomFields["PostCode"] = "16285 Schwedt";
            resStorage[0].CustomFields["Address"] = "Landhausstraße 52 ";
            // Increase default height to fit a multi-line caption.
            scheduler.OptionsView.ResourceHeaders.Height = 50;
            scheduler.GroupType = SchedulerGroupType.Resource;
            scheduler.CustomDrawResourceHeader += scheduler_CustomDrawResourceHeader;
            #endregion #CustomDrawResourceHeaderEvent
        }

        #region #@CustomDrawResourceHeaderEvent
        public static void scheduler_CustomDrawResourceHeader(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e)
        {
            ResourceHeader header = (ResourceHeader)e.ObjectInfo;
            // Get the resource information from custom fields.
            string postcode = (header.Resource.CustomFields["PostCode"] != null) ? header.Resource.CustomFields["PostCode"].ToString() : String.Empty;
            string address = (header.Resource.CustomFields["Address"] != null) ? header.Resource.CustomFields["Address"].ToString() : String.Empty;
            // Specify the header caption and appearance.
            header.Appearance.HeaderCaption.ForeColor = Color.Blue;
            header.Caption = header.Resource.Caption + System.Environment.NewLine + address + System.Environment.NewLine + postcode;
            header.Appearance.HeaderCaption.Font = e.Cache.GetFont(header.Appearance.HeaderCaption.Font, FontStyle.Bold);
            // Draw the header using default methods.
            e.DrawDefault();
            e.Handled = true;
        }
        #endregion #@CustomDrawResourceHeaderEvent

        static void CustomDrawTimeCell(SchedulerControl scheduler)
        {
            #region #CustomDrawTimeCellEvent
            scheduler.CustomDrawTimeCell += scheduler_CustomDrawTimeCell;
            scheduler.DayView.DayCount = 3;
            scheduler.ActiveViewType = SchedulerViewType.Day;
            #endregion #CustomDrawTimeCellEvent
        }

        #region #@CustomDrawTimeCellEvent
        public static void scheduler_CustomDrawTimeCell(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e)
        {
            // Get the cell to draw.
            SelectableIntervalViewInfo cell =
                e.ObjectInfo as SelectableIntervalViewInfo;
            if (cell != null) {
                // Draw the cell.
                Brush myBrush = (cell.Selected) ? SystemBrushes.Highlight : SystemBrushes.Window;
                e.Cache.FillRectangle(myBrush, cell.Bounds);
            }
            e.Handled = true;
        }
        #endregion #@CustomDrawTimeCellEvent

        public static void CustomDrawWeekViewTopLeftCorner(SchedulerControl scheduler)
        {
            #region #CustomDrawWeekViewTopLeftCornerEvent
            scheduler.ActiveViewType = SchedulerViewType.Timeline;
            scheduler.GroupType = SchedulerGroupType.Date;
            scheduler.OptionsView.ResourceHeaders.Height = 50;
            scheduler.CustomDrawWeekViewTopLeftCorner += scheduler_CustomDrawWeekViewTopLeftCorner;
            #endregion #CustomDrawWeekViewTopLeftCornerEvent
        }

        #region #@CustomDrawWeekViewTopLeftCornerEvent
        public static void scheduler_CustomDrawWeekViewTopLeftCorner(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e)
        {
            e.DrawDefault();
            UpperLeftCorner objectToDraw = e.ObjectInfo as UpperLeftCorner;
            string text = "Employee";
            // Draw the text rotated 45 degrees counterclockwise.
            Font myFont = objectToDraw.CaptionAppearance.Font;
            SizeF textSize = e.Graphics.MeasureString(text, myFont);
            e.Graphics.TranslateTransform(e.Bounds.Width / 2, e.Bounds.Height / 2);
            e.Graphics.RotateTransform(-45f);
            e.Graphics.DrawString(text, myFont, Brushes.Blue, -(textSize.Width / 2.0f), -(textSize.Height / 2.0f));
            e.Graphics.RotateTransform(45f);
            e.Graphics.TranslateTransform(-e.Bounds.Width / 2, -e.Bounds.Height / 2);
            e.Handled = true;
        }
        #endregion #@CustomDrawWeekViewTopLeftCornerEvent

        public static void CustomDrawTimeIndicator(SchedulerControl scheduler) {
            #region #CustomDrawTimeIndicatorEvent
            scheduler.ActiveViewType = SchedulerViewType.Day;
            scheduler.DayView.TopRowTime = new TimeSpan(DateTime.Now.Hour, 0, 0);
            scheduler.DayView.TimeIndicatorDisplayOptions.ShowOverAppointment = true;
            scheduler.GroupType = SchedulerGroupType.Date;
            scheduler.OptionsView.ResourceHeaders.Height = 50;
            scheduler.CustomDrawTimeIndicator += scheduler_CustomDrawTimeIndicator;
            #endregion #CustomDrawTimeIndicatorEvent
        }

        #region #@CustomDrawTimeIndicatorEvent
        public static void scheduler_CustomDrawTimeIndicator(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e) {
            TimeIndicatorViewInfo info = e.ObjectInfo as TimeIndicatorViewInfo;
            SchedulerControl scheduler = sender as SchedulerControl;
            foreach (var item in info.Items) {
                TimeIndicatorBaseItem timeIndicatorItem = item as TimeIndicatorBaseItem;
                if (timeIndicatorItem != null) {
                    e.DrawDefault();
                    Rectangle boundsText = Rectangle.Empty;
                    if (scheduler.ActiveView is DayView) {
                        boundsText = Rectangle.Inflate(timeIndicatorItem.Bounds, 0, 5);
                        boundsText.Offset(((int)e.Graphics.ClipBounds.Width / 2), -10);
                    }
                    e.Cache.DrawString(info.Interval.Start.ToString(), scheduler.Appearance.HeaderCaption.Font, new SolidBrush(Color.Red), boundsText, 
                        scheduler.Appearance.HeaderCaption.GetStringFormat());
                }
            }
            e.Handled = true;
        }
        #endregion #@CustomDrawTimeIndicatorEvent
    }
}
