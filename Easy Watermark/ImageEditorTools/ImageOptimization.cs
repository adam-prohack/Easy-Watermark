namespace EasyWatermark.ImagesEditorTools
{
    public class ImageOptimization
    {
        public ImageResolution MaximumResolution { get; set; }
        public bool IsEnable { get; set; }
        public ImageOptimization()
        {
            MaximumResolution = new ImageResolution(1920, 1080);
            IsEnable = false;
        }
        public ImageOptimization(ImageResolution maximumResolution)
        {
            MaximumResolution = maximumResolution;
            IsEnable = true;
        }
    }
}
