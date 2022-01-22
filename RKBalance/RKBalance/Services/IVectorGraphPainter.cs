using SkiaSharp.Views.Forms;

namespace RKBalance.Services
{
    public interface IVectorGraphPainter
    {
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args);
    }
}