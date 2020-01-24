using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DogBreeds
{
    public class HistoricalFileReader
    {
        public List<Plot> plots = new List<Plot>();
        DateTime first = new DateTime();

        public HistoricalFileReader()
        {
            int i = 0;
            double d = 0;
            DateTime date;
            string path = AppDomain.CurrentDomain.BaseDirectory + "Dogs.csv";

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    if(i == 0)
                    {
                        first = Convert.ToDateTime(parts[0]);
                    }

                    date = Convert.ToDateTime(parts[0]);
                    d = Convert.ToDouble(parts[2].TrimEnd('%'));
                    plots.Add(new Plot(d, date));

                    i++;
                }
                reader.Close();
            }
        }

        public DateTime getFirstDate()
        {
            return first;
        }
    }

    
}
