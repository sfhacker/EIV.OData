
namespace EIV.OData.Ejemplo.View
{
    using com.cairone.odataexample;
    using Microsoft.OData.Client;
    using System;
    using System.Windows;
    using Telerik.Windows;
    using Telerik.Windows.Controls.GridView;
    using ViewModel;

    /// <summary>
    /// Interaction logic for ProvinciasView.xaml
    /// </summary>
    public partial class ProvinciasView : Window
    {
        public ProvinciasView()
        {
            InitializeComponent();
        }

        private void provinciasGridView_Loaded(object sender, RoutedEventArgs e)
        {
            ProvinciasViewModel viewModel = (ProvinciasViewModel)this.DataContext;

            // it should never happen!
            if (viewModel == null)
            {
                return;
            }

            viewModel.AppendFilters(this.provinciasGridView.FilterDescriptors);
        }

        private void radContextMenu_ItemClick(object sender, RadRoutedEventArgs e)
        {
            if (this.provinciasGridView.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("Debe seleccionar una provincia.", "Provincias", MessageBoxButton.OK, MessageBoxImage.Stop);

                return;
            }

            if (this.provinciasGridView.SelectedItems.Count > 1)
            {
                System.Windows.MessageBox.Show("Debe seleccionar una sola provincia.", "Provincias", MessageBoxButton.OK, MessageBoxImage.Stop);

                return;
            }

            Provincia newProvincia = (Provincia)this.provinciasGridView.SelectedItems[0];
            if (newProvincia == null)
            {
                return;
            }

            ProvinciasViewModel viewModel = (ProvinciasViewModel)this.DataContext;

            // it should never happen!
            if (viewModel == null)
            {
                return;
            }

            var localidadesWin = new LocalidadesView();
            var localidadesViewModel = new LocalidadesViewModel(viewModel.GetPaisId, newProvincia.id);

            localidadesWin.DataContext = localidadesViewModel;

            localidadesWin.ShowDialog();

        }

        private void provinciasGridView_NewItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            ProvinciasViewModel viewModel = (ProvinciasViewModel)this.DataContext;

            // it should never happen!
            if (viewModel == null)
            {
                return;
            }

            e.NewObject = new Provincia() { paisId = viewModel.GetPaisId };
        }

        private void provinciasGridView_DataLoaded(object sender, EventArgs e)
        {
            if (this.provinciasGridView.CurrentCell != null)
            {
                this.provinciasGridView.CurrentCell.IsSelected = false;
            }
        }

        private void provinciasGridView_RowLoaded(object sender, RowLoadedEventArgs e)
        {
            // When the grid is first loaded,
            // the first row is always selected
            // if then the user clicks on'Delete' button
            // well, you know what happens
            // is there any other way of doing this?
            if (e.Row.IsSelected)
            {
                e.GridViewDataControl.CurrentItem = null;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.provinciasGridView.BeginInsert();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (this.provinciasGridView.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("Debe seleccionar una provincia.", "Provincias", MessageBoxButton.OK, MessageBoxImage.Stop);

                return;
            }

            var itemsToRemove = new System.Collections.ObjectModel.ObservableCollection<object>();

            foreach (var item in this.provinciasGridView.SelectedItems)
            {
                itemsToRemove.Add(item);
            }

            MessageBoxResult rst = System.Windows.MessageBox.Show(string.Format("Esta seguro de querer eliminar {0} item(s)?", itemsToRemove.Count), "Provincias", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (rst == MessageBoxResult.Yes)
            {
                foreach (var item in itemsToRemove)
                {
                    (this.provinciasGridView.ItemsSource as Telerik.Windows.Data.DataItemCollection).Remove(item);
                }

                this.statusInfo.Text = string.Format("{0} item(s) fueron enviados para su eliminacion.", itemsToRemove.Count);
            }
        }

        private void provinciasDataSource_SubmittingChanges(object sender, Telerik.Windows.Controls.DataServices.DataServiceSubmittingChangesEventArgs e)
        {
            // I wanted to set this option globally, but it didn't work!
            e.SaveChangesOptions = SaveChangesOptions.ReplaceOnUpdate;
        }

        private void provinciasDataSource_SubmittedChanges(object sender, Telerik.Windows.Controls.DataServices.DataServiceSubmittedChangesEventArgs e)
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

                this.statusInfo.Text = errorMsg;

                //this.provinciasDataSource.RejectChanges();

                // It does not remove the (invalid) row from the grid
                this.provinciasDataSource.CancelSubmit();

                return;
            }

            this.statusInfo.Text = "Cambios guardados exitosamente.";
        }

        private void btnSaveAll_Click(object sender, RoutedEventArgs e)
        {
            // When the user clicks on 'Save All'
            // but no changes were made
            if (this.provinciasDataSource.HasChanges)
            {
                this.provinciasDataSource.SubmitChanges();
                this.statusInfo.Text = "Cambios enviados";
            }
        }

        private void btnCancelAll_Click(object sender, RoutedEventArgs e)
        {
            // it should restore the DataPager
            this.provinciasDataSource.RejectChanges();

            // is this required?
            this.provinciasGridView.Rebind();

            this.statusInfo.Text = string.Empty;
        }
    }
}