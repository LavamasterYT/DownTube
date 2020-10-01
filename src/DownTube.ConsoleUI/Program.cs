using System;
using System.Threading.Tasks;
using DownTube.Core;

namespace DownTube.ConsoleUI
{
    class Program
    {
        static async Task Main()
        {
            Video vid = new Video("https://www.youtube.com/watch?v=dE-tPvbmItg");
            Console.WriteLine(vid.Title);
            Console.WriteLine(vid.Channel);
            foreach (var i in vid.VideoDatas)
            {
                Console.WriteLine(i.Quality);
            }
            foreach (var i in vid.AudioDatas)
            {
                Console.WriteLine(i.Quality);
            }

            await vid.Download("1080p", true, "$c - $t", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
