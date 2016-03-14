using SQLitePCL;
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
    public sealed partial class uredi_vrt : Page
    {
        int pomaknjenx = 0;
        int pomaknjeny = 0;
        int velikostx = 0;
        int velikosty = 0;
        int razdelix = 0;
        int razdeliy = 0;

        int[] poz_rastlin = new int[30];

        public uredi_vrt()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            drugiGrid.Children.Clear();

            using (var connection = new SQLiteConnection("baza_vrt.sqlite"))
            {
                using (var statement = connection.Prepare(@"SELECT ime, visina, dolzina, razdelitevX, razdelitevY, lok_rastl FROM vrt WHERE ime='" + ZnacilnostiVrta.izbranVrt + "';"))
                {
                    while (statement.Step() == SQLiteResult.ROW)
                    {
                        ZnacilnostiVrta.ime = statement[0].ToString();
                        ZnacilnostiVrta.velikostVrtaX = Convert.ToInt32(statement[1]);
                        ZnacilnostiVrta.velikostVrtaY = Convert.ToInt32(statement[2]);
                        ZnacilnostiVrta.razdelitevX = Convert.ToInt32(statement[3]);
                        ZnacilnostiVrta.razdelitevY = Convert.ToInt32(statement[4]);
                        ZnacilnostiVrta.lok_rast = statement[5].ToString();
                    }
                }
            }

            ime_vrta.Text = "Moj Vrt -    " + ZnacilnostiVrta.ime;

            int pomaknjenx = ZnacilnostiVrta.pomakjenx;
            int pomaknjeny = ZnacilnostiVrta.pomakjeny;
            int velikostX = ZnacilnostiVrta.velikostVrtaX;
            int velikostY = ZnacilnostiVrta.velikostVrtaY;
            int razdeliX = ZnacilnostiVrta.razdelitevX;
            int razdeliY = ZnacilnostiVrta.razdelitevY;

            /*dolzina.Text = velikostX.ToString();
            sirina.Text = velikostY.ToString();
            dolzinaRaz.Text = razdeliX.ToString();
            sirinaRaz.Text = razdeliY.ToString();*/

            string[] words = ZnacilnostiVrta.lok_rast.Split(',');

            int ii = 0;
            foreach (string word in words)
            {
                try
                {
                    poz_rastlin[ii] = Convert.ToInt32(word);
                    ii++;
                }
                catch { }
            }


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

                newPolygon.SetValue(Polygon.FillProperty, new SolidColorBrush(Colors.Pink));


                this.drugiGrid.Children.Add(newPolygon);
            }
            catch
            {
                napaka.Text = "Zgodila se je napaka";
            }

            napaka.Text = "";



            int razdeliPoX = razdeliX;
            int razdeliPoY = razdeliY;

            if (razdeliPoX > 20 || razdeliPoY > 20)
            {
                napaka.Text = "največja razdelitev je 20 x 20";
            }
            else
            {
                ZnacilnostiVrta.razdelitevX = razdeliPoX;
                ZnacilnostiVrta.razdelitevY = razdeliPoY;

                var mojPoli = (Polygon)this.drugiGrid.FindName("novPoligon");

                PointCollection orgPoints = new PointCollection();
                orgPoints = mojPoli.Points;

                mojPoli.Points = null;


                //razdeli po x
                double[,] koordinati = new double[21, 21];

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


                    var newPolygon = new Polygon() { Name = "novPoligon" };

                    PointCollection points = new PointCollection();

                    TextBlock ime = new TextBlock();

                    Thickness margin = ime.Margin;

                    for (int i = 0; i < 4; i++)
                    {

                        if (i == 0)
                        {
                            Point tocka0 = new Point(koordinati[0, x] + pomaknjenx, koordinati[y, 0] + pomaknjeny);
                            points.Add(tocka0);



                            margin.Left = tocka0.X + 20;
                            margin.Top = tocka0.Y + 20;
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

                    //newPolygon.Tapped += NewPolygon_Tapped;
                    newPolygon.Tag = barva;

                    using (var connection = new SQLiteConnection("baza_vrt.sqlite"))
                    {
                        using (var statement = connection.Prepare(@"SELECT ime FROM rastline WHERE id='" + poz_rastlin[barva - 1] + "';"))
                        {
                            while (statement.Step() == SQLiteResult.ROW)
                            {
                                ime.Text = statement[0].ToString();
                            }
                        }
                    }
                    //ime.Text = rastline.SelectedItem.ToString();


                    ime.Margin = margin;
                    ime.FontSize = 20;
                    ime.Foreground = new SolidColorBrush(Colors.Black);

                    newPolygon.Tapped += NewPolygon_Tapped;
                    newPolygon.Tag = barva;

                    this.drugiGrid.Children.Add(newPolygon);

                    //this.drugiGrid.Children.Add(ime);
                }
            }

            List<string> zadeve = new List<string>();

            using (var connection = new SQLiteConnection("baza_vrt.sqlite"))
            {
                using (var statement = connection.Prepare(@"SELECT ime FROM rastline;"))
                {
                    while (statement.Step() == SQLiteResult.ROW)
                    {
                        zadeve.Add(statement[0].ToString());
                    }
                }
            }

            rastline.ItemsSource = zadeve;
        }

        private void NewPolygon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Polygon polygon = sender as Polygon;
            if (polygon != null)
            {

                int y = 0;
                int barva = 0;

                for (int x = 0; ; x++)
                {
                    barva++;

                    if (x == razdelix)
                    {
                        x = 0;
                        y++;
                    }

                    if (y == razdeliy)
                    {
                        break;
                    }

                    if (barva == (int)polygon.Tag)
                    {
                        Point tocka0 = new Point(ZnacilnostiVrta.koordinati_shranjeni[0, x] + pomaknjenx, ZnacilnostiVrta.koordinati_shranjeni[y, 0] + pomaknjeny);

                        TextBlock ime = new TextBlock();
                        try
                        {
                            ime.Text = rastline.SelectedItem.ToString();

                            Thickness margin = ime.Margin;
                            margin.Left = tocka0.X + 20;
                            margin.Top = tocka0.Y + 20;

                            ime.Margin = margin;

                            ime.Foreground = new SolidColorBrush(Colors.Black);

                            this.drugiGrid.Children.Add(ime);

                            poz_rastlin[barva] = rastline.SelectedIndex;

                            //rastline.SelectedIndex = 0;
                        }
                        catch
                        {
                            napaka.Text = "Izberi rastlino";
                        }

                        break;
                    }
                }
            }
        }
    }
}
