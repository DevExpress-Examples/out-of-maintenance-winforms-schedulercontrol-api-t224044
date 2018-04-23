using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using System;
using System.Drawing;

namespace SchedulerAPISample.CodeExamples {
    class Selection {

        static void SelectIntervalWithResourceUnspecified(SchedulerControl scheduler) {
            #region #SelectIntervalWithResourceUnspecified
            scheduler.CustomDrawTimeCell += scheduler_CustomDrawTimeCell_1;
            //scheduler.Storage.Appointments.Clear();
            scheduler.ActiveView.SetSelection(new TimeInterval(DateTime.Now, new TimeSpan(2, 40, 0)), ResourceEmpty.Resource);
            #endregion #SelectIntervalWithResourceUnspecified
        }

        #region #@SelectIntervalWithResourceUnspecified
        public static void scheduler_CustomDrawTimeCell_1(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e) {
            Brush solidBrush = new SolidBrush(Color.Lime);
            SelectableIntervalViewInfo cell =
                e.ObjectInfo as SelectableIntervalViewInfo;
            if (cell != null) {
                if (cell.Selected) {
                    e.Graphics.FillRectangle(solidBrush, e.Bounds);
                    e.Handled = true;
                }
            }
        }
        #endregion #@SelectIntervalWithResourceUnspecified

        static void SelectIntervalWithSpecifiedResource(SchedulerControl scheduler) {
            #region #SelectIntervalWithSpecifiedResource
            scheduler.CustomDrawTimeCell += scheduler_CustomDrawTimeCell_2;
            //scheduler.Storage.Appointments.Clear();
            scheduler.ActiveView.GroupType = SchedulerGroupType.Resource;
            scheduler.ActiveView.SetSelection(new TimeInterval(DateTime.Now, new TimeSpan(2, 40, 0)), scheduler.ActiveView.GetResources()[1]);
            #endregion #SelectIntervalWithSpecifiedResource
        }

        #region #@SelectIntervalWithSpecifiedResource
        public static void scheduler_CustomDrawTimeCell_2(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e) {
            Brush solidBrush = new SolidBrush(Color.Lime);
            SelectableIntervalViewInfo cell =
                e.ObjectInfo as SelectableIntervalViewInfo;
            if (cell != null) {
                if (cell.Selected) {
                    e.Graphics.FillRectangle(solidBrush, e.Bounds);
                    e.Handled = true;
                }
            }
        }
        #endregion #@SelectIntervalWithSpecifiedResource

        static void SelectIntervalUsingService(SchedulerControl scheduler) {
            #region #SelectIntervalUsingService
            scheduler.CustomDrawTimeCell += scheduler_CustomDrawTimeCell_3;
            scheduler.Services.Selection.SelectedInterval = new TimeInterval(DateTime.Now, new TimeSpan(2, 40, 0));
            #endregion #SelectIntervalUsingService
        }

        #region #@SelectIntervalUsingService
        public static void scheduler_CustomDrawTimeCell_3(object sender, DevExpress.XtraScheduler.CustomDrawObjectEventArgs e) {
            Brush solidBrush = new SolidBrush(Color.Lime);
            SelectableIntervalViewInfo cell =
                e.ObjectInfo as SelectableIntervalViewInfo;
            if (cell != null) {
                if (cell.Selected) {
                    e.Graphics.FillRectangle(solidBrush, e.Bounds);
                    e.Handled = true;
                }
            }
        }
        #endregion #@SelectIntervalUsingService


        static void SelectSingleAppointment(SchedulerControl scheduler) {
            #region #SelectSingleAppointment
            scheduler.CustomDrawAppointmentBackground += scheduler_CustomDrawAppointmentBackground_1;
            scheduler.ActiveView.GroupType = SchedulerGroupType.Resource;
            scheduler.ActiveView.ChangeAppointmentSelection(scheduler.ActiveView.GetAppointments()[1]);
            #endregion #SelectSingleAppointment
        }
        #region #@SelectSingleAppointment
        public static void scheduler_CustomDrawAppointmentBackground_1(object sender, CustomDrawObjectEventArgs e) {
            AppointmentViewInfo aptViewInfo = e.ObjectInfo as AppointmentViewInfo;
            if (aptViewInfo == null)
                return;
            if (aptViewInfo.Selected) {
                e.DrawDefault();
                Rectangle r = e.Bounds;
                Brush brRect = aptViewInfo.Status.GetBrush();
                e.Graphics.DrawRectangle(new Pen(Color.Lime, 4), r);
                e.Handled = true;
            }
        }
        #endregion #@SelectSingleAppointment


        static void SelectMultipleAppointments(SchedulerControl scheduler) {
            #region #SelectMultipleAppointments
            scheduler.CustomDrawAppointmentBackground += scheduler_CustomDrawAppointmentBackground_2;
            scheduler.ActiveView.GroupType = SchedulerGroupType.Resource;
            scheduler.ActiveView.AddAppointmentSelection(scheduler.ActiveView.GetAppointments()[1]);
            scheduler.ActiveView.AddAppointmentSelection(scheduler.ActiveView.GetAppointments()[2]);
            scheduler.ActiveView.AddAppointmentSelection(scheduler.ActiveView.GetAppointments()[3]);
            #endregion #SelectMultipleAppointments
        }
        #region #@SelectMultipleAppointments
        public static void scheduler_CustomDrawAppointmentBackground_2(object sender, CustomDrawObjectEventArgs e) {
            AppointmentViewInfo aptViewInfo = e.ObjectInfo as AppointmentViewInfo;
            if (aptViewInfo == null)
                return;
            if (aptViewInfo.Selected) {
                e.DrawDefault();
                Rectangle r = e.Bounds;
                Brush brRect = aptViewInfo.Status.GetBrush();
                e.Graphics.DrawRectangle(new Pen(Color.Lime, 4), r);
                e.Handled = true;
            }
        }
        #endregion #@SelectMultipleAppointments

        static void SelectMultipleAppointmentsUsingService(SchedulerControl scheduler) {
            #region #SelectMultipleAppointmentUsingService
            scheduler.CustomDrawAppointmentBackground += scheduler_CustomDrawAppointmentBackground_3;
            scheduler.ActiveView.GroupType = SchedulerGroupType.Resource;
            scheduler.Services.AppointmentSelection.SetSelectedAppointments(scheduler.ActiveView.GetAppointments());
            #endregion #SelectMultipleAppointmentsUsingService
        }
        #region #@SelectMultipleAppointmentsUsingService
        public static void scheduler_CustomDrawAppointmentBackground_3(object sender, CustomDrawObjectEventArgs e) {
            AppointmentViewInfo aptViewInfo = e.ObjectInfo as AppointmentViewInfo;
            if (aptViewInfo == null)
                return;
            if (aptViewInfo.Selected) {
                e.DrawDefault();
                Rectangle r = e.Bounds;
                Brush brRect = aptViewInfo.Status.GetBrush();
                e.Graphics.DrawRectangle(new Pen(Color.Lime, 4), r);
                e.Handled = true;
            }
        }
        #endregion #@SelectMultipleAppointmentsUsingService
    }
}
