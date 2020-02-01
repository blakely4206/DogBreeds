using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DogBreeds
{
    public class HistoricalFileReader
    {
        public List<Plot> plots = new List<Plot>();
        public List<BarColumn> columns = new List<BarColumn>();
        DateTime first = new DateTime();

        public HistoricalFileReader()
        {
            Read();
        }

        public DateTime getFirstDate()
        {
            return first;
        }

        public void Read()
        {
            int i = 0;
            double d = 0;
            DateTime date;
            string path = AppDomain.CurrentDomain.BaseDirectory + "Dogs.csv";
            
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');

                            if (i == 0)
                            {
                                first = Convert.ToDateTime(parts[0]);
                            }

                            date = Convert.ToDateTime(parts[0]);
                            d = Convert.ToDouble(parts[2].TrimEnd('%'));
                            plots.Add(new Plot(d, date));

                            InsertIntoColumnsList(BreedList.Return_Breed(parts[1]).breed_name);

                            i++;
                        }
                        reader.Close();
                    }
                }
                else
                {
                    var x = File.Create(path);
                    x.Close();
                }
            
        }


        public void InsertIntoColumnsList(string breed)
        {
            if (columns.Exists(x => x.breedLabel == breed))
            {
                columns.Find(x => x.breedLabel == breed).breedFreq += 1;
            }
            else
            {
                columns.Add(new BarColumn(breed));
            }
        }
    }

    
}
