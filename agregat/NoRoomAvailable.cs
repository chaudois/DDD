using System;
using System.Runtime.Serialization;

namespace agregat
{
    [Serializable]
    public class NoRoomAvailable : Exception
    {
        public override string Message => "aucune salle disponible pour cet horaire";
    }
}