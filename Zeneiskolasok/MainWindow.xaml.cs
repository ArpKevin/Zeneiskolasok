using System.IO;
using System.Linq;
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

        private void ValasztottDarabok_Generalas(object sender, RoutedEventArgs e)
        {
            using StreamWriter swZenemuvek = new(@"..\..\..\src\zenemuvek.txt");

            List<Zenedarab> randomGeneraltZenedarabok = new();

            while (!(randomGeneraltZenedarabok.Count == 15))
            {
                Random r = new Random();
                var randomZenedarab = zenedarabok[r.Next(zenedarabok.Count)];
                if (!(randomGeneraltZenedarabok.Contains(randomZenedarab))) randomGeneraltZenedarabok.Add(randomZenedarab);
            }

            swZenemuvek.WriteLine($"Átlagos nehézség: {Math.Round(randomGeneraltZenedarabok.Average(r => r.Nehezseg), 0)}");

            foreach (var item in randomGeneraltZenedarabok)
            {
                swZenemuvek.Write($"{item.Cim} {item.Szerzo}|");
            }
        }

        private void Darab_Kereses(object sender, RoutedEventArgs e)
        {
            if (!(zenedarabok.Exists(z => z.Cim.Contains(textboxKeresettDarab.Text))))
            {
                MessageBox.Show("Nincs ilyen zenedarab!", "Hiba!");
                textboxKeresettDarab.Clear();
            }
            else
            {
                var talaltZenedarabok = zenedarabok.Where(z => z.Cim.Contains(textboxKeresettDarab.Text)).Select(z => z.Cim).ToList();
                listboxTalaltDarabok.ItemsSource = talaltZenedarabok;
                listboxTalaltDarabok.Visibility = Visibility.Visible;

                if (talaltZenedarabok.Count > 1)
                {
                    Random r = new Random();
                    var sorsoltErtek = talaltZenedarabok[r.Next(talaltZenedarabok.Count)];
                    sorsoltDarab.Content = sorsoltErtek;
                    sorsoltDarab.Visibility = Visibility.Visible;

                    hiddenTextblock.Visibility = Visibility.Visible;

                    hiddenButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void hiddenButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}