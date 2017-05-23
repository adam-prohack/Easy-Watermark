using System.Drawing;

namespace EasyWatermark.ImagesEditorTools
{
    public class ImagePosition
    {
        public PointF FirstPoint { get; set; }
        public PointF SecondPoint { get; set; }
        public PointF ThirdPoint { get; set; }
        public PointF[] GetArray()
        {
            return new PointF[]
            {
                FirstPoint,
                SecondPoint,
                ThirdPoint
            };
        }
    }
}
