using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Salle
    {
        public int id;
        public string name;


        public Salle(int id,string name)
        {
            this.id = id;
            this.name = name;
        }

        public override bool Equals(object obj)
        {
            if(((Salle)obj).id==this.id && ((Salle)obj).name==this.name)
                return true;
            return false;
        }
    }
}
