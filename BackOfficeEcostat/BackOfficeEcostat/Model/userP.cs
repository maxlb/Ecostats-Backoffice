using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeEcostat.Model
{
    partial class user
    {
        public user(string login, string mdp, string mail)
        {
            this.pseudo = login;
            this.mdp = mdp;
            this.mail = mail;
            this.personne = new personne();
        }
    }
}
