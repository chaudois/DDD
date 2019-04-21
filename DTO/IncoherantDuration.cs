using System;
using System.Runtime.Serialization;

namespace DTO
{
    [Serializable]
    internal class IncoherantDuration : Exception
    {
        public override string Message => "la date de fin est inferieur à la date de debut";

    }
}