using RKBalance.Models;
using RKBalance.Services;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;

namespace RKBalance.Views
{
    public partial class InitialRunPage : ContentPage, IVectorGraphPainter
    {
        private SPBalanceRun SPB;
        private SinglePlaneBalanceVectorTools BPTools = new SinglePlaneBalanceVectorTools();
        private bool Added = false;

        public InitialRunPage()
        {
            SPB = ((App)Application.Current).spbData;
            BindingContext = SPB;

            InitializeComponent();

            NextButton.IsEnabled = false;

            //Assigning image from Resource
            RotorKit.Source = ImageSource.FromResource("RKBalance.RK4_1.png");

            VGraph.SetDIPainter(this);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VGraph.PushDraw();
        }

        protected void AddInitialRunVector(object sender, EventArgs args)
        {
            Added = true;
            NextButton.IsEnabled = true;
            VGraph.PushDraw();
        }

        async protected void Next(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new TrialWeightPage()));
        }

        void IVectorGraphPainter.OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            if (Added) BPTools.DrawInitialVector(args.Info, args.Surface.Canvas, ((App)Application.Current).spbData);
            else BPTools.DrawGraph(args.Info, args.Surface.Canvas);
        }
    }
}