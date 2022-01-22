using RKBalance.Models;
using RKBalance.Services;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;

namespace RKBalance.Views
{
    public partial class BalanceWeightPlacementPage : ContentPage, IVectorGraphPainter
    {
        private SinglePlaneBalanceVectorTools BPTools = new SinglePlaneBalanceVectorTools();
        private SPBalanceRun SPB;

        public BalanceWeightPlacementPage()
        {
            BindingContext = this;
            InitializeComponent();
            //Assigning image from Resource
            RotorKit.Source = ImageSource.FromResource("RKBalance.RK4_1.png");
            SPB = ((App)Application.Current).spbData;
            BindingContext = SPB;

            VGraph.SetDIPainter(this);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VGraph.PushDraw();
        }

        async protected void Back(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        void IVectorGraphPainter.OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            BPTools.DrawBalanceWeight(args.Info, args.Surface.Canvas, ((App)Application.Current).spbData);
        }
    }
}