using System;
using System.Runtime.Serialization;

namespace agregat
{
    [Serializable]
    public class SpecificRoomNotAvailable : Exception
    {
        public override string Message => "Room no available";
    }
}