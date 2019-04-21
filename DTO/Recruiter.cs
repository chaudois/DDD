using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Recruiter : Actor
    {
        public readonly string poste;
        public readonly string departement;
        public Recruiter(int id, string lastname, string firstname, string poste, string departement) : base(id, lastname, firstname)
        {
            this.poste = poste;
            this.departement = departement;
        }
        public override bool Equals(object obj)
        {
            if(((Recruiter)obj).id==this.id && 
                ((Recruiter)obj).firstname==this.firstname && 
                ((Recruiter)obj).lastname==this.lastname && 
                ((Recruiter)obj).poste==this.poste && 
                ((Recruiter)obj).departement==this.departement
                )
            {
                return true;
            }
            return false;
        }
    }
}
