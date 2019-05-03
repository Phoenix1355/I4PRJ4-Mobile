using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.ViewModels;
using Xamarin.Forms;

namespace i4prj.SmartCab.Views
{
	public partial class CreateRidePage : ContentPage
    {
		public CreateRidePage ()
		{
			InitializeComponent();
		}

        private void Entry_OnCompleted(object sender, EventArgs e)
        {
            CreateRideViewModel b = (CreateRideViewModel)MainPage.BindingContext;

            b.CalculatePriceCommand.Execute();
            
        }
    }
}