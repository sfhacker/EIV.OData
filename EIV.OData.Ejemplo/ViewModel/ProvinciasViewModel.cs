
namespace EIV.OData.Ejemplo.ViewModel
{
    using System;
    using Telerik.Windows.Data;
    public class ProvinciasViewModel
    {
        private const string PAIS_ID_ELEMENT = "paisId";

        private readonly int paisId = -1;
        public ProvinciasViewModel(int paisId)
        {
            this.paisId = paisId;
        }

        public virtual int GetPaisId
        {
            get
            {
                return this.paisId;
            }
        }
        public void AppendFilters(CompositeFilterDescriptorCollection filters)
        {
            FilterDescriptor fd_paisId = null;

            if (filters == null)
            {
                throw new ArgumentNullException();
            }
            if (this.paisId == -1)
            {
                return;
            }

            fd_paisId = new FilterDescriptor(PAIS_ID_ELEMENT, FilterOperator.IsEqualTo, this.paisId, false, typeof(int));

            // Verify this filter does not exist prior to adding it
            filters.Add(fd_paisId);
        }
    }
}