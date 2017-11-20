﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAA.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AAA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevicesListPage : ContentPage
    {
        public DevicesListPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}