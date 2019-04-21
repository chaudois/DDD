using System;
using System.Runtime.Serialization;

namespace agregat
{
    [Serializable]
    internal class RoomOccupiedException : Exception
    {
        public override string Message => "Cette salle est déja occupé pour cet horaire";

    }
}