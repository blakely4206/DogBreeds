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
        public MainWindow()
        {
         //   Breed b = new Breed(1, "n02085620-Chihuahua", "Chihuahua");
         //   Dog d = new Dog(b, 100);
        //    DogPredict.Add(d);
            this.InitializeComponent();
            
            this.PieModel = pieChart2();
            this.ScatterModel = scatter();
            this.DataContext = this;
        }

        public PlotModel PieModel { get; set; }
        public PlotModel ScatterModel { get; set; }
        public ObservableCollection<ContinentItem> Continents { get; private set; }

        private PlotModel pieChart2()
        {
            var plotModel = new PlotModel { Title = "Breed Probability" };

            var pieSeries = new OxyPlot.Series.PieSeries();
            pieSeries.InnerDiameter = 0.2;
            pieSeries.ExplodedDistance = 0;
            pieSeries.Stroke = OxyColors.Black;
            pieSeries.StrokeThickness = 1.0;
            pieSeries.AngleSpan = 360;
            pieSeries.StartAngle = 0;
            plotModel.Series.Add(pieSeries);


            DogPredict = new ObservableCollection<Dog>();

            return plotModel;
        }

        private PlotModel scatter()
        {
            var tmp = new PlotModel { Title = "Predictions over Time"};
            tmp.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Key = "ColorAxis", Palette = OxyPalettes.Jet(30), Minimum = 0.0, Maximum = 1.0 });
            
            var startDate = DateTime.Now.AddDays(-10);
            var endDate = DateTime.Now;

            var minValue = OxyPlot.Axes.DateTimeAxis.ToDouble(hfr.getFirstDate());
            var maxValue = OxyPlot.Axes.DateTimeAxis.ToDouble(endDate);

            tmp.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = AxisPosition.Left, Minimum = 0.0, Maximum = 100.0 });
            tmp.Axes.Add(new OxyPlot.Axes.DateTimeAxis { Position = AxisPosition.Bottom, Minimum = minValue, Maximum = maxValue, StringFormat = "M/d" });
            var s1 = new OxyPlot.Series.LineSeries
            {
                StrokeThickness = 0,
                MarkerSize = 3,
                MarkerStroke = OxyColors.ForestGreen,
                MarkerType = MarkerType.Plus
            };

            foreach (var pt in hfr.plots)
            {
                s1.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(pt.when), pt.probability));
            }

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

            theGrid.ItemsSource = p.dogList;

            LogWriter.Log(p.getDogList());
            DogPredict.Clear();
            foreach(Dog d in p.dogList)
            {
                DogPredict.Add(d);
            }

        }      
    }

    public static class Fern

    {

        public static List<Point> Generate(int n = 1000, double width = 1.0, double height = 1.0)

        {

            // Probabilities

            double[] p = { 0.85, .92, .99, 1.00 };



            // Transformations

            var a1 = new MatrixTransform(new Matrix(0.85, -0.04, 0.04, 0.85, 0, 1.6));

            var a2 = new MatrixTransform(new Matrix(0.20, 0.23, -0.26, 0.22, 0, 1.6));

            var a3 = new MatrixTransform(new Matrix(-0.15, 0.26, 0.28, 0.24, 0, 0.44));

            var a4 = new MatrixTransform(new Matrix(0, 0, 0, 0.16, 0, 0));

            var random = new Random(17);

            var point = new Point(0.5, 0.5);

            var points = new List<Point>();



            // Transformation for [-3,3,0,10] => output coordinates

            var T = new MatrixTransform(new Matrix(width / 6.0, 0, 0, -height / 10.1, width / 2.0, height));



            for (int i = 0; i < n; i++)

            {

                var r = random.NextDouble();



                if (r < p[0])

                    point = a1.Transform(point);

                else if (r < p[1])

                    point = a2.Transform(point);

                else if (r < p[2])

                    point = a3.Transform(point);

                else

                    point = a4.Transform(point);



                points.Add(T.Transform(point));

            }



            return points;

        }

    }
}
