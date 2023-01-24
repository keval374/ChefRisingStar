using ChefRisingStar.Helpers;
using ChefRisingStar.Models;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

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

            testc();
        }

        private async void testc()
        {
            //RestHelper helper = new RestHelper();
            //UserCred userCred = new UserCred("jonathan.brunette@gmail.com", "password1");
            //string token = await helper.Post<UserCred, string>(userCred, "api/auth/authenticate");
            //helper.SetBearer(token);

            //List<DataPoint> dataPoints;
            //List<AxisInfo> AxisInfos;

            //dataPoints = new List<DataPoint>();
            //dataPoints.Add(new DataPoint("Audi e-tron", Color.Red, 69850, 80900, 8.4f, 218, 100f / 44));
            //dataPoints.Add(new DataPoint("Jaguar I-PACE", Color.Green, 39090, 44590, 8.2f, 234, 100f / 30));
            //dataPoints.Add(new DataPoint("Polestar 2", Color.Blue, 59900, 59900, 8.2f, 275, 100f / 27));

            //AxisInfos = new List<AxisInfo>();
            //AxisInfos.Add(new AxisInfo("PriceLow", "c", 90000, 30000));
            //AxisInfos.Add(new AxisInfo("PriceHigh", "c", 90000, 30000));
            //AxisInfos.Add(new AxisInfo("Rating", "0.0", 0, 10));
            //AxisInfos.Add(new AxisInfo("Range", "0", 0, 300));
            //AxisInfos.Add(new AxisInfo("Miles/kWh", "0.00", 0, 5));

            //string tmp = await RestHelper.Get<string>("graph", string.Empty);

            //DisplayAlert("AfterTest", $"Receieved {tmp}", "Cancel");
        }
    }
}