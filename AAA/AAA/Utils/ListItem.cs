using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDPFLibrary;

namespace AAA.Utils
{
    public class ListItem : ViewModelBase
    {
        private string _s1;
        private string _s2;

        public string S1
        {
            get => _s1;
            set => SetProperty(ref _s1, value);
        }

        public ListItem(string s1, string s2 = null)
        {
            S1 = s1;
        }
    }
}
