using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Vrt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IzdelavaVrta_Razdelitev : Page
    {
        int pomaknjenx = ZnacilnostiVrta.pomakjenx;
        int pomaknjeny = ZnacilnostiVrta.pomakjeny;

        int velikostX = ZnacilnostiVrta.velikostVrtaX;
        int velikostY = ZnacilnostiVrta.velikostVrtaY;

        PointCollection orgPoints_copy = new PointCollection();

        double[,] koordinati = new double[21, 21];

        public IzdelavaVrta_Razdelitev()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Create the poligon
                var newPolygon = new Polygon() { Name = "novPoligon" };

                Point Point1 = new Point(pomaknjenx, pomaknjeny);
                Point Point2 = new Point(pomaknjenx + Convert.ToInt32(velikostX), pomaknjeny);
                Point Point3 = new Point(pomaknjenx + Convert.ToInt32(velikostX), pomaknjeny + Convert.ToInt32(velikostY));
                Point Point4 = new Point(pomaknjenx, pomaknjeny + Convert.ToInt32(velikostY));

                PointCollection myPointCollection = new PointCollection();

                myPointCollection.Add(Point1);
                myPointCollection.Add(Point2);
                myPointCollection.Add(Point3);
                myPointCollection.Add(Point4);
                newPolygon.Points = myPointCollection;

                newPolygon.SetValue(Polygon.FillProperty, new SolidColorBrush(Colors.SaddleBrown));


                this.drugiGrid.Children.Add(newPolygon);
            }
            catch
            {
                napaka.Text = "Zgodila se je napaka";
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e) //popraj
        {
            napaka.Text = "";

            

            int razdeliPoX = Convert.ToInt32(razdeliX.Text);
            int razdeliPoY = Convert.ToInt32(razdeliY.Text);

            if (razdeliPoX > 4 || razdeliPoY > 4)
            {
                napaka.Text = "največja razdelitev je 4 x 4";
            }
            else
            {
                ZnacilnostiVrta.razdelitevX = razdeliPoX;
                ZnacilnostiVrta.razdelitevY = razdeliPoY;

                var mojPoli = (Polygon)this.glavniGrid.FindName("novPoligon");

                PointCollection orgPoints = new PointCollection();

                try
                {
                    orgPoints = mojPoli.Points;

                    orgPoints_copy = orgPoints;

                    mojPoli.Points = null;
                }
                catch
                {
                    orgPoints = orgPoints_copy;
                }
                


                //razdeli po x
                

                for (int x = 0; x <= razdeliPoX; x++)
                {
                    koordinati[0, x] = ((orgPoints[1].X - pomaknjenx) / Convert.ToInt32(razdeliPoX)) * x;
                }

                //razdeli po y
                for (int x = 0; x <= razdeliPoY; x++)
                {
                    koordinati[x, 0] = ((orgPoints[3].Y - pomaknjeny) / Convert.ToInt32(razdeliPoY)) * x;
                }

                //izračunaj vse kooordinate
                /*
                for (int x = 1; x <= razdeliPoX; x++)
                {
                    for (int y2 = 1; y2 <= razdeliPoY; y2++)
                    {
                        Point novPoint = new Point(koordinati[0, x], koordinati[y2, 0]);

                    }
                }*/

                drugiGrid.Children.Clear();

                int y = 0;
                int barva = 0;

                for (int x = 0; ; x++)
                {
                    barva++;

                    if (x == razdeliPoX)
                    {
                        x = 0;
                        y++;
                    }

                    if (y == razdeliPoY)
                    {
                        break;
                    }


                    var newPolygon = new Polygon() { Name = "novPoligon" + barva };

                    PointCollection points = new PointCollection();


                    for (int i = 0; i < 4; i++)
                    {

                        if (i == 0)
                        {
                            Point tocka0 = new Point(koordinati[0, x] + pomaknjenx, koordinati[y, 0] + pomaknjeny);
                            points.Add(tocka0);
                        }
                        else if (i == 1)
                        {
                            Point tocka1 = new Point(koordinati[0, x + 1] + pomaknjenx, koordinati[y, 0] + pomaknjeny);
                            points.Add(tocka1);
                        }
                        else if (i == 2)
                        {
                            Point tocka2 = new Point(koordinati[0, x + 1] + pomaknjenx, koordinati[y + 1, 0] + pomaknjeny);
                            points.Add(tocka2);
                        }
                        else
                        {
                            Point tocka3 = new Point(koordinati[0, x] + pomaknjenx, koordinati[y + 1, 0] + pomaknjeny);
                            points.Add(tocka3);
                        }
                    }

                    if (barva % 5 == 0)
                    {
                        newPolygon.SetValue(Polygon.FillProperty, new SolidColorBrush(Colors.LightGreen));
                    }
                    else if (barva % 5 == 1)
                    {
                        newPolygon.SetValue(Polygon.FillProperty, new SolidColorBrush(Colors.DarkSeaGreen));
                    }
                    else if (barva % 5 == 2)
                    {
                        newPolygon.SetValue(Polygon.FillProperty, new SolidColorBrush(Colors.DarkGreen));
                    }
                    else if (barva % 5 == 3)
                    {
                        newPolygon.SetValue(Polygon.FillProperty, new SolidColorBrush(Colors.Green));
                    }
                    else
                    {
                        newPolygon.SetValue(Polygon.FillProperty, new SolidColorBrush(Colors.ForestGreen));
                    }


                    newPolygon.Points = points;

                    

                    this.drugiGrid.Children.Add(newPolygon);


                }
            }
        }

        private void naprej_Click(object sender, RoutedEventArgs e)
        {
            ZnacilnostiVrta.koordinati_shranjeni = koordinati;

            Frame.Navigate(typeof(IzdelavaVrta_Dodajanje));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(IzdelavaVrta_Dimenzije));
        }
    }
}
