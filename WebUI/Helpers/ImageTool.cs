using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace WebUI.Helpers
{
    public class ImageTool
    {
        public static byte[] GetByteFromPath(string path)
        {
            byte[] imageBytes = Array.Empty<byte>();
            try
            {
                var image = System.Drawing.Image.FromFile(path);
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    imageBytes = m.ToArray();
                }
            }
            catch
            {

            }
            
            return imageBytes;
        }
        public static string ImageToBase64(Image img)
        {
            using (Image image = img)
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
        public static string ImagePathToBase64(string path)
        {
            var image = System.Drawing.Image.FromFile(path);
            var url = "data:image/png;base64," + WebUI.Helpers.ImageTool.ImageToBase64(image);
            return url;
        }

        public static byte[] CreateThumbnail(byte[] arr)
        {
            try
            {
                Image imgToResize = null;
                using (MemoryStream stream = new MemoryStream(arr))
                {
                    imgToResize = Image.FromStream(stream);
                }

                float width = 100;
                float height = 100;
                float imageWidth = imgToResize.Width;
                float imageHeight = imgToResize.Height;

                if (imageWidth > imageHeight)
                {
                    //resize by width
                    height = (width / imageWidth) * imageHeight;
                }
                else
                {
                    width = (height / imageHeight) * imageWidth;
                }


                var resized = new Bitmap(imgToResize, new Size((int)width, (int)height));
                using var imageStream = new MemoryStream();
                resized.Save(imageStream, ImageFormat.Png);
                var imageBytes = imageStream.ToArray();
                return imageBytes;
            }
            catch (Exception)
            {

                return null;
            }
            


            //Size size = new Size(100, 100);
            ////Get the image current width  
            //int sourceWidth = imgToResize.Width;
            ////Get the image current height  
            //int sourceHeight = imgToResize.Height;
            //float nPercent = 0;
            //float nPercentW = 0;
            //float nPercentH = 0;
            ////Calulate  width with new desired size  
            //nPercentW = ((float)size.Width / (float)sourceWidth);
            ////Calculate height with new desired size  
            //nPercentH = ((float)size.Height / (float)sourceHeight);
            //if (nPercentH < nPercentW)
            //    nPercent = nPercentH;
            //else
            //    nPercent = nPercentW;
            ////New Width  
            //int destWidth = (int)(sourceWidth * nPercent);
            ////New Height  
            //int destHeight = (int)(sourceHeight * nPercent);
            //Bitmap b = new Bitmap(destWidth, destHeight);
            //Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //// Draw image with new width and height  
            //g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            //g.Dispose();

            //byte[] result;
            //using (MemoryStream m = new MemoryStream())
            //{
            //    b.Save(m, b.RawFormat);
            //    result = m.ToArray();
            //}
            //return result;
        }
    }
}
