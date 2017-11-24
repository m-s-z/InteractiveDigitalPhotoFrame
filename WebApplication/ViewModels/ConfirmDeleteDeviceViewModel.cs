using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class ConfirmDeleteDeviceViewModel
    {
        public ConfirmDeleteDeviceViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public String Name { get; set; }
    }
}