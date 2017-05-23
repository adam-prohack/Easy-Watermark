using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EasyWatermark
{
    public class ImagesStorage
    {
        private string imagesDirectory;
        private string resultImagesDirectory;
        private string watermarkPath;

        public string ImagesDirectory
        {
            get
            {
                if (Directory.Exists(imagesDirectory)) return imagesDirectory;
                return "Empty";
            }
            set
            {
                if (value != null && Directory.Exists(value))
                {
                    imagesDirectory = value;
                    if (value[value.Length - 1] != '/') imagesDirectory += '/';
                }
            }
        }
        public string ResultImagesDirectory
        {
            get
            {
                if (Directory.Exists(resultImagesDirectory)) return resultImagesDirectory;
                return "Empty";
            }
            set
            {
                if (value != null)
                {
                    if (!Directory.Exists(value)) Directory.CreateDirectory(value);
                    resultImagesDirectory = value;
                    if (value[value.Length - 1] != '/') resultImagesDirectory += '/';
                }
            }
        }
        public string WatermarkPath
        {
            get
            {
                if (File.Exists(watermarkPath)) return watermarkPath;
                return "Empty";
            }
            set
            {
                if (File.Exists(value))
                {
                    ImagesEditor.Watermark.Image = Image.FromFile(value);
                    watermarkPath = value;
                }
            }
        }
        public Collection<string> ImagesPaths
        {
            get { return new Collection<string>(Directory.GetFiles(imagesDirectory)); }
        }

        public void SaveImage(Image image, ImageFormat imageFormat, string fileName)
        {
            if (image != null) image.Save(resultImagesDirectory + fileName, imageFormat);
        }
    }
}
