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
using Pra.Bieren.Core.Services;
using Pra.Bieren.Core.Entities;

namespace Pra.Bieren.WPF
{

    public partial class winBieren : Window
    {
        public winBieren()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        BierService bierService = new BierService();
        bool isNew;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateBieren();
            PopulateBiersoorten();
            grpDetails.IsEnabled = false;
            grpBieren.IsEnabled = true;
        }
        private void PopulateBieren()
        {
            lstBieren.ItemsSource = bierService.GetBieren();
            lstBieren.Items.Refresh();
        }
        private void PopulateBiersoorten()
        {
            cmbBiersoort.ItemsSource = bierService.GetBierSoorten();
            cmbBiersoort.Items.Refresh();
        }
        private void ClearControls()
        {
            txtAlcohol.Text = "";
            txtNaam.Text = "";
            cmbBiersoort.SelectedIndex = -1;
            sldScore.Value = 1;
        }
        private void LstBieren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if(lstBieren.SelectedItem != null)
            {
                Bier bier = (Bier)lstBieren.SelectedItem;
                txtNaam.Text = bier.Naam;
                txtAlcohol.Text = bier.Alcohol.ToString("0.00");
                sldScore.Value = bier.Score;
                cmbBiersoort.SelectedValue = bier.BierSoortId;
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            grpBieren.IsEnabled = false;
            grpDetails.IsEnabled = true;
            txtNaam.Focus();
            isNew = true;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(lstBieren.SelectedItem != null)
            {
                grpBieren.IsEnabled = false;
                grpDetails.IsEnabled = true;
                txtNaam.Focus();
                isNew = false;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(lstBieren.SelectedItem != null)
            {
                if(MessageBox.Show("Mag dit bier verdwijnen?", "Bier wissen", MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
                {
                    if(bierService.DeleteBier((Bier)lstBieren.SelectedItem))
                    {
                        ClearControls();
                        PopulateBieren();
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
            string naam = txtNaam.Text;
            int bierSoortId = int.Parse(cmbBiersoort.SelectedValue.ToString());
            float.TryParse(txtAlcohol.Text, out float alcohol);
            int score =(int)sldScore.Value;
            Bier bier;
            int selectId;
            if(isNew)
            {
                bier = new Bier(naam, bierSoortId, alcohol, score);
                selectId = bierService.AddBier(bier);
                if(selectId == 0)
                {
                    MessageBox.Show("Toevoegen mislukt");
                    return;
                }
            }
            else
            {
                bier = (Bier)lstBieren.SelectedItem;
                bier.Naam = naam;
                bier.Alcohol = alcohol;
                bier.Score = score;
                bier.BierSoortId = bierSoortId;
                selectId = bier.Id;
                if(!bierService.UpdateBier(bier))
                {
                    MessageBox.Show("Wijzigen mislukt");
                    return;
                }
            }
            grpBieren.IsEnabled = true;
            grpDetails.IsEnabled = false;
            PopulateBieren();
            lstBieren.SelectedValue = selectId;


        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            grpBieren.IsEnabled = true;
            grpDetails.IsEnabled = false;
            if(lstBieren.SelectedItem != null)
            {
                LstBieren_SelectionChanged(null, null);
            }
        }

        private void BtnBiersoorten_Click(object sender, RoutedEventArgs e)
        {
            int selectId = -1;
            if (cmbBiersoort.SelectedItem != null) 
                int.Parse(cmbBiersoort.SelectedValue.ToString());

            winBierSoorten window = new winBierSoorten();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();

            if(window.refreshParent)
            {
                PopulateBiersoorten();
                if(selectId != -1)
                    cmbBiersoort.SelectedValue = selectId;
            }

        }
    }
}
