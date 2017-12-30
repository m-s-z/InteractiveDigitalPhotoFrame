namespace DPF.Models
{
    public interface INetworkConnectionService
    {
        event ErrorOccurredDelegate ErrorOccured;

        bool CheckIfNetworkConnected();
    }
}