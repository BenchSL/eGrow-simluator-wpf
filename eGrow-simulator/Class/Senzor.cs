using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGrow_simulator.Class
{
    public class Senzor
    {
        public int SensorDataId { get; set; }
        public DateTime Timestamp { get; set; }
        public double SoiltemperatureCelsius { get; set; }
        public double AmbientTemperatureCelsius { get; set; }
        public int UvIndex { get; set; }
        public int SolarRadiation { get; set; }
        public int LeafWetnes { get; set; }
        public int AmbientHumidityPercentage { get; set; }
        public int SoilHumidityPercentage { get; set; }
        public int GrowthCm { get; set; }
    }
}