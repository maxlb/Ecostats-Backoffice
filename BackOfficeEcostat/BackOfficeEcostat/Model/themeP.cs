using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeEcostat.Model
{
    public partial class theme
    {
        private static ecostatbddEntities db = new ecostatbddEntities();

        public static List<theme> getAll()
        {
            List<theme> ths = new List<theme>();
            foreach (theme th in db.themes)
            {
                ths.Add(th);
            }
            return ths;
        }

        public theme(string n)
        {
            nom = n;
            theme1 = new List<theme>();
            theme2 = db.themes.Find(1);
            questionnaires = new List<questionnaire>();
            db.themes.Add(this);
            db.SaveChanges();

        }

        public theme(string n, int ID_ThemeParent)
        {
            nom = n;
            theme2 = db.themes.Find(ID_ThemeParent);
            db.themes.Add(this);
            db.themes.Find(ID_ThemeParent).theme1.Add(this);
            db.SaveChanges();
        }
    }
}
