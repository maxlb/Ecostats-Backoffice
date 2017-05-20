using BackOfficeEcostat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeEcostat
{
    public class Controller
    {
        private static ecostatbddEntities db = new ecostatbddEntities();

        /// <summary>
        /// Renvoie un boolean si l'utilisateur peut se connecter
        /// </summary>
        /// <param name="Identifiant">User</param>
        /// <param name="MotDePasse">Password</param>
        /// <returns></returns>
        public bool CanLogin(string Identifiant,string MotDePasse)
        {
            return db.users.Any(u => u.pseudo == Identifiant && u.mdp == MotDePasse);
        }

        public theme AddThemeAlone(string n)
        {
            try
            {
                return db.themes.Where(t => t.nom == n).First();
            }
            catch
            {
                return new theme(n);
            }
        }

        public theme AddChildrenTheme(string n, int ID)
        {
            return new theme(n, ID);
        }

        public List<string> getAllNomsThemes()
        {
            List<string> nomsThemes = new List<string>();
            foreach (var theme in theme.getAll())
            {
                nomsThemes.Add(theme.nom);
            }
            return nomsThemes;
        }

        public enquete getEnqueteById(int e)
        {
            return db.enquetes.Find(e);
        }

        public List<questionnaire> getAllSondages()
        {
            List<questionnaire> AllSondages = new List<questionnaire>();
            foreach (sondage son in sondage.getAll())
            {
                AllSondages.Add(getQuestionnaireBySondage(son));
            }
            return AllSondages;
        }

        public List<enquete> getAllEnquetes()
        {
            List<enquete> AllEnquetes = new List<enquete>();
            foreach (enquete enq in db.enquetes)
            {
                AllEnquetes.Add(enq);
            }
            return AllEnquetes;
        }


        public questionnaire getQuestionnaireById(int q)
        {
            return db.questionnaires.Find(q);
        }

        public questionnaire UpdateSequence(int id, string t, string d, string th, int nbQ, bool dispo)
        {
            questionnaire q = db.questionnaires.Find(id);
            q.Titre = t;
            q.Description = d;
            q.theme = db.themes.Where(the => the.nom == th).FirstOrDefault();
            q.Id_Theme = db.themes.Where(the => the.nom == th).FirstOrDefault().Id;
            q.questions = new List<question>(nbQ);
            foreach (question que in getAllQuestionOfQuestionnaire(id))
            {
                q.questions.Add(que);
            }
            q.Disponible = dispo;
            db.SaveChanges();
            return q;
        }

        public List<question> getAllQuestionOfQuestionnaire(int ID)
        {
            List<question> AllQuestions = new List<question>();
            foreach (question question in db.questions.Where(q => q.Id_Questionnaire == ID))
            {
                AllQuestions.Add(question);
            }
            return AllQuestions;
        }

        public questionnaire AddSequence(string t, string d, string th, int nbQ, bool dispo, enquete enq)
        {
            return new questionnaire(t, d, db.themes.Where(the => the.nom == th).FirstOrDefault(), db.enquetes.Find(enq.Id) ,nbQ, dispo);
        }

        public List<choix> getAllChoixOfQuestion(int ID)
        {
            List<choix> AllChoix = new List<choix>();
            foreach (choix choix in db.choixes.Where(ch => ch.Id_Question == ID))
            {
                AllChoix.Add(choix);
            }
            return AllChoix;
        }

        public sondage AddSondage(string t, string d, string th, int rem, int nbQ, bool dispo)
        {
            questionnaire q = AddQuestionnaire(t, d, th, nbQ);
            q.Disponible = dispo;
            sondage s = new sondage(rem, q);
            q.sondages.Add(s);
            return s;
        }

        public sondage UpdateSondage(int id, string t, string d, string th, int rem, int nbQ, bool dispo)
        {
            questionnaire q = getQuestionnaireById(id);
            q.Titre = t;
            q.Description = d;
            q.theme = db.themes.Where(the => the.nom == th).FirstOrDefault();
            q.Id_Theme = db.themes.Where(the => the.nom == th).FirstOrDefault().Id;
            q.questions = new List<question>(nbQ);
            q.Disponible = dispo;
            foreach (question question in getAllQuestionOfQuestionnaire(id))
            {
                q.questions.Add(question);
            }

            sondage s = getSondageByQuestionnaire(id);
            s.Remuneration = rem;
            db.SaveChanges();
            return s;
        }

        public enquete UpdateEnqueteWithThemeWithThemeParent(int id, string t, string d, string th, string th1, int nbQ, bool dispo)
        {
            theme theme = AddThemeAlone(th1);
            theme.theme2 = db.themes.Where(them => them.nom == th).FirstOrDefault();
            db.themes.Find(db.themes.Where(them => them.nom == th).FirstOrDefault().Id).theme1.Add(theme);

            enquete UpdatedEnquete = db.enquetes.Find(id);
            UpdatedEnquete.titre = t;
            UpdatedEnquete.description = d;
            UpdatedEnquete.theme = db.themes.Where(the => the.nom == th).FirstOrDefault();
            UpdatedEnquete.Id_Theme = db.themes.Where(the => the.nom == th).FirstOrDefault().Id;
            UpdatedEnquete.disponible = dispo;
            UpdatedEnquete.questionnaires = new List<questionnaire>(nbQ);
            foreach (questionnaire questionnaire in getAllQuestionnaireOfEnquete(id))
            {
                UpdatedEnquete.questionnaires.Add(questionnaire);
            }

            db.SaveChanges();
            return UpdatedEnquete;
        }

        public enquete AddEnqueteWithThemeWithThemeParent(string t, string d, string th, string th1, int nbQ, bool dispo)
        {
            theme theme = AddChildrenTheme(th1, db.themes.Where(the => the.nom == th).FirstOrDefault().Id);
            return new enquete(t, d, th, nbQ, dispo);

        }

        public enquete AddEnquete(string t, string d, string th, int nbQ, bool dispo)
        {
            return new enquete(t, d, th, nbQ, dispo);
        }

        public enquete UpdateEnquete(int id, string t, string d, string th, int nbQ, bool dispo)
        {
            enquete UpdatedEnquete = db.enquetes.Find(id);
            UpdatedEnquete.titre = t;
            UpdatedEnquete.description = d;
            UpdatedEnquete.theme = db.themes.Where(the => the.nom == th).FirstOrDefault();
            UpdatedEnquete.Id_Theme = db.themes.Where(the => the.nom == th).FirstOrDefault().Id;
            UpdatedEnquete.disponible = dispo;
            UpdatedEnquete.questionnaires = new List<questionnaire>(nbQ);
            foreach (questionnaire questionnaire in getAllQuestionnaireOfEnquete(id))
            {
                UpdatedEnquete.questionnaires.Add(questionnaire);
            }

            db.SaveChanges();
            return UpdatedEnquete;
        }

        public List<questionnaire> getAllQuestionnaireOfEnquete(int id)
        {
            List<questionnaire> qs = new List<questionnaire>();
            foreach (questionnaire q in db.questionnaires.Where(que => que.Id_enquete == id))
            {
                qs.Add(q);
            }
            return qs;
        }

        public void UpdateDiponibiliteQuestionnaire(int id, bool dispo)
        {
            questionnaire q = getQuestionnaireById(id);
            q.Disponible = dispo;
            db.SaveChanges();
        }

        public choix UpdateChoix(int id, string unChoix)
        {
            choix choix = db.choixes.Find(id);
            choix.contenu = unChoix;

            db.SaveChanges();
            return choix;
        }

        public question UpdateQuestionOfSondage(int idQ, string text)
        {
            question UpdatedQuestion = db.questions.Find(idQ);
            UpdatedQuestion.contenu = text;
            db.SaveChanges();
            return UpdatedQuestion;
        }

        public sondage UpdateSondageWithThemeWithThemeParent(int id, string t, string d, string th1, string th, int rem, int nbQ, bool dispo)
        {
            theme theme = AddThemeAlone(th1);
            theme.theme2 = db.themes.Where(them => them.nom == th).FirstOrDefault();
            db.themes.Find(db.themes.Where(them => them.nom == th).FirstOrDefault().Id).theme1.Add(theme);

            questionnaire q = getQuestionnaireById(id);
            q.Titre = t;
            q.Description = d;
            q.theme = db.themes.Where(the => the.nom == th1).FirstOrDefault();
            q.Id_Theme = db.themes.Where(the => the.nom == th1).FirstOrDefault().Id;
            q.questions = new List<question>(nbQ);
            q.Disponible = dispo;
            foreach (question question in getAllQuestionOfQuestionnaire(id))
            {
                q.questions.Add(question);
            }

            sondage s = getSondageByQuestionnaire(id);
            s.Remuneration = rem;
            db.SaveChanges();
            return s;
        }

        public sondage getSondageByQuestionnaire(int id)
        {
            return db.sondages.Where(s => s.Id_questionnaire == id).FirstOrDefault();
        }

        public sondage AddSondageWithThemeWithThemeParent(string t, string d, string th1, string th, int rem, int nbQ, bool dispo)
        {
            theme theme = AddChildrenTheme(th1, db.themes.Where(the => the.nom ==  th).FirstOrDefault().Id);
            questionnaire q = AddQuestionnaire(t, d, th1, nbQ);
            q.Disponible = dispo;
            sondage s = new sondage(rem, q);
            q.sondages.Add(s);
            db.SaveChanges();
            return s;
        }

        public sondage AddSondageWithTheme(string t, string d, string th, int rem, int nbQ, bool dispo)
        {
            theme theme = AddThemeAlone(th);
            questionnaire q = AddQuestionnaire(t, d, th, nbQ);
            q.Disponible = dispo;
            sondage s = new sondage(rem, q);
            q.sondages.Add(s);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            return s;
        }

        public questionnaire AddQuestionnaire(string t, string d, string th, int nbQ)
        {
            return new questionnaire(t, d, db.themes.Where(the => the.nom == th).FirstOrDefault(), nbQ);
        }

        public question AddQuestionOfSondage(questionnaire q, string c)
        {
            return new question(c, q);
        }

        public choix AddChoix(question q, string c)
        {
            choix choix = new choix(c, q);
            db.choixes.Add(choix);
            db.SaveChanges();

            question qu = db.questions.Find(q.Id);
            qu.choixes.Add(choix);
            choix.Id_Question = qu.Id;
            db.Entry(qu).State = System.Data.Entity.EntityState.Modified;
            db.Entry(choix).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return choix;
        }

        public questionnaire getQuestionnaireBySondage(sondage s)
        {
            return db.questionnaires.Find(s.Id_questionnaire);
        }


    }
}
