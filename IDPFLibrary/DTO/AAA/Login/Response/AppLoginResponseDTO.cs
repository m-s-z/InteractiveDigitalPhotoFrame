﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO.AAA.Login.Response
{
    /// <summary>
    /// class for response for AppLogin controller method
    /// </summary>
    public class AppLoginResponseDTO
    {
        #region properties
        /// <summary>
        /// string response message
        /// </summary>
        public string Message { get; set; }
        #endregion properties
        /// <summary>
        /// constructor for AppLoginResponseDTO class
        /// </summary>
        public AppLoginResponseDTO()
        {

        }
    }
}
