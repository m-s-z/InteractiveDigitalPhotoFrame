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
        private bool _isActive;
        private bool _isSlideshow;
        private int _slideshowCounter;
        public string PhotoPath
        {
            get => _photoPath;
            set => SetProperty(ref _photoPath, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        public bool IsSlideshow
        {
            get => _isSlideshow;
            set => SetProperty(ref _isSlideshow, value);
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

        public Command TapToActiveCommand
        {
            get;
            set;
        }
        public Command ControlSlideshowCommand
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
            TapToActiveCommand = new Command(ExecuteTapToActiveCommand);
            PreviousPhotoCommand = new Command(ExecutePreviousPhotoCommand);
            NextPhotoCommand = new Command(ExecuteNextPhotoCommand);
            ControlSlideshowCommand = new Command(ExecuteControlSlideshowCommand);
        }

        private void ExecuteNextPhotoCommand()
        {
            _slideshowCounter = 0;
            _photoCounter++;
            if (_photoCounter > 2)
            {
                _photoCounter = 0;
            }
            PhotoPath = ListOfImageNames[_photoCounter];
        }

        private void ExecutePreviousPhotoCommand()
        {
            _slideshowCounter = 0;
            _photoCounter--;
            if (_photoCounter < 0)
            {
                _photoCounter = 2;
            }
            PhotoPath = ListOfImageNames[_photoCounter];
        }

        private void ExecuteTapToActiveCommand()
        {
            IsActive = !IsActive;
        }

        private void ExecuteControlSlideshowCommand()
        {
            IsSlideshow = !IsSlideshow;

            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                if (IsSlideshow)
                {
                    _slideshowCounter++;

                    if (_slideshowCounter >= 30)
                    {
                        ExecuteNextPhotoCommand();
                    }
                }
                else
                {
                    _slideshowCounter = 0;
                }

                return IsSlideshow;
            });
        }

    }
}
