using System.Drawing;
using ZXing.Common;

namespace ZXing.Rendering
{
    internal class BitmapRenderer : IBarcodeRenderer<Bitmap>
    {
        public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content)
        {
            throw new System.NotImplementedException();
        }

        public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}