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
using Pra.Bieren.Core.Entities;
using Pra.Bieren.Core.Services;


namespace Pra.Bieren.WPF
{
    /// <summary>
    /// Interaction logic for winBierSoorten.xaml
    /// </summary>
    public partial class winBierSoorten : Window
    {

        BierService bierService = new BierService();
        public bool refreshParent = false;
        public winBierSoorten()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateSoorten();
        }
        private void PopulateSoorten()
        {
            lstBiersoorten.ItemsSource = bierService.GetBierSoorten();
            lstBiersoorten.Items.Refresh();
        }
        private void btnSaveNew_Click(object sender, RoutedEventArgs e)
        {
            string soort = txtNew.Text;
            BierSoort bierSoort = new BierSoort(soort);
            int id = bierService.AddBierSoort(bierSoort);
            if(id > 0)
            {
                refreshParent = true;
                this.Close();
            }
        }

        private void btnSaveCurrent_Click(object sender, RoutedEventArgs e)
        {
            if (lstBiersoorten.SelectedItem != null)
            {
                BierSoort bierSoort = (BierSoort)lstBiersoorten.SelectedItem;
                bierSoort.Soort = txtEdit.Text;
                if (bierService.UpdateBierSoort(bierSoort))
                {
                    refreshParent = true;
                    this.Close();
                }
            }
        }

        private void btnDeleteCurrent_Click(object sender, RoutedEventArgs e)
        {
            if (lstBiersoorten.SelectedItem != null)
            {
                BierSoort bierSoort = (BierSoort)lstBiersoorten.SelectedItem;
                if (bierService.DeleteBierSoort(bierSoort))
                {
                    refreshParent = true;
                    this.Close();
                }
            }
        }

        private void lstBiersoorten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtNew.Text = "";
            txtEdit.Text = "";
            if(lstBiersoorten.SelectedItem != null)
            {
                txtEdit.Text = ((BierSoort)lstBiersoorten.SelectedItem).Soort;
            }
        }
    }
}
