

namespace EIV.OData.Ejemplo.ViewModel
{
    using System;
    using Telerik.Windows.Data;
    public class LocalidadesViewModel
    {
        private const string PAIS_ID_ELEMENT = "paisId";
        private const string PROVINCIA_ID_ELEMENT = "provinciaId";

        private readonly int paisId = -1;
        private readonly int provinciaId = -1;
        public LocalidadesViewModel(int paisId, int provinciaId)
        {
            this.paisId = paisId;
            this.provinciaId = provinciaId;
        }

        public virtual int GetPaisId
        {
            get
            {
                return this.paisId;
            }
        }

        public virtual int GetProvinciaId
        {
            get
            {
                return this.provinciaId;
            }
        }

        public void AppendFilters(CompositeFilterDescriptorCollection filters)
        {
            FilterDescriptor fd_paisId = null;
            FilterDescriptor fd_provinciaId = null;

            if (filters == null)
            {
                throw new ArgumentNullException();
            }
            if ((this.paisId == -1) || (this.provinciaId == -1))
            {
                return;
            }

            fd_paisId = new FilterDescriptor(PAIS_ID_ELEMENT, FilterOperator.IsEqualTo, this.paisId, false, typeof(int));
            fd_provinciaId = new FilterDescriptor(PROVINCIA_ID_ELEMENT, FilterOperator.IsEqualTo, this.provinciaId, false, typeof(int));

            // Verify this filter does not exist prior to adding it
            filters.Add(fd_paisId);
            filters.Add(fd_provinciaId);
        }
    }
}