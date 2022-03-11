using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGrow_simulator.Class
{
    public class Error
    {
        public string ErrorCode { get; set; }
        public string ErrorNotification { get; set; }
        public string IdSensor { get; set; }

        public Error()
        {

        }

        public Error(string errCode, string errNoty, string idSen)
        {
            ErrorCode = errCode;
            ErrorNotification = errNoty;
            IdSensor = idSen;
        }

        public override string ToString()
        {
            return $"Error code: {ErrorCode} | Error notification: {ErrorNotification} | IdSensor: {IdSensor}";
        }
    }
}
