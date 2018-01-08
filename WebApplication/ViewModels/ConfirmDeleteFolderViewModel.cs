using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    /// <summary>
    /// Confirm delete folder view model class
    /// </summary>
    public class ConfirmDeleteFolderViewModel
    {
        /// <summary>
        /// constructor for ConfirmDeleteFolderViewModel class
        /// </summary>
        /// <param name="folderId">id</param>
        public ConfirmDeleteFolderViewModel(int folderId)
        {
            FolderId = folderId;
        }
        /// <summary>
        /// constructor for ConfirmDeleteFolderViewModel class
        /// </summary>
        /// <param name="folderId">id</param>
        /// <param name="name">name</param>
        public ConfirmDeleteFolderViewModel(int folderId, string name)
        {
            FolderId = folderId;
            Name = name;
        }
        #region properties
        /// <summary>
        /// folder id to be removed
        /// </summary>
        public int FolderId { get; set; }
        /// <summary>
        /// name of folder to be removed, only for display the folder is being removed based on id
        /// </summary>
        public String Name { get; set; }
        #endregion properties
    }
}