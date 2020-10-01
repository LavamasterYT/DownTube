using DownTube.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DownTube.Win32
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Video> vidList = new List<Video>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void removeVideo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void downloadVideo_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                Video vid = new Video(url.Text);
                vidList.Add(vid);
            });

            videos.ItemsSource = vidList;
        }
    }
}
