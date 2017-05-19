using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeEcostat.Model
{
    partial class question
    {
        private static ecostatbddEntities db = new ecostatbddEntities();

        public question(string c, questionnaire q)
        {
            questionnaire = db.questionnaires.Find(q.Id);
            contenu = c;
            choixes = new List<choix>();
            try
            {
                Id = db.questions.OrderByDescending(quest => quest.Id).FirstOrDefault().Id + 1;
            }
            catch
            {
                Id = 0;
            }
            

            db.questions.Add(this);
            db.SaveChanges();
        }
    }
}
