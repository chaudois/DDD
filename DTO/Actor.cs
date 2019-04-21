using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Actor
    {
        public readonly string lastname;
        public readonly string firstname;
        public readonly int id;

        public Actor(int id,string lastname, string firstname)
        {
            this.lastname = lastname;
            this.firstname = firstname;
            this.id = id;
        }
    }
}
