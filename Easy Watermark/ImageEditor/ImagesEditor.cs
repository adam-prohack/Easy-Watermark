using System.IO;
using System.Drawing;
using System;
using EasyWatermark.ImagesEditorTools;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Drawing.Drawing2D;

namespace EasyWatermark
{
    public static class ImagesEditor
    {
        public static ImagesEditorActionHandlers ActionHandlers { get; set; }
        public static ImagesStorage Storage { get; set; }

        public static Watermark Watermark { get; set; }
        public static ImageOptimization Optimization { get; set; }
        public static ImageFormatConversion ImageFormatConversion { get; set; }

        static ImageResolution GetResolution(this Image image)
        {
            if (image == null) throw new InvalidOperationException();
            var resultResolution = new ImageResolution();
            resultResolution.Height = image.Height;
            resultResolution.Width = image.Width;
            try
            {
                if (Optimization.IsEnable && (image.Width > Optimization.MaximumResolution.Width || image.Height > Optimization.MaximumResolution.Height))
                {
                    if (image.Height > image.Width)
                    {
                        resultResolution.Width = Optimization.MaximumResolution.Width;
                        resultResolution.Height = resultResolution.Width / image.Width * resultResolution.Height;
                    }
                    else
                    {
                        resultResolution.Height = Optimization.MaximumResolution.Height;
                        resultResolution.Width = resultResolution.Height / image.Height * resultResolution.Width;
                    }
                }
                return resultResolution;
            }
            catch (ArgumentException) { throw new ArgumentException(); }
        }
        static ImageFormat GetImageFormat(string fileName)
        {
            switch (fileName)
            {
                case ".jpeg": return ImageFormat.Jpeg;
                case ".jpg": return ImageFormat.Jpeg;
                case ".bmp": return ImageFormat.Bmp;
                case ".ico": return ImageFormat.Icon;
                case ".png": return ImageFormat.Png;
                case ".gif": return ImageFormat.Gif;
                default: return null;
            }
        }
        static string GetImageExtension(ImageFormat imageFormat)
        {
            if (imageFormat == ImageFormat.Jpeg) return ".jpg";
            else if (imageFormat == ImageFormat.Bmp) return ".bmp";
            else if (imageFormat == ImageFormat.Icon) return ".ico";
            else if (imageFormat == ImageFormat.Png) return ".png";
            else if (imageFormat == ImageFormat.Gif) return ".gif";
            else return ".jpg";
        }
        static Image AddWatermark(this Image image)
        {
            if (Watermark.Image == null || image == null) throw new ArgumentException();
            var imageResolution = image.GetResolution();
            var resultBitmap = new Bitmap((int)imageResolution.Width, (int)imageResolution.Height);
            try
            {
                resultBitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                Image watermarkImageCopy = null;
                lock (Watermark) { watermarkImageCopy = (Image)Watermark.Image.Clone(); }
                using (var graphicCanvas = Graphics.FromImage(resultBitmap))
                {
                    graphicCanvas.CompositingQuality = CompositingQuality.HighQuality;
                    graphicCanvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphicCanvas.SmoothingMode = SmoothingMode.HighQuality;

                    var watermarkOrginalSize = new RectangleF(0, 0, watermarkImageCopy.Width, watermarkImageCopy.Height);
                    var watermarkComputedResolution = Watermark.Scale.GetComputedResolution(imageResolution, watermarkImageCopy.GetResolution());
                    var waterwarkComputedMargin = Watermark.Margin.GetComputedMargin(imageResolution);
                    var watermarkComputedPosition = Watermark.GetWatermarkPosition(imageResolution, watermarkComputedResolution, waterwarkComputedMargin);
                    var watermarkColorMatrix = new ColorMatrix();
                    watermarkColorMatrix.Matrix33 = Watermark.Opacity;
                    var watermarkImageAttributes = new ImageAttributes();
                    watermarkImageAttributes.SetColorMatrix(watermarkColorMatrix);

                    graphicCanvas.DrawImage(image, 0, 0, imageResolution.Width, imageResolution.Height);
                    graphicCanvas.DrawImage(watermarkImageCopy, watermarkComputedPosition.GetArray(), watermarkOrginalSize, GraphicsUnit.Pixel, watermarkImageAttributes);
                }
                watermarkImageCopy.Dispose();
                return resultBitmap;
            }
            catch
            {
                resultBitmap.Dispose();
                throw new ArgumentException();
            }
        }

        public static void EditImageFromFile(string imagePath)
        {
            try
            {
                var imageFormat = GetImageFormat(Path.GetExtension(imagePath));
                if (imageFormat == null || ImageFormatConversion.ForceConvertImage == true) imageFormat = ImageFormatConversion.DefaultImageFormat;
                using (var imageSource = Image.FromFile(imagePath))
                {
                    using (var imageResult = AddWatermark(imageSource))
                    {
                        var fileName = Path.GetFileNameWithoutExtension(imagePath);
                        var fileExtension = GetImageExtension(imageFormat);
                        Storage.SaveImage(imageResult, imageFormat, fileName + fileExtension);
                    }
                }
            }
            catch (Exception e)
            {
                string message = "Error with saving file: " + imagePath + "\n";
                message += "Error message: " + e.Message;
                MessageBox.Show(message, "Saving Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ActionHandlers.EndSingleImageEdition?.Invoke();
        }
        public static void EditImages()
        {
            ImagesEditorWorker.SetPathsQueue(Storage.ImagesPaths);
            var threads = new List<Thread>();
            var worker = new ImagesEditorWorker();
            var action = new ThreadStart(worker.Work);
            for (var i = 0; i < ImagesEditorWorker.ThreadsCount; i++)
            {
                var thread = new Thread(action);
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
                threads.Add(thread);
            }
            foreach (var a in threads) a.Join();
            ActionHandlers.EndImagesEdition?.Invoke();
        }
        static public void SetThreadsCount(int number)
        {
            if (number < 0) return;
            ImagesEditorWorker.ThreadsCount = number;
        }

        static ImagesEditor()
        {
            Storage = new ImagesStorage();
            ActionHandlers = new ImagesEditorActionHandlers();
            Optimization = new ImageOptimization();
            Watermark = new Watermark();
            ImageFormatConversion = new ImageFormatConversion();
        }
    }
}
