using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Data_Access;

namespace WebApplication1.Controllers
{

    public class ReceptionistController : Controller
    {

        PaymentDataAccess paymentAccess = new PaymentDataAccess();
        AppointmentDataAccess appointmentAccess = new AppointmentDataAccess();
        DeseaseDataAccess deseaseAccess = new DeseaseDataAccess();
        TreatmentDataAccess treatmentAccess = new TreatmentDataAccess();
        ConsumableDataAccess consumableAccess = new ConsumableDataAccess();
        PatientDataAccess patientAccess = new PatientDataAccess();
        BillingDataAccess billingAccess = new BillingDataAccess();


        [Authorize(Roles = "Receptionist")]
        public ActionResult Index()
        {
            TempData.Keep("name");
            return View();
        }




        [HttpPost]
        [Authorize(Roles = "Receptionist")]
        public ActionResult ShowAppointments(bool? isApproved, string patientId)
        {
            try
            {
              
                if (isApproved != null)
                {


                    var allAppointments = appointmentAccess.GetAllAppointments();

                    var appointmentsPending = allAppointments.Where(a => a.isApproved == false).First();

                    
                    var res = appointmentAccess.ApproveOrReject(appointmentsPending, (bool)isApproved);

                    if (res == true)
                    {
                        var details = deseaseAccess.GetDeseaseDetails(patientId);

                        if (details == null)
                        {
                            var deseaseDetails = new Desease_Details()
                            {
                                no_of_visits = 0,
                                patient_id = patientId,
                                desease_catagory = "General"
                            };

                            ViewBag.AppointmentWith = "Your Appointment is successfully booked with \"General Physician.\"";
                            deseaseAccess.AddDeseaseDetails(deseaseDetails);
                        }

                    }

                }

                var appointData = appointmentAccess.GetPendingAppointments();
                return View(appointData);

            }
            catch (Exception)
            {

                return View("ReceptionistError");
            }
        }

        [Authorize(Roles = "Receptionist")]
        public ActionResult ShowAllAppointments(DateTime? date1)
        {
            try
            {
                if (date1 == null)
                {
                    var allAppointments = appointmentAccess.GetAllAppointments().ToList().Where(a => a.isApproved == true).ToList();
                    return View(allAppointments);
                }
                else
                {
                    var allAppointments = appointmentAccess.GetAllAppointments().Where(a => a.appointmentDate == date1 && a.isApproved == true).ToList();
                    return View(allAppointments);
                }
            }
            catch (Exception)
            {
                return View("ReceptionistError");
            }
        }

        [Authorize(Roles = "Receptionist")]
        public ActionResult ShowBills()
        {
            try
            {
                var paymentDetails = paymentAccess.GetPaymentDetails().Where(a => a.isRequested == false);
                
                return View(paymentDetails);
            }
            catch (Exception)
            {
                return View("ReceptionistError");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Receptionist")]
        public ActionResult ShowBills(string patientId, bool? isApporved)
        {
            try
            {
                patientId = (string)Session["BillOfPatient"];
                if (isApporved == true)
                {
                    patientId = (string)Session["BillOfPatient"];
                    var billsOfPatient = billingAccess.ShowBillsByPatientId(patientId).Where(a => a.is_paid == false).ToList();
                    for (int i = 0; i < billsOfPatient.Count(); i++)
                    {
                        billingAccess.EditBillings(billsOfPatient[i], patientId);
                    }

                    var data = paymentAccess.GetPaymentDetails().Where(a => a.patientId == patientId);

                    paymentAccess.EditPaymentDetails(patientId);
                    var paymentDetails = paymentAccess.GetPaymentDetails().Where(a => a.isRequested == false);

                    return View(paymentDetails);
                }
                else
                {
                    paymentAccess.DeletePaymentDetails(patientId);
                    var paymentDetails = paymentAccess.GetPaymentDetails().Where(a => a.isRequested == false);

                    return View(paymentDetails);
                }
            }
            catch (Exception)
            {
                return View("ReceptionistError");
            }


        }
    }
}