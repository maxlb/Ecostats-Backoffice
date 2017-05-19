using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeEcostat.Model
{
    partial class questionnaire
    {
        private static ecostatbddEntities db = new ecostatbddEntities();

        public questionnaire(string t, string d, theme th, enquete e, int nbQ, bool dispo)
        {
            Titre = t;
            Description = d;
            Disponible = dispo;
            theme = db.themes.Find(th.Id);
            enquete1 = e;
            Id_Theme = th.Id;
            questions = new List<question>(nbQ);
            sondages = new List<sondage>();
            questionnairecompletes = new List<questionnairecomplete>();
            db.questionnaires.Add(this);
            db.SaveChanges();
        }

        public questionnaire(string t, string d, theme th, int nbQ)
        {
            Titre = t;
            Description = d;
            theme = db.themes.Find(th.Id);

            sondages = new List<sondage>(1);
            Id_Theme = th.Id;
            questions = new List<question>(nbQ);
            questionnairecompletes = new List<questionnairecomplete>();

            db.questionnaires.Add(this);
            db.SaveChanges();
        }
    }
}
