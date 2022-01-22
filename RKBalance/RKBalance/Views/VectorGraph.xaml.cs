using RKBalance.Services;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace RKBalance.Views
{
    public partial class VectorGraph : ContentView
    {
        public SinglePlaneBalanceVectorTools spbData { get; set; }
        internal IVectorGraphPainter Painter { get; set; }

        public VectorGraph()
        {
            InitializeComponent();
        }

        public void SetDIPainter(IVectorGraphPainter _painter) => Painter = _painter;

        public void PushDraw()
        {
            canvasView.InvalidateSurface();
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            Painter?.OnCanvasViewPaintSurface(sender, args);
        }

        public void Clear()
        {
            canvasView.InvalidateSurface();
        }
    }
}