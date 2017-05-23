namespace EasyWatermark.ImagesEditorTools
{
    public class ImageResolution
    {
        public float Width { get; set; }
        public float Height { get; set; }
        public ImageResolution()
        {
            Height = 0;
            Width = 0;
        }
        public ImageResolution(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }
}
