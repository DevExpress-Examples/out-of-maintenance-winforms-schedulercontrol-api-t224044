using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerAPISample.CodeExamples
{
    class LabelsAndStatuses
    {
        static void LabelInfoAction(SchedulerControl scheduler) {
            #region #LabelInfo
            scheduler.InitAppointmentDisplayText += scheduler_InitAppointmentDisplayText;
            scheduler.ActiveView.LayoutChanged();
            #endregion #LabelInfo
        }

        #region #@LabelInfo
        public static void scheduler_InitAppointmentDisplayText(object sender, AppointmentDisplayTextEventArgs e) {
            SchedulerControl scheduler = sender as SchedulerControl;
            IAppointmentLabel label = scheduler.DataStorage.Appointments.Labels.GetById(e.Appointment.LabelKey);
            e.Description = String.Format("Label Info:\nDisplayName = '{0}'\nID = '{1}'", label.DisplayName, label.Id.ToString());
        }
        #endregion #@LabelInfo


        static void StatusInfoAction(SchedulerControl scheduler) {
            #region #StatusInfo
            scheduler.InitAppointmentDisplayText += scheduler_InitAppointmentDisplayText_1;
            scheduler.ActiveView.LayoutChanged();
            #endregion #StatusInfo
        }

        #region #@StatusInfo
        public static void scheduler_InitAppointmentDisplayText_1(object sender, AppointmentDisplayTextEventArgs e) {
            SchedulerControl scheduler = sender as SchedulerControl;
            IAppointmentStatus status = scheduler.DataStorage.Appointments.Statuses.GetById(e.Appointment.StatusKey);
            e.Description = String.Format("Status Info:\nDisplayName = '{0}'\nID = '{1}'", status.DisplayName, status.Id.ToString());
        }
        #endregion #@StatusInfo

        static void ChangingLabelColorSchemeAction(SchedulerControl scheduler) {
            #region #ChangingLabelColorScheme
            IAppointmentLabelStorage labelStorage = scheduler.DataStorage.Appointments.Labels;
            foreach (IAppointmentLabel label in labelStorage) {
                AppointmentLabel appLabel = label as AppointmentLabel;
                if (appLabel != null) {
                    string skinElemName = AppointmentLabel.GetSkinElementName(appLabel.ColorId);
                    Color skinColor = DevExpress.Skins.SkinManager.Default.Skins["Office 2016 Colorful"].GetSkin(DevExpress.Skins.SkinProductId.Scheduler).Colors.GetPrimaryColor(skinElemName, Color.Empty);
                    if (skinColor == Color.Empty)
                        skinColor = AppointmentLabel.GetDefaultColorByStringId(appLabel.ColorId);
                    appLabel.SetColor(skinColor);
                }
                scheduler.ActiveView.LayoutChanged();
            }
            #endregion #ChangingLabelColorScheme
        }

        static void CustomLabelsAndStatusesAction(SchedulerControl scheduler) {
            #region #CustomLabelsAndStatuses
            scheduler.DataStorage.Appointments.Clear();

            string[] IssueList = { "Consultation", "Treatment", "X-Ray" };
            Color[] IssueColorList = { Color.Ivory, Color.Pink, Color.Plum };
            string[] PaymentStatuses = { "Paid", "Unpaid" };
            Color[] PaymentColorStatuses = { Color.Green, Color.Red };


            IAppointmentLabelStorage labelStorage = scheduler.DataStorage.Appointments.Labels;
            labelStorage.Clear();
            int count = IssueList.Length;
            for (int i = 0; i < count; i++) {
                IAppointmentLabel label = labelStorage.CreateNewLabel(i, IssueList[i]);
                label.SetColor(IssueColorList[i]);
                labelStorage.Add(label);
            }
            IAppointmentStatusStorage statuses = scheduler.DataStorage.Appointments.Statuses;
            statuses.Clear();
            count = PaymentStatuses.Length;
            for (int i = 0; i < count; i++) {
                IAppointmentStatus status = statuses.CreateNewStatus(i, PaymentStatuses[i], PaymentStatuses[i]);
                status.SetBrush(new SolidBrush(PaymentColorStatuses[i]));
                statuses.Add(status);
            }
            #endregion #CustomLabelsAndStatuses
        }
    }
}
