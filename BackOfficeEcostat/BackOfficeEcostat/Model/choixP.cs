using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeEcostat.Model
{
    partial class choix
    {
        private static ecostatbddEntities db = new ecostatbddEntities();

        public choix(string c, question q)
        {
            try
            {
                Id = db.choixes.OrderByDescending(ch => ch.Id).FirstOrDefault().Id + 1;
            }
            catch 
            {

                Id = 1;
            }

            contenu = c;
            personnes = new List<personne>();
        }
    }
}
