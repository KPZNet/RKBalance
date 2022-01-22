using RKBalance.Models;
using RKBalance.Services;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;

namespace RKBalance.Views
{
    public partial class TrialWeightPage : ContentPage, IVectorGraphPainter
    {
        private SinglePlaneBalanceVectorTools BPTools = new SinglePlaneBalanceVectorTools();
        private SPBalanceRun SPB;
        private bool Added = false;

        public TrialWeightPage()
        {
            SPB = ((App)Application.Current).spbData;
            BindingContext = SPB;

            InitializeComponent();
            //Assigning image from Resource
            RotorKit.Source = ImageSource.FromResource("RKBalance.RK4_1.png");
            NextButton.IsEnabled = false;
            VGraph.SetDIPainter(this);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VGraph.PushDraw();
        }

        protected void AddTrialWeight(object sender, EventArgs args)
        {
            Added = true;
            NextButton.IsEnabled = true;
            VGraph.PushDraw();
        }

        async protected void Back(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        async protected void Next(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new InfluenceVectorPage()));
        }

        void IVectorGraphPainter.OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {           
            if (Added) BPTools.DrawTrialWeight(args.Info, args.Surface.Canvas, ((App)Application.Current).spbData);
            else BPTools.DrawInitialVector(args.Info, args.Surface.Canvas, ((App)Application.Current).spbData);
        }
    }
}