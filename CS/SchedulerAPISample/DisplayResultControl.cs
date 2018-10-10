using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraScheduler;

namespace SchedulerAPISample
{
    public partial class DisplayResultControl : UserControl
    {
        private BindingList<CustomResource> CustomResourceCollection = new BindingList<CustomResource>();
        private BindingList<CustomAppointment> CustomEventList = new BindingList<CustomAppointment>();
        
        public SchedulerControl SchedulerControl {get; set;}

        public DisplayResultControl()
        {
            InitializeComponent();
            this.SchedulerControl = this.schedulerControl1;

            InitHelper.InitResources(CustomResourceCollection);
            InitHelper.InitAppointments(CustomEventList, CustomResourceCollection);

            ResourceMappingInfo mappingsResource = this.schedulerDataStorage1.Resources.Mappings;
            mappingsResource.Id = "ResID";
            mappingsResource.Caption = "Name";

            AppointmentMappingInfo mappingsAppointment = this.schedulerDataStorage1.Appointments.Mappings;
            mappingsAppointment.Start = "StartTime";
            mappingsAppointment.End = "EndTime";
            mappingsAppointment.Subject = "Subject";
            mappingsAppointment.AllDay = "AllDay";
            mappingsAppointment.Description = "Description";
            mappingsAppointment.Label = "Label";
            mappingsAppointment.Location = "Location";
            mappingsAppointment.RecurrenceInfo = "RecurrenceInfo";
            mappingsAppointment.ReminderInfo = "ReminderInfo";
            mappingsAppointment.ResourceId = "OwnerId";
            mappingsAppointment.Status = "Status";
            mappingsAppointment.Type = "EventType";

            this.schedulerDataStorage1.Resources.DataSource = CustomResourceCollection;
            this.schedulerDataStorage1.Appointments.DataSource = CustomEventList;

            this.schedulerControl1.Start = DateTime.Now;
        }
}
    }
