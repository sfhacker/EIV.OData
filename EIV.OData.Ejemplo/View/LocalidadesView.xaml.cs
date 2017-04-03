
namespace EIV.OData.Ejemplo.View
{
    using com.cairone.odataexample;
    using Microsoft.OData.Client;
    using System.Windows;
    using ViewModel;

    /// <summary>
    /// Interaction logic for LocalidadesView.xaml
    /// </summary>
    public partial class LocalidadesView : Window
    {
        public LocalidadesView()
        {
            InitializeComponent();
        }

        private void localidadesGridView_Loaded(object sender, RoutedEventArgs e)
        {
            LocalidadesViewModel viewModel = (LocalidadesViewModel)this.DataContext;

            // it should never happen!
            if (viewModel == null)
            {
                return;
            }

            viewModel.AppendFilters(this.localidadesGridView.FilterDescriptors);
        }

        private void localidadesDataSource_SubmittingChanges(object sender, Telerik.Windows.Controls.DataServices.DataServiceSubmittingChangesEventArgs e)
        {
            // I wanted to set this option globally, but it didn't work!
            e.SaveChangesOptions = SaveChangesOptions.ReplaceOnUpdate;
        }

        private void localidadesDataSource_SubmittedChanges(object sender, Telerik.Windows.Controls.DataServices.DataServiceSubmittedChangesEventArgs e)
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

                //this.localidadesDataSource.RejectChanges();

                // It does not remove the (invalid) row from the grid (if new record)
                this.localidadesDataSource.CancelSubmit();

                return;
            }

            this.statusInfo.Text = "Cambios guardados exitosamente.";
        }

        private void localidadesGridView_NewItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            LocalidadesViewModel viewModel = (LocalidadesViewModel)this.DataContext;

            // it should never happen!
            if (viewModel == null)
            {
                return;
            }

            e.NewObject = new Localidad() { paisId = viewModel.GetPaisId, provinciaId = viewModel.GetProvinciaId };
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.localidadesGridView.BeginInsert();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (this.localidadesGridView.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("Debe seleccionar una localidad.", "Localidades", MessageBoxButton.OK, MessageBoxImage.Stop);

                return;
            }

            var itemsToRemove = new System.Collections.ObjectModel.ObservableCollection<object>();

            foreach (var item in this.localidadesGridView.SelectedItems)
            {
                itemsToRemove.Add(item);
            }

            MessageBoxResult rst = System.Windows.MessageBox.Show(string.Format("Esta seguro de querer eliminar {0} item(s)?", itemsToRemove.Count), "Paises", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (rst == MessageBoxResult.Yes)
            {
                foreach (var item in itemsToRemove)
                {
                    (this.localidadesGridView.ItemsSource as Telerik.Windows.Data.DataItemCollection).Remove(item);
                }

                this.statusInfo.Text = string.Format("{0} item(s) fueron eliminados.", itemsToRemove.Count);
            }
        }

        private void btnSaveAll_Click(object sender, RoutedEventArgs e)
        {
            // When the user clicks on 'Save All'
            // but no changes were made
            if (this.localidadesDataSource.HasChanges)
            {
                this.localidadesDataSource.SubmitChanges();
            }
        }

        private void btnCancelAll_Click(object sender, RoutedEventArgs e)
        {
            // http://www.telerik.com/forums/paging-is-not-enabled-when-domaincontext-has-changes
            // http://www.telerik.com/forums/datasource-cancelchanges-not-reflected-in-ui
            this.localidadesDataSource.RejectChanges();

            // is this required?
            //this.localidadesGridView.Rebind();
            this.localidadesGridView.CancelEdit();

            this.statusInfo.Text = string.Empty;
        }

        private void localidadesGridView_CellValidated(object sender, Telerik.Windows.Controls.GridViewCellValidatedEventArgs e)
        {
            var alas = e.ValidationResult;

        }
    }
}
