using System;

namespace EasyWatermark.ImagesEditorTools
{
    public enum ImageScaleDimension
    {
        Width, Height, Combined
    }

    public class ImageScale
    {
        public float Value { get; set; }
        public ImageScaleDimension PropertyDimension { get; set; }

        public ImageResolution GetComputedResolution(ImageResolution mainImageResolution, ImageResolution scaledImageResolution)
        {
            if (mainImageResolution == null || scaledImageResolution == null) throw new ArgumentNullException();
            var result = new ImageResolution();
            if (PropertyDimension == ImageScaleDimension.Combined)
            {
                result.Width = mainImageResolution.Width * Value;
                result.Height = mainImageResolution.Height * Value;
            }
            else if (PropertyDimension == ImageScaleDimension.Width)
            {
                result.Width = mainImageResolution.Width * Value;
                result.Height = scaledImageResolution.Height * mainImageResolution.Width * Value / scaledImageResolution.Width;
            }
            else if (PropertyDimension == ImageScaleDimension.Height)
            {
                result.Height = mainImageResolution.Height * Value;
                result.Width = scaledImageResolution.Width * mainImageResolution.Height * Value / scaledImageResolution.Height;
            }
            else throw new NotImplementedException();
            return result;
        }

        public ImageScale()
        {
            Value = 1;
            PropertyDimension = ImageScaleDimension.Width;
        }
        public ImageScale(float scale, ImageScaleDimension propertyDimension)
        {
            Value = scale;
            PropertyDimension = propertyDimension;
        }
    }
}
