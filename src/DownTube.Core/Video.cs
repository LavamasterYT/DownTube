using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace DownTube.Core
{
    public class Video
    {
        public string Title { get; set; }

        public string Channel { get; set; }

        public IList<VideoData> VideoDatas { get; set; }
        
        public IList<VideoData> AudioDatas { get; set; }

        public string ThumbnailURL { get; set; }

        public string VideoID { get; set; }

        public WebClient WebClient;

        public Video(string url)
        {
            WebClient = new WebClient();

            VideoID = HttpUtility.ParseQueryString(new Uri(url).Query).Get("v");
            ThumbnailURL = $"http://i3.ytimg.com/vi/{VideoID}/hqdefault.jpg";
            AudioDatas = new List<VideoData>();
            VideoDatas = new List<VideoData>();

            string json = HTTP.GET($"https://upull.me/api/yt/{VideoID}?sig=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0cyI6MTYwMTQyODQwMH0.xCBFfDBjaF3LS-GO6mbWuDGnt1kSA9th9su2hySNdTM");

            VideoApi result = VideoApi.FromJson(json);

            Title = result.Details.Title;
            Channel = result.Details.Author;

            foreach (var i in result.VideoLayers)
            {
                if (i.Meta.Type != TypeEnum.Mp4)
                    continue;

                VideoDatas.Add(new VideoData
                {
                    DownloadURL = i.Url.AbsoluteUri,
                    Quality = i.Meta.Quality
                });
            }

            foreach (var i in result.AudioLayers)
            {
                if (i.Meta.Type != TypeEnum.M4A)
                    continue;

                AudioDatas.Add(new VideoData
                {
                    DownloadURL = i.Url.AbsoluteUri,
                    Quality = i.Meta.Bitrate
                });
            }
        }

        public void Dispose()
        {
            WebClient.Dispose();
            AudioDatas.Clear();
            VideoDatas.Clear();
        }

        public async Task Download(string quality, bool downloadVideo, string format, string directory)
        {
            string fileName = format.Replace("$c", Channel).Replace("$t", Title);

            fileName = $"{directory}\\{fileName.RemoveInvalidChars()}";

            string videoURL = "";
            string audioURL = "";

            foreach (var i in VideoDatas)
            {
                if (i.Quality == quality)
                {
                    videoURL = i.DownloadURL;
                    break;
                }
            }
            foreach (var i in AudioDatas)
            {
                audioURL = i.DownloadURL;
                break;
            }

            if (!downloadVideo)
            {
                await WebClient.DownloadFileTaskAsync(audioURL, fileName + ".m4a");
            }
            else
            {
                await WebClient.DownloadFileTaskAsync(audioURL, fileName + "-temp.m4a");
                await WebClient.DownloadFileTaskAsync(videoURL, fileName + "-temp.mp4");

                System.Diagnostics.Process p = System.Diagnostics.Process.Start(@"D:\(3) Applications\Program Files\Console Commands\\ffmpeg.exe", $"-i \"{fileName}-temp.mp4\" -i \"{fileName}-temp.m4a\" -c:v copy -c:a copy \"{fileName}\".mp4");
                p.WaitForExit();

                File.Delete(fileName + "-temp.m4a");
                File.Delete(fileName + "-temp.mp4");
            }
        }

        ~Video()
        {
            Dispose();
        }
    }
}