using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeEcostat.Model
{
    partial class enquete
    {
        private static ecostatbddEntities db = new ecostatbddEntities();

        public enquete(string t, string d, string th, int nbQ, bool dispo)
        {
            titre = t;
            description = d;
            theme = db.themes.Where(the => the.nom == th).FirstOrDefault();
            questionnaires = new List<questionnaire>(nbQ);
            disponible = dispo;
            Id_Theme = db.themes.Where(the => the.nom == th).FirstOrDefault().Id;

            db.enquetes.Add(this);
            db.SaveChanges();
        }
    }
}
