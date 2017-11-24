using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class ConfirmDeleteFolderViewModel
    {
        public ConfirmDeleteFolderViewModel(int folderId)
        {
            FolderId = folderId;
        }

        public ConfirmDeleteFolderViewModel(int folderId, string name)
        {
            FolderId = folderId;
            Name = name;
        }

        public int FolderId { get; set; }
        public String Name { get; set; }
    }
}