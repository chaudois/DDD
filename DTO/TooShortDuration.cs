using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class TooShortDuration :Exception
    {
        public override string Message => "la durée ne doit etre plus grande que zero!";
    }
}
