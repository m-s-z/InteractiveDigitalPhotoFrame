using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// Confirm delete cloud view model class
    /// </summary>
    public class ConfirmDeleteCloudViewModel
    {
        /// <summary>
        /// constructor for ConfirmDeleteCloudViewModel
        /// </summary>
        /// <param name="cloudId"></param>
        public ConfirmDeleteCloudViewModel(int cloudId)
        {
            CloudId = cloudId;
        }
        #region properties
        /// <summary>
        /// cloud id to be deleted
        /// </summary>
        public int CloudId { get; set; }
        #endregion properties
    }
}