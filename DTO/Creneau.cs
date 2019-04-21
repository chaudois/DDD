using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Creneau
    {
        public  DateTime heureDebut;
        public  DateTime HeureFin;
        public  int id;
        public  Salle salle;

        public Status status { get => status; set => status = value; }
        public Creneau(int id,DateTime dateDebut, DateTime dateFin, Salle salle, int status )
        {
            this.status = (Status)status;
            this.salle = salle;
            if (dateDebut == dateFin)
            {
                throw new TooShortDuration();
            }
            if (dateDebut > dateFin)
            {
                throw new IncoherantDuration();
            }
            this.heureDebut = dateDebut;
            this.HeureFin = dateFin;
            this.id = id;
        }


        public override bool Equals(object creneau)
        {
            if (((Creneau)creneau).heureDebut.Date.ToString().Equals(this.heureDebut.Date.ToString())
                && ((Creneau)creneau).HeureFin.Date.ToString().Equals(this.HeureFin.Date.ToString()))
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return 1; 
        }
    }
}
