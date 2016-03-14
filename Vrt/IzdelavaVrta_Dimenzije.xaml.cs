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
    public sealed partial class IzdelavaVrta_Dimenzije : Page
    {
        int pomaknjenx = ZnacilnostiVrta.pomakjenx;
        int pomaknjeny = ZnacilnostiVrta.pomakjeny;
        bool dela = true;
        int velikostX;
        int velikostY;
        int velikostX_backup;

        

        public IzdelavaVrta_Dimenzije()
        {
            this.InitializeComponent();
        }

        private void dodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dela = true;
                napaka.Text = "";
                velikostX = Convert.ToInt32(velX.Text);
                velikostY = Convert.ToInt32(velY.Text);
                velikostX_backup = velikostX;


                if (velikostX <= 1000 && velikostY <= 600)
                {

                    try
                    {
                        drugiGrid.Children.Clear();
                        /*
                        objekt.

                        objekt.Visibility = Visibility.Collapsed;*/
                    }
                    catch { }

                    if (velikostX < velikostY)
                    {
                        velikostX = velikostY;
                        velikostY = velikostX_backup;

                        velX.Text = velikostX.ToString();
                        velY.Text = velikostY.ToString();

                        ZnacilnostiVrta.velikostVrtaX = velikostX;
                        ZnacilnostiVrta.velikostVrtaY = velikostY;
                    }
                    else
                    {
                        ZnacilnostiVrta.velikostVrtaX = velikostX;
                        ZnacilnostiVrta.velikostVrtaY = velikostY;
                    }

                    if (dela == true)
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

                            //newPolygon.Visibility = Visibility.Collapsed;
                        }
                        catch
                        {
                            napaka.Text = "Zgodila se je napaka";
                        }
                    }
                }
                else
                {
                    napaka.Text = "Največja velikost je 1000x600";
                }
            }
            catch
            {
                napaka.Text = "Dovoljena so samo števila";
                dela = false;
            }
        }

        private void naprej_Click(object sender, RoutedEventArgs e)
        {
            if (ZnacilnostiVrta.velikostVrtaX > 0 && ZnacilnostiVrta.velikostVrtaY > 0 && ime.Text != "")
            {
                ZnacilnostiVrta.ime = ime.Text;

                Frame.Navigate(typeof(IzdelavaVrta_Razdelitev));
            }
            else
            {
                napaka.Text = "Izpolni vse podatke (velikost in ime)";
            }
        }
    }
}
