using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using WebApplication1.Data_Access;

namespace WebApplication1.Controllers
{
    
    public class PatientController : Controller
    {

        PatientDataAccess patientAccess = new PatientDataAccess();
        AppointmentDataAccess appointmentAccess = new AppointmentDataAccess();
        DeseaseDataAccess deseaseAccess = new DeseaseDataAccess();
        UserDataAccess userAccess = new UserDataAccess();
        TreatmentDataAccess treatmentAccess = new TreatmentDataAccess();
        ConsumableDataAccess consumableAccess = new ConsumableDataAccess();
        BillingDataAccess billingAccess = new BillingDataAccess();
        PaymentDataAccess paymentAccess = new PaymentDataAccess();


        [Authorize(Roles = "Patient")]
        public ActionResult Index()
        {
            TempData.Keep("name");

            var allAppointments = appointmentAccess.GetAllAppointments();
            return View(allAppointments);
        }


        
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public ActionResult TakeAppointment(DateTime? date, string time)
        {
            try
            {
                if (date != null)
                {
                    Session["AppointmentDate"] = date;
                }
                else
                {
                    if (time != null)
                    {
                        Session["AppointmentTime"] = time;
                    }
                    else
                    {
                        ViewBag.AppointmentDateRequired = "Please Select Appointment Date";
                        return View("Index");
                    }

                }


                if (Session["AppointmentDate"] != null && Session["AppointmentTime"] != null)
                {
                    return RedirectToAction("PatientDetails");
                }
                var allAppointments = appointmentAccess.GetAllAppointments();

                return View("Index", allAppointments);

            }
            catch (Exception)
            {
                return View("PatientError");
            }
        }

        [Authorize(Roles = "Patient")]
        public ActionResult PatientDetails()
        {
            try
            {

                var user = ((User)Session["User"]);
                if (user == null)
                {
                    return RedirectToAction("LoginPage", "Home");
                }

                var patientData = patientAccess.GetAllPatients();
                var isExisting = patientData.Where(a => a.userId == user.userId).FirstOrDefault();


                if (isExisting == null)
                {
                    return View();
                }
                else
                {

                    var patient = patientData.Where(a => a.userId == user.userId).FirstOrDefault();
                    return View(patient);
                }
            }
            catch (Exception)
            {
                return View("PatientError");
            }
        }

        
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public ActionResult PatientDetails(Patient details, string deseaseCategory)
        {

            try
            {
                var loggedUser = (User)Session["User"];

                details.patient_id = ((loggedUser).userId).ToString()
                    + "_" + (loggedUser).contact_number.ToString();

                details.userId = loggedUser.userId;

                var existingDataOfPatient = patientAccess.GetAllPatients()
                    .Where(a => a.patient_id == details.patient_id)
                    .FirstOrDefault();

                if (ModelState.IsValid)
                {
                    if (existingDataOfPatient != null)
                    {
                        existingDataOfPatient.userId = details.userId;
                        existingDataOfPatient.prev_history = details.prev_history;
                        existingDataOfPatient.reports = details.reports;

                        patientAccess.EditPatientDetails(details.patient_id, existingDataOfPatient);
                    }
                    else
                    {
                        patientAccess.CreatePatient(details);
                    }


                    {
                        var foundInDeseaseDetail = deseaseAccess.GetDeseaseDetails(details.patient_id);

                        TimeSpan time = TimeSpan.Parse(Session["AppointmentTime"].ToString());
                        var appointmentRequest = new Appointment()
                        {
                            patient_id = details.patient_id,
                            appointmentDate = (DateTime)Session["AppointmentDate"],
                            appointmentTime = time,
                            isApproved = false
                        };

                        if (foundInDeseaseDetail == null)
                        {
                            if (deseaseCategory == "")
                            {
                                var doctorId = userAccess.GetAllUsers().Where(a => a.specialization == "General")
                                .Select(a => a.userId)
                                .First();

                                appointmentRequest.userId = doctorId;
                            }
                            else
                            {
                                var doctorId = userAccess.GetAllUsers().Where(a => a.specialization == deseaseCategory)
                                    .Select(a => a.userId)
                                    .First();
                                appointmentRequest.userId = doctorId;
                            }

                        }
                        else
                        {
                            var deseaseDetailOfPatient = deseaseAccess.GetDeseaseDetails(details.patient_id);
                            string category = deseaseDetailOfPatient.desease_catagory;


                            var doctorId = userAccess.GetAllUsers().Where(a => a.specialization == $"{category}")
                                .Select(a => a.userId)
                                .First();

                            appointmentRequest.userId = doctorId;

                            var treatmentDetails = treatmentAccess.GetTreatmentDetails(details.patient_id);
                            treatmentAccess.EditDetails(treatmentDetails);

                        }

                        var appoint = appointmentAccess.CreateAppointment(appointmentRequest);
                        Session.Remove("AppointmentDate");
                        Session.Remove("AppointmentTime");
                    }
                }
                else
                {
                    return View();
                }



                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View("PatientError");
            }
        }

        [Authorize(Roles = "Patient")]
        public ActionResult viewAppointments()
        {
            try
            {
                var user = ((User)Session["User"]);
                var list = appointmentAccess.GetUpcomingAppointments(user);
                return View(list);
            }
            catch (Exception)
            {
                return View("PatientError");
            }

        }

        [Authorize(Roles = "Patient")]
        public ActionResult CurrentTreatment()
        {
            try
            {
                var user = (User)Session["User"];
                var allPatients = patientAccess.GetAllPatients();
                Tuple<IEnumerable<Treatment>, IEnumerable<Consumable>> tuple = null;
                try
                {
                    var patDetails = allPatients.Where(a => a.userId == user.userId).First();
                    var treatmemtTable = treatmentAccess.GetTreatmentDetails(patDetails.patient_id).Where(a => a.IsActive == true);
                    var consumableTable = consumableAccess.GetAllConsumables();

                    tuple = new Tuple<IEnumerable<Treatment>, IEnumerable<Consumable>>(treatmemtTable, consumableTable);
                }
                catch (Exception)
                {

                    return View();
                }


                return View(tuple);
            }
            catch (Exception)
            {
                return View("PatientError");
            }
        }

        [Authorize(Roles = "Patient")]
        public ActionResult ShowBilling()
        {
            try
            {
                var user = (User)Session["User"];
                var patient = patientAccess.GetAllPatients().Where(a => a.userId == user.userId).First();
                var bills = billingAccess.ShowBillsByPatientId(patient.patient_id);
                var consumables = consumableAccess.GetAllConsumables();

                Tuple<IEnumerable<Billing>, IEnumerable<Consumable>> tuple =
                    new Tuple<IEnumerable<Billing>, IEnumerable<Consumable>>(bills, consumables);
                return View(tuple);
            }
            catch (Exception)
            {
                return View("PatientError");
            }
        }

        [Authorize(Roles = "Patient")]
        public ActionResult GetReports()
        {
            try
            {
                var user = (User)Session["User"];
                var reportData = patientAccess.GetReports(user.userId);
                return View(reportData);
            }
            catch (Exception)
            {

                return View();
            }
        }

        [Authorize(Roles = "Patient")]
        public ActionResult PayBill()
        {
            try
            {
                var user = (User)Session["User"];
                var allPatients = patientAccess.GetAllPatients();
                Tuple<IEnumerable<Treatment>, IEnumerable<Consumable>> tuple = null;
                try
                {
                    var patDetails = allPatients.Where(a => a.userId == user.userId).First();
                    var treatmemtTable = treatmentAccess.GetTreatmentDetails(patDetails.patient_id).Where(a => a.IsActive == true);
                    var consumableTable = consumableAccess.GetAllConsumables();

                    tuple = new Tuple<IEnumerable<Treatment>, IEnumerable<Consumable>>(treatmemtTable, consumableTable);
                }
                catch (Exception)
                {

                    return View();
                }


                return View(tuple);
            }
            catch (Exception)
            {

                return View("PatientError");
            }
        }

        
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public ActionResult PayBill(string patientId, int? amount)
        {

            if (Session["isPaid"]==null)
            {
                Session["isPaid"] = true;
            }
            Payment payment = new Payment()
            {
                patientId = patientId,
                total_bill = amount,
                isRequested = false
            };

            try
            {
                var res = paymentAccess.GetPaymentDetails().Where(a => a.patientId == patientId).ToList();

                if (res.Count() != 0)
                {
                    if ((bool)Session["isPaid"] == true)
                    {
                        paymentAccess.AddPaymentDetails(payment);
                        Session["isPaid"] = false;
                        return View("Successful");
                    }
                    else if ((bool)Session["isPaid"] == false)
                    {
                        return View("AlreadyResponded");
                    }

                }
                else
                {
                    paymentAccess.AddPaymentDetails(payment);
                    Session["isPaid"] = false;
                    return View("Successful");
                }
            }
            catch (Exception)
            {

                paymentAccess.AddPaymentDetails(payment);

                return View("Successful");
            }

            return View("AlreadyResponded");
        }

    }
}