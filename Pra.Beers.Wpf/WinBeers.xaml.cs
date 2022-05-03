using Pra.Beers.Core.Entities;
using Pra.Beers.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public partial class WinBeers : Window
    {
        BeerService beerService = new BeerService();
        bool isNew;

        public WinBeers()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateBeers();
            PopulateBeerTypes();
            grpDetails.IsEnabled = false;
            grpBeers.IsEnabled = true;
        }
        private void PopulateBeers()
        {
            lstBeers.ItemsSource = beerService.GetBeers();
            lstBeers.Items.Refresh();
        }

        private void PopulateBeerTypes()
        {
            cmbBeerType.ItemsSource = beerService.GetBeerTypes();
            cmbBeerType.Items.Refresh();
        }

        private void ClearControls()
        {
            txtAlcohol.Text = "";
            txtName.Text = "";
            cmbBeerType.SelectedIndex = -1;
            sldScore.Value = 1;
        }

        private void LstBeers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if(lstBeers.SelectedItem != null)
            {
                Beer beer = (Beer)lstBeers.SelectedItem;
                txtName.Text = beer.Name;
                txtAlcohol.Text = beer.Alcohol.ToString("0.00");
                sldScore.Value = beer.Score;
                cmbBeerType.SelectedValue = beer.BeerTypeID;
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            grpBeers.IsEnabled = false;
            grpDetails.IsEnabled = true;
            txtName.Focus();
            isNew = true;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(lstBeers.SelectedItem != null)
            {
                grpBeers.IsEnabled = false;
                grpDetails.IsEnabled = true;
                txtName.Focus();
                isNew = false;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(lstBeers.SelectedItem != null)
            {
                if(MessageBox.Show("Mag dit bier verdwijnen?", "Bier wissen", MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
                {
                    if(beerService.DeleteBeer((Beer)lstBeers.SelectedItem))
                    {
                        ClearControls();
                        PopulateBeers();
                    }
                    else
                    {
                        MessageBox.Show("Bier niet verwijderd", "DBError", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            int beerTypeId = int.Parse(cmbBeerType.SelectedValue.ToString());
            float.TryParse(txtAlcohol.Text, out float alcohol);
            int score =(int)sldScore.Value;

            Beer beer;
            int selectId;
            if(isNew)
            {
                beer = new Beer(name, beerTypeId, alcohol, score);
                selectId = beerService.AddBeer(beer);
                if(selectId == 0)
                {
                    MessageBox.Show("Toevoegen mislukt");
                    return;
                }
            }
            else
            {
                beer = (Beer)lstBeers.SelectedItem;
                beer.Name = name;
                beer.Alcohol = alcohol;
                beer.Score = score;
                beer.BeerTypeID = beerTypeId;
                selectId = beer.Id;
                if(!beerService.UpdateBeer(beer))
                {
                    MessageBox.Show("Wijzigen mislukt");
                    return;
                }
            }
            grpBeers.IsEnabled = true;
            grpDetails.IsEnabled = false;
            PopulateBeers();
            lstBeers.SelectedValue = selectId;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            grpBeers.IsEnabled = true;
            grpDetails.IsEnabled = false;
            if(lstBeers.SelectedItem != null)
            {
                LstBeers_SelectionChanged(null, null);
            }
        }

        private void BtnBeerTypes_Click(object sender, RoutedEventArgs e)
        {
            int selectId = -1;
            if (cmbBeerType.SelectedItem != null) 
                int.Parse(cmbBeerType.SelectedValue.ToString());

            WinBeerTypes window = new WinBeerTypes();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();

            if(window.refreshParent)
            {
                PopulateBeerTypes();
                if(selectId != -1)
                    cmbBeerType.SelectedValue = selectId;
            }

        }
    }
}
