using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zeneiskolasok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal List<Zenedarab> zenedarabok = new List<Zenedarab>();
        public MainWindow()
        {
            InitializeComponent();

            foreach (var item in File.ReadAllLines(@"..\..\..\src\zene.txt"))
            {
                zenedarabok.Add(new(item));
            }
            listboxZeneiDarabok.ItemsSource = zenedarabok.Select(z => z.Cim);

        }

        private void Szerzok_Listazas(object sender, RoutedEventArgs e)
        {
            listboxZeneszerzok.ItemsSource = zenedarabok.Select(z => z.Szerzo).Distinct().OrderBy(x => x);
        }

        private void DarabKeletkezese_Listazas(object sender, RoutedEventArgs e)
        {
            textblockDarabokKeletkezesiEve.Text = $"A komolyzenei darabok keletkezési éve {zenedarabok.Min(z => z.KeletkezesiEv)} és {zenedarabok.Max(z => z.KeletkezesiEv)} között terjed.";
            textblockDarabokKeletkezesiEve.Visibility = Visibility.Visible;
        }

        private void TizesNehezseguDarabokSzama_Listazas(object sender, RoutedEventArgs e)
        {
            textblockTizesNehezseguDarabokSzama.Text = $"Az adatfájlban {zenedarabok.Count(z => z.Nehezseg == 10)} db 10-es nehézségű komolyzenei darab van.";
            textblockTizesNehezseguDarabokSzama.Visibility = Visibility.Visible;
        }

        private void DebussySzamai_Listazas(object sender, RoutedEventArgs e)
        {
            textblockDebussySzamai.Text = $"Az adatfájlban {zenedarabok.Count(z => z.Szerzo.Contains("Debussy"))} db Debussy szerzőjű komolyzenei darab van.";
            textblockDebussySzamai.Visibility = Visibility.Visible;
        }
    }
}