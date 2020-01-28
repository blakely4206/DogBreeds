using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using OxyPlot;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System.Collections.ObjectModel;
using OxyPlot.Axes;
using LinearColorAxis = OxyPlot.Axes.LinearColorAxis;

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
            hfr.Read();

            DogPlots = new ObservableCollection<Plot>(hfr.plots);
            DogPredict = new ObservableCollection<Dog>();
            CurrentBreeds = new ObservableCollection<BarColumn>(hfr.columns);

            this.InitializeComponent();

            this.BarModel = barChart();
            this.ScatterModel = scatterChart();

            this.DataContext = this;
        }

        public PlotModel BarModel { get; set; }
        public PlotModel ScatterModel { get; set; }

        private PlotModel barChart()
        {
            var plotModel = new PlotModel { Title = "Breeds of the Week" };

            var barSeries = new OxyPlot.Series.BarSeries
            {
                
            };

            barSeries.ItemsSource = CurrentBreeds;
            plotModel.Series.Add(barSeries);

            return plotModel;
        }

        private PlotModel scatterChart()
        {
            DogPlots = new ObservableCollection<Plot>(hfr.plots);
            var tmp = new PlotModel { Title = "Predictions over Time"};
            tmp.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Key = "ColorAxis", Palette = OxyPalettes.Jet(30), Minimum = 0.0, Maximum = 1.0 });
            
            var startDate = DateTime.Now.AddDays(-7);
            var endDate = DateTime.Now;

            var minValue = OxyPlot.Axes.DateTimeAxis.ToDouble(hfr.getFirstDate());
            var maxValue = OxyPlot.Axes.DateTimeAxis.ToDouble(endDate);

            tmp.Axes.Add(new OxyPlot.Axes.LinearAxis { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Left, Minimum = 0.0, Maximum = 100.0});
            tmp.Axes.Add(new OxyPlot.Axes.DateTimeAxis { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Bottom, Minimum = minValue, Maximum = maxValue, IntervalType = DateTimeIntervalType.Weeks, StringFormat = "MM/dd/yy" });

            var s1 = new OxyPlot.Series.LineSeries
            {
                StrokeThickness = 0,
                MarkerSize = 3,
                MarkerStroke = OxyColors.Blue,
                MarkerType = MarkerType.Square                
            };

            s1.ItemsSource = DogPlots;
            s1.DataFieldX = "when";
            s1.DataFieldY = "probability";
            tmp.Series.Add(s1);
            
            return tmp;
        }

        private void btnPredict_Click(object sender, RoutedEventArgs e)
        {
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

            theGrid.ItemsSource = DogPredict.Select(x => new { theGridColumn1 = x.Breed_Name, theGridColumn2 = x.Probability });
            theGrid.Columns[0].Header = "Breed";
            theGrid.Columns[1].Header = "Probability";
        }      
    }
}
