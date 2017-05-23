using System.Collections.Generic;

namespace EasyWatermark
{
    public class ImagesEditorWorker
    {
        private static Queue<string> ImagesPaths = new Queue<string>();
        public static long ThreadsCount = 2;
        public static void SetPathsQueue(ICollection<string> imagesPaths)
        {
            if (imagesPaths == null) return;
            lock (ImagesPaths)
            {
                ImagesPaths.Clear();
                foreach (var a in imagesPaths) ImagesPaths.Enqueue(a);
            }
        }

        public void Work()
        {
            var imagePath = "";
            while (true)
            {
                lock (ImagesPaths)
                {
                    if (ImagesPaths.Count == 0) return;
                    else imagePath = ImagesPaths.Dequeue();
                }
                ImagesEditor.EditImageFromFile(imagePath);
            }
        }
    }
}
