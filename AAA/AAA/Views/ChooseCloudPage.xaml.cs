using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAA.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AAA.Views
{
    /// <summary>
    /// ChooseCloudPage class.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseCloudPage : ContentPage
    {
        /// <summary>
        /// ChooseCloudPage class constructor.
        /// </summary>
        /// <param name="viewModel">View model to use as a binding context.</param>
        public ChooseCloudPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}