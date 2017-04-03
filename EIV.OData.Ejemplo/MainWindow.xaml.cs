
namespace EIV.OData.Ejemplo
{
    using System;
    using System.Windows;
    using Telerik.Windows.Controls;
    using View;
    using ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow;

            this.statusInfo.Text = "Ready ...";

            this.statusDateTime.Text = string.Format("{0}-{1}-{2} {3}", dateTime.Year, dateTime.Month, dateTime.Day, dateTime.ToShortTimeString());
        }

        // This seems not ok!!!!!!
        private void archivoMenuItem_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            this.Close();
        }

        private void paisesMenuItem_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            var paisesWin = new CountriesView();
            var paisesViewModel = new CountriesViewModel();

            paisesWin.DataContext = paisesViewModel;

            paisesWin.ShowDialog();
        }

        private void MainMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            // Not very useful, is it?
            RadMenu ItemClicked = sender as RadMenu;
        }
    }
}