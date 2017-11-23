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

        public int FolderId { get; set; }
    }
}