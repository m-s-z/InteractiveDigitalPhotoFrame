using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class ConfirmDeleteCloudViewModel
    {
        public ConfirmDeleteCloudViewModel(int cloudId)
        {
            CloudId = cloudId;
        }

        public int CloudId { get; set; }
    }
}