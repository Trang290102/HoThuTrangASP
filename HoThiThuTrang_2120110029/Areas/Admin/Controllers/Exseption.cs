using System;
using System.Runtime.Serialization;

namespace HoThiThuTrang_2120110029.Areas.Admin.Controllers
{
    [Serializable]
    internal class Exseption : Exception
    {
        public Exseption()
        {
        }

        public Exseption(string message) : base(message)
        {
        }

        public Exseption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Exseption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}