using IDPFLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DPF.ViewModels
{
    public class MainDPFViewModel : ViewModelBase
    {
        private List<String> _listOfImageNames;
        private int _photoCounter;
        private string _photoPath;

        public string PhotoPath
        {
            get => _photoPath;
            set => SetProperty(ref _photoPath, value);
        }

        public List<String> ListOfImageNames
        {
            get;
            set;
        }

        public Command NextPhotoCommand
        {
            get;
            set;
        }

        public Command PreviousPhotoCommand
        {
            get;
            set;
        }

        public MainDPFViewModel()
        {
            ListOfImageNames = new List<string>()
            {
                "dpf_mock_background.png",
                "mock_2.jpg",
                "mock_3.jpg"
            };
            PhotoPath = ListOfImageNames[0];
            _photoCounter = 0;
            PreviousPhotoCommand = new Command(ExecutePreviousPhotoCommand);
            NextPhotoCommand = new Command(ExecuteNextPhotoCommand);
        }

        private void ExecuteNextPhotoCommand()
        {
            _photoCounter++;
            if (_photoCounter > 2)
            {
                _photoCounter = 0;
            }
            PhotoPath = ListOfImageNames[_photoCounter];
        }

        private void ExecutePreviousPhotoCommand()
        {
            _photoCounter--;
            if (_photoCounter < 0)
            {
                _photoCounter = 2;
            }
            PhotoPath = ListOfImageNames[_photoCounter];
        }


    }
}
