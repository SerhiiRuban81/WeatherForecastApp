using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp89
{
        public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Current
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double uvi { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public IList<Weather> weather { get; set; }
    }

    public class Minutely
    {
        public int dt { get; set; }
        public int precipitation { get; set; }
    }

    //public class Weather
    //{
    //    public int id { get; set; }
    //    public string main { get; set; }
    //    public string description { get; set; }
    //    public string icon { get; set; }
    //}

    public class Rain
    {
        [JsonProperty("1h")]
        public double  H1 { get; set; }
    }

    public class Hourly
    {
        public int dt { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public int clouds { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public IList<Weather> weather { get; set; }
        public Rain rain { get; set; }

        public override string ToString()
        {
            //int timestamp = 1590343200;
            DateTime dateTime = new DateTime(1970, 1, 1).AddSeconds(dt);
            //MessageBox.Show(dt.ToString());
            return $"{dateTime} : {temp} C, {humidity} %, Скорость ветра {wind_speed} м/с";
        }
    }

    public class Temp
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    public class FeelsLike
    {
        public double day { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    //public class Weather
    //{
    //    public int id { get; set; }
    //    public string main { get; set; }
    //    public string description { get; set; }
    //    public string icon { get; set; }
    //}

    public class Daily
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public Temp temp { get; set; }
        public FeelsLike feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public IList<Weather> weather { get; set; }
        public int clouds { get; set; }
        public double uvi { get; set; }
        public double? rain { get; set; }
    }

    public class Example
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string timezone { get; set; }
        public int timezone_offset { get; set; }
        public Current current { get; set; }
        public IList<Minutely> minutely { get; set; }
        public IList<Hourly> hourly { get; set; }
        public IList<Daily> daily { get; set; }
    }
}
