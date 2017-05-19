using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeEcostat.Model
{
    partial class sondage
    {
        private static ecostatbddEntities db = new ecostatbddEntities();

        public sondage()
        {

        }

        public static List<sondage> getAll()
        {
            List<sondage> sdgs = new List<sondage>();
            foreach (sondage sd in db.sondages)
            {
                sdgs.Add(sd);
            }
            return sdgs;
        }

        public sondage(int rem, questionnaire q)
        {
            Remuneration = rem;
            questionnaire = db.questionnaires.Find(q.Id);

            db.sondages.Add(this);
            db.SaveChanges();
        }
    }
}
