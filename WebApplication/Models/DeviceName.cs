using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DeviceName
    {
        public DeviceName()
        {

        }
        public Account Account { get; set; }
        public Device Device { get; set; }
        public String CustomDeviceName { get; set; }
    }
}