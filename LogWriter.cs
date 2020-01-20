using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DogBreeds
{
    class LogWriter
    {
        public static void Log(List<Dog> dogList)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Dogs.csv";
            StringBuilder sB = new StringBuilder();
            string theDate = Convert.ToString(DateTime.Now);

            StreamWriter reader = new StreamWriter(path, true);
            
            sB.Append(theDate + "," + dogList[0].GetBreedKey() + "," + dogList[0].Probability + "\n");
            
            reader.Write(sB);
            reader.Flush();
            reader.Close();
        }
    }    
}
