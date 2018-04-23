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
        static void CustomLabelsAndStatusesAction(SchedulerControl scheduler)
        {
            #region #CustomLabelsAndStatuses
            scheduler.Storage.Appointments.Clear();

            string[] IssueList = { "Consultation", "Treatment", "X-Ray" };
            Color[] IssueColorList = { Color.Ivory, Color.Pink, Color.Plum };
            string[] PaymentStatuses = { "Paid", "Unpaid" };
            Color[] PaymentColorStatuses = { Color.Green, Color.Red };


            IAppointmentLabelStorage labelStorage = scheduler.Storage.Appointments.Labels;
            labelStorage.Clear();
            int count = IssueList.Length;
            for (int i = 0; i < count; i++)
            {
                IAppointmentLabel label = labelStorage.CreateNewLabel(i, IssueList[i]);
                label.SetColor(IssueColorList[i]);
                labelStorage.Add(label);
            }
            IAppointmentStatusStorage statusStorage = scheduler.Storage.Appointments.Statuses;
            statusStorage.Clear();
            count = PaymentStatuses.Length;
            for (int i = 0; i < count; i++)
            {
                IAppointmentStatus status = statusStorage.CreateNewStatus(i, PaymentStatuses[i], PaymentStatuses[i]);
                status.SetBrush(new SolidBrush(PaymentColorStatuses[i]));
                statusStorage.Add(status);
            }
            #endregion #CustomLabelsAndStatuses
        }
    }
}
