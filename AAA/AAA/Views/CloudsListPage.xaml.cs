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
    /// CloudsListPage class.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CloudsListPage : ContentPage
    {
        /// <summary>
        /// CloudsListPage class constructor.
        /// </summary>
        /// <param name="viewModel">View model to use as a binding context.</param>
        public CloudsListPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}