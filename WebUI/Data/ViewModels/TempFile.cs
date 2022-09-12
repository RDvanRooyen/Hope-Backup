using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Data.ViewModels
{
    public class TempFile
    {
        public byte[] Content { get; set; }
        public string Filename { get; set; }
        public int? ContentFileId { get; set; }
        public byte[] Thumbnail { get; set; }
        public string ThumbnailBase64
        {
            get
            {
                if (Thumbnail == null || !Thumbnail.Any()) return string.Empty;

                return "data:image/png;base64," + Convert.ToBase64String(Thumbnail);
            }
        }

        public string ContentType { get; internal set; }

        public bool HasDescription { get; set; }
        public string Description { get; set; }
    }
}
