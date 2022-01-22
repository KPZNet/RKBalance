using RKBalance.ViewModels;
using System;
using Xamarin.Forms;

namespace RKBalance.Views
{
    public partial class VectorPage : ContentPage
    {
        public VectorPage()
        {
            InitializeComponent();
            BindingContext = new SinglePlaneBalanceRunViewModel();
        }

        public void SetActivityIndicator(bool activity)
        {
            ActionIndicator.IsVisible = activity;
        }

        protected void CancelCallInProgress(object sender, EventArgs args)
        {
            ActionIndicator.IsVisible = !ActionIndicator.IsVisible;
        }

        protected void ShowInput(object sender, EventArgs args)
        {
            ActionIndicator.IsVisible = !ActionIndicator.IsVisible;
        }
    }
}