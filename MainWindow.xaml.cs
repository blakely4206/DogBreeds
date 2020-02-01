using System;
using System.Windows;
using System.Collections.ObjectModel;

namespace DogBreeds
{
    public partial class MainWindow : Window
    {
        ImageSetter img = new ImageSetter();

        HistoricalFileReader hfr = new HistoricalFileReader();
        public ObservableCollection<Dog> DogPredict { get; private set; }
        public ObservableCollection<BarColumn> CurrentBreeds { get; private set; }
        public ObservableCollection<Plot> DogPlots { get; private set; }

        public MainWindow()
        {
            
            DogPlots = new ObservableCollection<Plot>(hfr.plots);
            DogPredict = new ObservableCollection<Dog>();
            CurrentBreeds = new ObservableCollection<BarColumn>(hfr.columns);

            this.InitializeComponent();
            
            dateAxis.Maximum = OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now);
            dateAxis.Minimum = OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.AddDays(-7));

            this.DataContext = this;

            theGrid.Visibility = Visibility.Hidden;
        }

        private void btnPredict_Click(object sender, RoutedEventArgs e)
        {
            DogPredict.Clear();
            var input = new ModelInput();
            string path = FileNameGetter.return_filename();

            img.image_setter(imgPicture, path);
            img.image_setter(input, path);                    

            ModelOutput prediction = ConsumeModel.Predict(input);
            Prediction p = new Prediction(prediction.Score);

            LogWriter.Log(p.getDogList());

            DogPlots.Add(new Plot(p.getDogList()[0].GetProbability(), DateTime.Now));

            foreach (Dog d in p.dogList)
            {
                DogPredict.Add(d);
            }

            theGrid.Visibility = Visibility.Visible;
            theGrid.Columns[2].Visibility = Visibility.Hidden;
            theGrid.Columns[3].Visibility = Visibility.Hidden;
            theGrid.Columns[4].Visibility = Visibility.Hidden;

            hfr.InsertIntoColumnsList(p.dogList[0].Breed_Name);

            bool newBreed = true;
            foreach(BarColumn b in CurrentBreeds)
            {
                if(b.breedLabel == p.dogList[0].Breed_Name)
                {
                    b.breedFreq += 1; 
                    newBreed = false;
                }
            }

            if(newBreed)
            {
                CurrentBreeds.Add(new BarColumn(p.dogList[0].Breed_Name));
            }
        }      
    }
}
