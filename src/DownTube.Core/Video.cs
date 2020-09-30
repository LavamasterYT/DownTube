using System.Collections.Generic;

namespace DownTube.Core
{
    public class Video
    {
        public string Title { get; set; }

        public string Channel { get; set; }

        public IList<string> VideoQualties { get; set; }

        public string DownloadURL { get; set; }

        public string ThumbnailURL { get; set; }
    }
}