using System.Drawing.Imaging;

namespace EasyWatermark.ImagesEditorTools
{
    public class ImageFormatConversion
    {
        public ImageFormat DefaultImageFormat { get; set; }
        public bool ForceConvertImage { get; set; }

        public ImageFormatConversion()
        {
            DefaultImageFormat = ImageFormat.Jpeg;
            ForceConvertImage = false;
        }
    }
}
