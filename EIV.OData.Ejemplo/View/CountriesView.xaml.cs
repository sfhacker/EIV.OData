
namespace EIV.OData.Ejemplo.View
{
    using com.cairone.odataexample;
    using Microsoft.OData.Client;
    using System.Windows;
    using Telerik.Windows;
    using ViewModel;

    /// <summary>
    /// Interaction logic for CountriesView.xaml
    /// </summary>
    public partial class CountriesView : Window
    {
        public CountriesView()
        {
            InitializeComponent();

            //this.paisesDataSource.DataServiceContext = new EIV.OData.Service.DataServiceContext();
        }

        private void radContextMenu_ItemClick(object sender, RadRoutedEventArgs e)
        {
            if (this.paisesGridView.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("Debe seleccionar un pais.", "Paises", MessageBoxButton.OK, MessageBoxImage.Stop);

                return;
            }

            if (this.paisesGridView.SelectedItems.Count > 1)
            {
                System.Windows.MessageBox.Show("Debe seleccionar un solo pais.", "Paises", MessageBoxButton.OK, MessageBoxImage.Stop);

                return;
            }

            Pais newPais = (Pais)this.paisesGridView.SelectedItems[0];
            if (newPais == null)
            {
                return;
            }

            var provinciasWin = new ProvinciasView();
            var provinciasViewModel = new ProvinciasViewModel(newPais.id);

            provinciasWin.DataContext = provinciasViewModel;

            provinciasWin.ShowDialog();

        }

        private void paisesDataSource_LoadedData(object sender, Telerik.Windows.Controls.DataServices.LoadedDataEventArgs e)
        {
            if (e.HasError)
            {
                e.MarkErrorAsHandled();
                this.statusInfo.Text = string.Format("Error: {0}", e.Error.Message);
            }
            else if (e.Cancelled)
            {
                this.statusInfo.Text = string.Format("Load operation was cancelled.");
            }
            else
            {
                this.statusInfo.Text = "Server is content!";
            }
        }

        private void paisesGridView_DataLoaded(object sender, System.EventArgs e)
        {
            if (this.paisesGridView.CurrentCell != null)
            {
                this.paisesGridView.CurrentCell.IsSelected = false;
            }
        }

        // When the grid is first loaded,
        // the first row is always selected
        // if then the user clicks on'Delete' button
        // well, you know what happens
        // is there any other way of doing this?
        private void paisesGridView_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            if (e.Row.IsSelected)
            {
                e.GridViewDataControl.CurrentItem = null;
            }
        }

        // Remove from here!
        private void paisesGridView_NewItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            // No need for this
            //e.NewObject = new Pais();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.paisesGridView.BeginInsert();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (this.paisesGridView.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("Debe seleccionar un pais.", "Paises", MessageBoxButton.OK, MessageBoxImage.Stop);

                return;
            }

            var itemsToRemove = new System.Collections.ObjectModel.ObservableCollection<object>();

            foreach (var item in this.paisesGridView.SelectedItems)
            {
                itemsToRemove.Add(item);
            }

            MessageBoxResult rst = System.Windows.MessageBox.Show(string.Format("Esta seguro de querer eliminar {0} item(s)?", itemsToRemove.Count), "Paises", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (rst == MessageBoxResult.Yes)
            {
                foreach (var item in itemsToRemove)
                {
                    (this.paisesGridView.ItemsSource as Telerik.Windows.Data.DataItemCollection).Remove(item);
                }

                this.statusInfo.Text = string.Format("{0} item(s) fueron eliminados.", itemsToRemove.Count);
            }
        }

        private void btnSaveAll_Click(object sender, RoutedEventArgs e)
        {
            // when the user clicks on 'Save All'
            // but no changes were made
            if (this.paisesDataSource.HasChanges)
            {
                this.paisesDataSource.SubmitChanges();
            }
        }

        private void btnCancelAll_Click(object sender, RoutedEventArgs e)
        {
            this.paisesDataSource.RejectChanges();
        }

        private void paisesDataSource_SubmittingChanges(object sender, Telerik.Windows.Controls.DataServices.DataServiceSubmittingChangesEventArgs e)
        {
            // I wanted to set this option globally, but it didn't work!
            e.SaveChangesOptions = SaveChangesOptions.ReplaceOnUpdate;
        }

        private void paisesDataSource_SubmittedChanges(object sender, Telerik.Windows.Controls.DataServices.DataServiceSubmittedChangesEventArgs e)
        {
            string errorMsg = string.Empty;

            if (e.HasError)
            {
                e.MarkErrorAsHandled();
                errorMsg = string.Format("Error: {0}", e.Error.Message);
                if (e.Error.InnerException != null)
                {
                    errorMsg += string.Format("\r\nExtra error info: {0}", e.Error.InnerException.Message);
                }

                this.statusInfo.Text = "Some error here!";   //  errorMsg;
                this.paisesDataSource.RejectChanges();
            }
        }
    }
}