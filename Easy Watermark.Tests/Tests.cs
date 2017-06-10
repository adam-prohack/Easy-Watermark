using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyWatermark;
using System;

namespace EasyWatermark.Tests
{
    [TestClass]
    public class ImageEditorTests
    {
        string startDirectory = @"../../Resources/";
        float marginScale = 0.05f;
        float opacity = 0.5f;

        [TestMethod]
        public void CreateImagesSingleThread()
        {
            ImagesEditorWorker.ThreadsCount = 1;
            ImagesEditor.Storage.ImagesDirectory = startDirectory + @"Images";
            ImagesEditor.Storage.WatermarkPath = startDirectory + @"Watermark.png";
            ImagesEditor.Watermark.Margin = new ImagesEditorTools.RelativeImageMargin(marginScale, marginScale, marginScale, marginScale);
            ImagesEditor.Watermark.Opacity = opacity;
            foreach (var a in Enum.GetValues(typeof(ImagesEditorTools.ImageAlignment)))
            {
                ImagesEditor.Watermark.Alignment = (ImagesEditorTools.ImageAlignment)a;
                ImagesEditor.Storage.ResultImagesDirectory = startDirectory + @"Images/Results/SingleThread/" + Enum.GetName(typeof(ImagesEditorTools.ImageAlignment), a) + @"/";
                ImagesEditor.EditImages();
            }
        }

        [TestMethod]
        public void CreateImagesMultithreads()
        {
            ImagesEditorWorker.ThreadsCount = 5;
            ImagesEditor.Storage.ImagesDirectory = startDirectory + @"Images";
            ImagesEditor.Storage.WatermarkPath = startDirectory + @"Watermark.png";
            ImagesEditor.Watermark.Margin = new ImagesEditorTools.RelativeImageMargin(marginScale, marginScale, marginScale, marginScale);
            ImagesEditor.Watermark.Opacity = opacity;
            foreach (var a in Enum.GetValues(typeof(ImagesEditorTools.ImageAlignment)))
            {
                ImagesEditor.Watermark.Alignment = (ImagesEditorTools.ImageAlignment)a;
                ImagesEditor.Storage.ResultImagesDirectory = startDirectory + @"Images/Results/MultiThreads/" + Enum.GetName(typeof(ImagesEditorTools.ImageAlignment), a) + @"/";
                ImagesEditor.EditImages();
            }
        }
    }
}