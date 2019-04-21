using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Candidate:Actor
    {

        public Uri CVPath;



        public Candidate(int id, string lastname, string firstname, Uri CVPath) : base(id, lastname, firstname)
        {
            this.CVPath = CVPath;
        }
    }
}
