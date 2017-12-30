using System.Collections.Generic;
using Xamarin.Forms;

namespace DPF.Models
{
    public class NetworkConnectionModel
    {
        private INetworkConnectionService _service;

        public event ErrorOccurredDelegate ErrorOccured;

        public NetworkConnectionModel()
        {
            _service = DependencyService.Get<INetworkConnectionService>();
            _service.ErrorOccured += ErrorOccuredEventHandler;
        }

        public bool CheckIfNetworkConnected()
        {
            return _service.CheckIfNetworkConnected();
        }

        private void ErrorOccuredEventHandler(object sender, string errorMessage)
        {
            ErrorOccured?.Invoke(this, errorMessage);
        }
    }
}