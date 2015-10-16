using System;


namespace Utils
{

    public class HotelEntity : Java.IO.ISerializable
    {
        private double R = 6371229;
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public double Distance { get; set; }

        public IntPtr Handle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void getDistance(double longt1, double lat1)
        {
            double x, y, distance;
            x = (Longitude - longt1) * Math.PI * R
              * Math.Cos(((lat1 + Latitude) / 2) * Math.PI / 180) / 180;
            y = (Latitude - lat1) * Math.PI * R / 180;
            //System.out.println((Math.hypot(x, y) / 1000));
            //distance = Double.parseDouble(String.format("%.0f", Math.hypot(x, y) / 1000));
            Distance = (Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) / 1000);
        }

        public override string ToString()
        {
            return string.Format("{0} {3}km({1}, {2})", Name, Longitude.ToString("0.0000"), Latitude.ToString("0.0000"), Distance.ToString("0.00"));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
