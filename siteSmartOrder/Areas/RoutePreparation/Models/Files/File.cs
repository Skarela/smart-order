using System.Text.RegularExpressions;
using siteSmartOrder.Infrastructure.Extensions;
using siteSmartOrder.Infrastructure.Settings;

namespace siteSmartOrder.Areas.RoutePreparation.Models.Files
{
    public class File
    {
        private string _href = AppSettings.FilesFolder + "Unknown.png";
        private string _source = AppSettings.FilesFolder + "Unknown.png";

        public string Type
        {
            get
            {
                if (IsImage) return "Imagen";
                if (IsPdf) return "PDF";
                if (IsTxt) return "TXT";
                if (IsVideo) return "Video";
                return "Desconocido";
            }
        }

        public string Class
        {
            get
            {
                if (IsPdf) return "pdf";
                if (IsTxt) return "text";
                if (IsVideo) return "video";
                return "image";
            }
        }

        public string Source
        {
            get { return _source; }
        }

        public string FilePath
        {
            get { return _href; }
        }

        public bool IsImage
        {
            get { return Regex.IsMatch(_href, @"\.(?:jpg|jpeg|gif|bmp|png)$", RegexOptions.IgnoreCase); }
        }

        public bool IsPdf
        {
            get { return Regex.IsMatch(_href, @"(.pdf)$", RegexOptions.IgnoreCase); }
        }

        public bool IsTxt
        {
            get { return Regex.IsMatch(_href, @"(.txt)$", RegexOptions.IgnoreCase); }
        }

        public bool IsVideo
        {
            get { return Regex.IsMatch(_href, @"\.(?:mp4|wmv|3gp|avi)$", RegexOptions.IgnoreCase); }
        }

        public bool IsAudio
        {
            get { return Regex.IsMatch(_href, @"\.(?:mp3|aac|wav|wma|midi|mp4|m4a|m4p|m4v|3gp)$", RegexOptions.IgnoreCase); }
        }

        public void ResolvePath(string path)
        {
            if (IsPdf) _source = AppSettings.FilesFolder + "PDF.png";
            if (IsTxt) _source = AppSettings.FilesFolder + "TXT.png";
            if (IsVideo) _source = AppSettings.FilesFolder + "Video.png";

            if (path.IsNotNullOrEmpty())
            {
                if (IsImage) _source = path;
                _href = path;
            }
        }
    }
}