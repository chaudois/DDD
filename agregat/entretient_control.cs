using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using repository;
namespace agregat
{
    public class entretient_control
    {
        SQLManager sQLManager;
        public entretient_control()
        {
            sQLManager = new SQLManager();
        }

        public Entretien CreateEntretient(List<Recruiter> recruiters, Candidate candidate, Creneau _creneau)
        {


            return sQLManager.CreateEntretient(recruiters, candidate, _creneau);
        }

        public IEnumerable<Entretien> getAllActiveEntretient()
        {
            return sQLManager.GetEntretients();
        }

        public IEnumerable<Candidate> GetCandidats()
        {
            return sQLManager.GetCandidats();
        }

        public Creneau getCreneauInAnyRoom(DateTime debut, DateTime fin)
        {
            foreach (Salle salle in sQLManager.getAllRooms())
            {
                bool taken = false;
                foreach (var creneau in sQLManager.getAllActiveCreaneau())
                {
                    if ((creneau.salle.Equals(salle))
                        && (creneau.heureDebut < debut && creneau.HeureFin > debut)) taken = true; ;

                }
                if (!taken)
                {
                    return sQLManager.SaveCreneau(debut, fin, salle);

                }
            }
            throw new NoRoomAvailable();
        }

        public IEnumerable<Salle> GetSalles()
        {
            return sQLManager.GetSalles();
        }

        public IEnumerable<Recruiter> GetRecruteurs()
        {
            return sQLManager.GetRecruteurs();
        }

        public Creneau getCreneau(DateTime debut, DateTime fin, string salleName)
        {
            bool taken = false;
            Salle salle = sQLManager.getSalleFromName(salleName);
            foreach (var creneau in sQLManager.getAllActiveCreaneau())
            {
                if ((creneau.salle.Equals(salle))
                    && (creneau.heureDebut < debut && creneau.HeureFin > debut)) taken = true; ;

            }
            if (!taken)
            {
                return sQLManager.SaveCreneau(debut, fin, salle);

            }
            throw new SpecificRoomNotAvailable();
        }

        public Recruiter GetRecruteurByName(string firstname, string lastname)
        {
            return sQLManager.GetRecruteurByName(firstname, lastname);
        }

        public Candidate GetCandidatByName(string firstname, string lastname)
        {
            return sQLManager.GetCandidatByName(firstname, lastname);
        }
    }
}

