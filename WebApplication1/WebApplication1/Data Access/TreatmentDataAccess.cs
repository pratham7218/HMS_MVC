using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Data_Access
{
    public class TreatmentDataAccess
    {
        HMS_Project_newEntities2 entities = new HMS_Project_newEntities2();

        public void AddDetails(Treatment treatment)
        {
            var res = entities.Treatments.Add(treatment);
            entities.SaveChanges();
        }

        public IEnumerable<Treatment> GetTreatmentDetails(string patId)
        {
            var data = entities.Treatments.Where(a => a.patient_id == patId).ToList();

        

            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }
        public void EditDetails(IEnumerable<Treatment> treatments)
        {
            foreach (var treatment in treatments)
            {
                var oldTreatment = entities.Treatments.Where(a => a.treatment_id == treatment.treatment_id).First();
                oldTreatment.quantity = treatment.quantity;
                oldTreatment.patient_id = treatment.patient_id;
                oldTreatment.consumable_id = treatment.consumable_id;
                oldTreatment.IsActive = false;

                entities.SaveChanges();

            }
        }
    }
}