using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class NewFolderViewModel
    {
        public NewFolderViewModel()
        {
                
        }
        public NewFolderViewModel(int deviceId)
        {
            this.DeviceId = deviceId;
        }

        public int DeviceId { get; set; }
    }
}