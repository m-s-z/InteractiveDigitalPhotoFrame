using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// cloud view model
    /// </summary>
    public class CloudViewModel
    {
        /// <summary>
        /// constructor with CloudViewModel
        /// </summary>
        /// <param name="clouds"></param>
        public CloudViewModel(List<Cloud> clouds)
        {
            Clouds = clouds;
        }
        #region properties
        /// <summary>
        /// List of cloud model class to be displayed
        /// </summary>
        public List<Cloud> Clouds { get; set; }
        #endregion properties
    }
}