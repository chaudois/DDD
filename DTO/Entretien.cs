using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Entretien
    {
        string message;
        public List<Recruiter>recruiter;
        public Candidate candidate;
        public Creneau creneau;
        public int id;
        public Entretien(int id, List<Recruiter> recruiter, Candidate candidate, Creneau creneau, string message)
        {
            this.id = id;
            this.recruiter = recruiter;
            this.candidate = candidate;
            this.creneau = creneau;
            this.message = message;
        }
        public void confirm()
        {
            if (creneau.status == Status.Reserved)
                creneau.status++;
        }
        public void cancel(string reason)
        {
            creneau.status = Status.canceled;
            message = reason == "" ? message:reason;
        }
    }
}
