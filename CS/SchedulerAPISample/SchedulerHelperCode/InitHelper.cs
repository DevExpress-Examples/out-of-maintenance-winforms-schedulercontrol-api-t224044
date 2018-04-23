using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerAPISample
{
    public class InitHelper
    {
        public static Random RandomInstance = new Random();

        public static void InitResources(BindingList<CustomResource> resources)
        {
            resources.Add(CreateCustomResource(1, "Peter Dolan", Color.PowderBlue));
            resources.Add(CreateCustomResource(2, "Ryan Fisher", Color.PaleVioletRed));
            resources.Add(CreateCustomResource(3, "Andrew Miller", Color.PeachPuff));
        }

        public static CustomResource CreateCustomResource(int res_id, string caption, Color ResColor)
        {
            CustomResource cr = new CustomResource();
            cr.ResID = res_id;
            cr.Name = caption;
            return cr;
        }



        public static void InitAppointments(BindingList<CustomAppointment> appointments, BindingList<CustomResource> resources)
        {
            GenerateEvents(appointments, resources);
        }


        public static void GenerateEvents(BindingList<CustomAppointment> eventList, BindingList<CustomResource> resources)
        {
            int count = resources.Count;

            for (int i = 0; i < count; i++)
            {
                CustomResource resource = resources[i];
                string subjPrefix = resource.Name + "'s ";
                eventList.Add(CreateEvent(subjPrefix + "meeting", resource.ResID, 2, 5));
                eventList.Add(CreateEvent(subjPrefix + "travel", resource.ResID, 3, 6));
                eventList.Add(CreateEvent(subjPrefix + "special", resource.ResID, 0, 10));
            }
        }
        public static CustomAppointment CreateEvent(string subject, object resourceId, int status, int label)
        {
            CustomAppointment apt = new CustomAppointment();
            apt.Subject = subject;
            apt.OwnerId = resourceId;
            Random rnd = RandomInstance;
            int rangeInMinutes = 60 * 24;
            apt.StartTime = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes));
            apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes / 4));
            apt.Status = status;
            apt.Label = label;
            apt.Description = (rnd.Next() % 3 == 0) ? "Sample description" : String.Empty;
            return apt;
        }
    }
}
