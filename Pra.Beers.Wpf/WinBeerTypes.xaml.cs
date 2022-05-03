using Pra.Beers.Core.Entities;
using Pra.Beers.Core.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Pra.Beers.Wpf
{
    /// <summary>
    /// Interaction logic for winBierSoorten.xaml
    /// </summary>
    public partial class WinBeerTypes : Window
    {

        BeerService beerService = new BeerService();
        public bool refreshParent = false;

        public WinBeerTypes()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateBeerTypes();
        }

        private void PopulateBeerTypes()
        {
            lstBeerTypes.ItemsSource = beerService.GetBeerTypes();
            lstBeerTypes.Items.Refresh();
        }

        private void BtnSaveNew_Click(object sender, RoutedEventArgs e)
        {
            string type = txtNew.Text;
            BeerType beerType = new BeerType(type);
            int id = beerService.AddBeerType(beerType);
            if(id > 0)
            {
                refreshParent = true;
                Close();
            }
        }

        private void BtnSaveCurrent_Click(object sender, RoutedEventArgs e)
        {
            if (lstBeerTypes.SelectedItem != null)
            {
                BeerType beerType = (BeerType)lstBeerTypes.SelectedItem;
                beerType.Type = txtEdit.Text;
                if (beerService.UpdateBeerType(beerType))
                {
                    refreshParent = true;
                    Close();
                }
            }
        }

        private void BtnDeleteCurrent_Click(object sender, RoutedEventArgs e)
        {
            if (lstBeerTypes.SelectedItem != null)
            {
                BeerType beerType = (BeerType)lstBeerTypes.SelectedItem;
                if (beerService.DeleteBeerType(beerType))
                {
                    refreshParent = true;
                    Close();
                }
            }
        }

        private void LstBeerTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtNew.Text = "";
            txtEdit.Text = "";
            if(lstBeerTypes.SelectedItem != null)
            {
                txtEdit.Text = ((BeerType)lstBeerTypes.SelectedItem).Type;
            }
        }
    }
}
