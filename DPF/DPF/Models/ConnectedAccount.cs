using System.Collections.ObjectModel;

namespace DPF.Models
{
    public class ConnectedAccount
    {
        private int _accountId;
        private string _accountLogin;
        private ObservableCollection<AccountCloud> _cloudsCollection;
    }
}