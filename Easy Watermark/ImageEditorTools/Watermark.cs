using System;
using System.Drawing;

namespace EasyWatermark.ImagesEditorTools
{
    public class Watermark
    {
        public Image Image { get; set; }
        public ImageScale Scale { get; set; }
        public IImageMargin Margin { get; set; }
        public ImageAlignment Alignment { get; set; }
        public float Opacity { get; set; }

        public ImagePosition GetWatermarkPosition(ImageResolution imageResolution, ImageResolution watermarkResolution, AbsoluteImageMargin margin)
        {
            if (margin != null && imageResolution != null && watermarkResolution != null)
            {
                var resultPosition = new ImagePosition();
                if (Alignment == ImageAlignment.TopLeft)
                {
                    resultPosition.FirstPoint = new PointF(margin.Left, margin.Top);
                    resultPosition.SecondPoint = new PointF(watermarkResolution.Width + margin.Left, margin.Top);
                    resultPosition.ThirdPoint = new PointF(margin.Left, watermarkResolution.Height + margin.Top);
                }
                else if (Alignment == ImageAlignment.TopRight)
                {
                    resultPosition.FirstPoint = new PointF(imageResolution.Width - margin.Right - watermarkResolution.Width, margin.Top);
                    resultPosition.SecondPoint = new PointF(imageResolution.Width - margin.Right, margin.Top);
                    resultPosition.ThirdPoint = new PointF(imageResolution.Width - margin.Right - watermarkResolution.Width, watermarkResolution.Height + margin.Top);
                }
                else if (Alignment == ImageAlignment.BottomLeft)
                {
                    resultPosition.FirstPoint = new PointF(margin.Left, imageResolution.Height - watermarkResolution.Height - margin.Bottom);
                    resultPosition.SecondPoint = new PointF(watermarkResolution.Width + margin.Left, imageResolution.Height - watermarkResolution.Height - margin.Bottom);
                    resultPosition.ThirdPoint = new PointF(margin.Left, imageResolution.Height - margin.Bottom);
                }
                else if (Alignment == ImageAlignment.BottomRight)
                {
                    resultPosition.FirstPoint = new PointF(imageResolution.Width - watermarkResolution.Width - margin.Right, imageResolution.Height - watermarkResolution.Height - margin.Bottom);
                    resultPosition.SecondPoint = new PointF(imageResolution.Width - margin.Right, imageResolution.Height - watermarkResolution.Height - margin.Bottom);
                    resultPosition.ThirdPoint = new PointF(imageResolution.Width - watermarkResolution.Width - margin.Right, imageResolution.Height - margin.Bottom);
                }
                else if (Alignment == ImageAlignment.Top)
                {
                    resultPosition.FirstPoint = new PointF((imageResolution.Width / 2) - (watermarkResolution.Width / 2), margin.Top);
                    resultPosition.SecondPoint = new PointF((imageResolution.Width / 2) + (watermarkResolution.Width / 2), margin.Top);
                    resultPosition.ThirdPoint = new PointF((imageResolution.Width / 2) - (watermarkResolution.Width / 2), margin.Top + watermarkResolution.Height);
                }
                else if (Alignment == ImageAlignment.Bottom)
                {
                    resultPosition.FirstPoint = new PointF((imageResolution.Width / 2) - (watermarkResolution.Width / 2), imageResolution.Height - margin.Bottom - watermarkResolution.Height);
                    resultPosition.SecondPoint = new PointF((imageResolution.Width / 2) + (watermarkResolution.Width / 2), imageResolution.Height - margin.Bottom - watermarkResolution.Height);
                    resultPosition.ThirdPoint = new PointF((imageResolution.Width / 2) - (watermarkResolution.Width / 2), imageResolution.Height - margin.Bottom);
                }
                else if (Alignment == ImageAlignment.Left)
                {
                    resultPosition.FirstPoint = new PointF(margin.Left, (imageResolution.Height / 2) - (watermarkResolution.Height / 2));
                    resultPosition.SecondPoint = new PointF(margin.Left + watermarkResolution.Width, (imageResolution.Height / 2) - (watermarkResolution.Height / 2));
                    resultPosition.ThirdPoint = new PointF(margin.Left, (imageResolution.Height / 2) + (watermarkResolution.Height / 2));
                }
                else if (Alignment == ImageAlignment.Right)
                {
                    resultPosition.FirstPoint = new PointF(imageResolution.Width - margin.Right - watermarkResolution.Width, (imageResolution.Height / 2) - (watermarkResolution.Height / 2));
                    resultPosition.SecondPoint = new PointF(imageResolution.Width - margin.Right, (imageResolution.Height / 2) - (watermarkResolution.Height / 2));
                    resultPosition.ThirdPoint = new PointF(imageResolution.Width - margin.Right - watermarkResolution.Width, (imageResolution.Height / 2) + (watermarkResolution.Height / 2));
                }
                else if (Alignment == ImageAlignment.Center)
                {
                    resultPosition.FirstPoint = new PointF((imageResolution.Width / 2) - (watermarkResolution.Width / 2), (imageResolution.Height / 2) - (watermarkResolution.Height / 2));
                    resultPosition.SecondPoint = new PointF((imageResolution.Width / 2) + (watermarkResolution.Width / 2), (imageResolution.Height / 2) - (watermarkResolution.Height / 2));
                    resultPosition.ThirdPoint = new PointF((imageResolution.Width / 2) - (watermarkResolution.Width / 2), (imageResolution.Height / 2) + (watermarkResolution.Height / 2));
                }
                else throw new NotImplementedException();
                return resultPosition;
            }
            throw new ArgumentException();
        }

        public Watermark()
        {
            Image = null;
            Scale = new ImageScale(0.2F, ImageScaleDimension.Width);
            Margin = new AbsoluteImageMargin();
            Alignment = ImageAlignment.BottomRight;
            Opacity = 1f;
        }
    }
}
