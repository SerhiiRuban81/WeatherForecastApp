using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using color=System.Windows.Media;

namespace WindowsFormsApp89
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        delegate void ListDelegate(IList<Hourly> weather);
        delegate void ChartDelegate(IList<Hourly> weather);
        delegate void ChartDelegate<T>(IEnumerable<T> weather, IEnumerable<string> dates);
        private async void button1_Click(object sender, EventArgs e)
        {
            //pictureBox1.Load("https://lh3.googleusercontent.com/proxy/Pm1kgwEn_Ni4PqTuK2ZorPlDF1zxT9G92rJJCHGcd24ZuKSIhAzLeSyK_UiPq6H2eJ_JC-qLk-HXQ95e5FXZOEU6yAe-F4omE2FdZETFFLRNaIgAkuWTgRIoRbAY_wJlVnxTS_k0IQXLtuI");
            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage reqw = new HttpRequestMessage())
            {
                //reqw.RequestUri = new Uri("https://api.openweathermap.org/data/2.5/onecall?lat=30.489772&lon=-99.771335&units=metric&lang=ua&appid=");
                //reqw.RequestUri = new Uri("https://api.openweathermap.org/data/2.5/onecall?lat=47.908091&lon=33.387017&units=metric&lang=ua&appid=");
                reqw.RequestUri = new Uri($"https://api.openweathermap.org/data/2.5/onecall?lat={textBox1.Text}&lon={textBox2.Text}&units=metric&lang=ua&appid=");
                reqw.Method = HttpMethod.Get;
                HttpResponseMessage httpResponse = await client.SendAsync(reqw).ConfigureAwait(false);
                string resp = await httpResponse.Content.ReadAsStringAsync();
                Example example = JsonConvert.DeserializeObject<Example>(resp);
                //MessageBox.Show($"{example.current.temp} C, {example.current.humidity}%");
                listBox1.BeginInvoke(new ListDelegate(UpdateList), example.hourly);
                cartesianChart1.BeginInvoke(new ChartDelegate(UpdateChart), example.hourly);
                IEnumerable<double> temps = example.hourly.Select(t => t.temp);
                IEnumerable<int> humidity = example.hourly.Select(t => t.humidity);
                IEnumerable<string> dates = example.hourly.Select(t => new DateTime(1970, 1, 1).AddSeconds(t.dt).ToString("dd.M.y HH:mm"));
                cartesianChart2.BeginInvoke(new ChartDelegate<int>(UpdateChart2<int>), humidity, dates);
                cartesianChart1.BeginInvoke(new ChartDelegate(UpdateChart), example.hourly);
               


               

               

            }
        }

        private void UpdateList(IList<Hourly> weather)
        {
            listBox1.DataSource = weather;
        }

        private void UpdateChart(IList<Hourly> weather)
        {
            IEnumerable<double> temps = weather.Select(t => t.temp);
           
            IEnumerable<string> dates = weather.Select(t => new DateTime(1970, 1, 1).AddSeconds(t.dt).ToString());
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Температура",
                    //Values = new ChartValues<double> {4, 6, 5, 2, 7}
                    Values = new ChartValues<double> (temps),
                    PointForeground = color.Brushes.DarkKhaki,
                    
                },
                //new LineSeries
                //{
                //    Title = "Series 2",
                //    Values = new ChartValues<double> {6, 7, 3, 4, 6},
                //    PointGeometry = null
                //},
                //new LineSeries
                //{
                //    Title = "Series 2",
                //    Values = new ChartValues<double> {5, 2, 8, 3},
                //    PointGeometry = DefaultGeometries.Square,
                //    PointGeometrySize = 15
                //}
            };
            //cartesianChart1.DefaultLegend.
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Время",
                //Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" }
                Labels = dates.ToList()
            });
            cartesianChart1.LegendLocation = LegendLocation.Top;
            cartesianChart1.AxisY.Clear();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Температура",
                LabelFormatter = value => $" {value} °C"
            });
        }

        private void UpdateChart2<T>(IEnumerable<T> data, IEnumerable<string> dates)
        {
            //IEnumerable<T> temps = weather.Select(t => t.temp);

            //IEnumerable<string> dates = weather.Select(t => new DateTime(1970, 1, 1).AddSeconds(t.dt).ToString());
            cartesianChart2.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Влажность",
                    Values = new ChartValues<T> (data),
                    PointForeground = color.Brushes.DarkBlue,

                },
            };
            cartesianChart2.AxisX.Clear();
            cartesianChart2.AxisX.Add(new Axis
            {
                Title = "Время",
                Labels = dates.ToList()
            });
            cartesianChart2.LegendLocation = LegendLocation.Bottom;
            cartesianChart2.AxisY.Clear();
            //ToString("N1") + "°C"
            cartesianChart2.AxisY.Add(new Axis
            {
                Title = "Влажность",
                LabelFormatter = value => $" {value} %"
            }) ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "47.908091";
            textBox2.Text = "33.387017";
        }
    }
}
