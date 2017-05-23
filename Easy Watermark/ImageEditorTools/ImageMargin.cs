using System;

namespace EasyWatermark.ImagesEditorTools
{
    public interface IImageMargin
    {
        float Top { get; set; }
        float Right { get; set; }
        float Bottom { get; set; }
        float Left { get; set; }

        AbsoluteImageMargin GetComputedMargin(ImageResolution imageResolution);
    }

    public class AbsoluteImageMargin : IImageMargin
    {
        public float Top { get; set; }
        public float Right { get; set; }
        public float Bottom { get; set; }
        public float Left { get; set; }

        public AbsoluteImageMargin GetComputedMargin(ImageResolution imageResolution)
        {
            return this;
        }

        public AbsoluteImageMargin()
        {
            Top = 0;
            Right = 0;
            Bottom = 0;
            Left = 0;
        }
        public AbsoluteImageMargin(float top, float right, float bottom, float left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

    }

    public class RelativeImageMargin : IImageMargin
    {
        public float Top { get; set; }
        public float Right { get; set; }
        public float Bottom { get; set; }
        public float Left { get; set; }

        public AbsoluteImageMargin GetComputedMargin(ImageResolution imageResolution)
        {
            if (imageResolution == null) throw new ArgumentNullException();
            return new AbsoluteImageMargin()
            {
                Top = imageResolution.Height * Top,
                Right = imageResolution.Width * Right,
                Bottom = imageResolution.Height * Bottom,
                Left = imageResolution.Width * Left
            };
        }

        public RelativeImageMargin()
        {
            Top = 0;
            Right = 0;
            Bottom = 0;
            Left = 0;
        }
        public RelativeImageMargin(float top, float right, float bottom, float left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
    }
}
