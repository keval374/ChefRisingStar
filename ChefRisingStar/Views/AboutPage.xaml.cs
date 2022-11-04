using ChefRisingStar.Models;
using System.Diagnostics;
using System;
using Xamarin.Forms;
using System.Text.Json;

namespace ChefRisingStar.Views
{
    public partial class AboutPage : ContentPage
    {
        
        public AboutPage()
        {
            InitializeComponent();

            //string text = "GFG is a CS portal.";
            //byte[] data = Encoding.ASCII.GetBytes(text);

            //var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "count.txt");
            //using (var writer = File.CreateText(backingFile))
            //{
            //    writer.WriteLineAsync(text.ToString());
            //}
            //var GraphFileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Test.png");            

            //model.Image = chart.GetImage();
            //imgBox.Source = chart.GetImage();
            //imgBox.Source = new BitmapImage(new Uri(GraphFileName, UriKind.Absolute));
        }
    }
}