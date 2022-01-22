using RKBalance.Models;
using RKBalance.Services;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;

namespace RKBalance.Views
{
    public partial class InfluenceVectorPage : ContentPage, IVectorGraphPainter
    {
        private SPBalanceRun SPB;
        private SinglePlaneBalanceVectorTools BPTools = new SinglePlaneBalanceVectorTools();
        private bool Added = false;

        public InfluenceVectorPage()
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

        protected void AddInfluenceVector(object sender, EventArgs args)
        {
            Added = true;
            NextButton.IsEnabled = true;
            SPB.InfluenceVector =
                BPTools.GetInfluenceVector(SPB.InitialRunVector,
                                           SPB.TrialRunVector);
            SPB.WeightPlacementVector = BPTools.CalcBalanceWeight(SPB.InitialRunVector, SPB.TrialWeightVector, SPB.InfluenceVector);
            VGraph.PushDraw();
        }

        async protected void Back(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        async protected void Next(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new BalanceWeightPlacementPage()));
        }

        void IVectorGraphPainter.OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {           
            if (Added) BPTools.DrawInfluenceVector(args.Info, args.Surface.Canvas, ((App)Application.Current).spbData);
            else BPTools.DrawTrialWeight(args.Info, args.Surface.Canvas, ((App)Application.Current).spbData);
        }
    }
}