using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary
{
    /// <summary>
    /// autorization response for AAA requests
    /// </summary>
    public enum AuthorizationResponse
    {
        /// <summary>
        /// token was valid
        /// </summary>
        Ok,
        /// <summary>
        /// token invalid
        /// </summary>
        InvalidToken,
        /// <summary>
        /// token has expired
        /// </summary>
        TokenExpired
    }
}
