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
    /// DevicesListPage class.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevicesListPage : ContentPage
    {
        /// <summary>
        /// DevicesListPage class constructor.
        /// </summary>
        /// <param name="viewModel">View model to use as a binding context.</param>
        public DevicesListPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}