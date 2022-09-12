using System;
using System.IO;
using System.Linq;

namespace WebUI.Data.Models
{
    public partial class ContentFiles
    {

        public const string PhotoContentFileCategory ="Photos";
        public const string MaterialContentFileCategory ="Material";
        public const string ResourceContentFileCategory ="Resources";
        public const string CommunityContentFileCategory ="Connection";
        public const string FormativeContentFileCategory ="FormativeAssessments";
        public const string SummativeContentFileCategory ="SummativeAssessments";
        public const string PacingContentFileCategory ="PacingGuide";
        public const string StudentWorkContentFileCategory ="StudentWorks";
        public int Id { get; set; }
        public string RelativePath { get; set; }

        public string FilenameFromPath
        {
            get
            {
                if (string.IsNullOrEmpty(RelativePath)) return string.Empty;

                var s = RelativePath.Split("\\", StringSplitOptions.RemoveEmptyEntries);
                return s.LastOrDefault();
            }
        }

        public int ContentId { get; set; }

        public ContentDetails Content { get; set; }

        public string Category { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string Filename { get; internal set; }
        public bool IsPrimaryPhoto { get; internal set; }

        public FileStream? GetFileStream(string baseDirectory, string RelativePath)
        {                     
            if (!File.Exists(Path.Combine(baseDirectory, RelativePath)))
            {
                return null;
            }
            return File.OpenRead(Path.Combine(baseDirectory, RelativePath));            
        }

        public ContentFiles(string baseDirectory, int contentId, string subDirectory, string fileName, byte[] bytes, string category="")
        {
            ContentId = contentId;
            RelativePath = Path.Combine(contentId.ToString(), subDirectory, fileName);
            Category = category;
            var path = Path.Combine(baseDirectory, RelativePath);
            
            if(!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }
            if (!Directory.Exists(Path.Combine(baseDirectory, contentId.ToString())))
            {
                Directory.CreateDirectory(Path.Combine(baseDirectory, contentId.ToString()));
            }
            if (!Directory.Exists(Path.Combine(baseDirectory, contentId.ToString(), subDirectory)))
            {
                Directory.CreateDirectory(Path.Combine(baseDirectory, contentId.ToString(), subDirectory));
            }

            File.WriteAllBytes(path, bytes);
        }
        private ContentFiles()
        {

        }
    }    
}