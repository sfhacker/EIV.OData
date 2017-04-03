
namespace EIV.OData.Ejemplo.Model
{
    using com.cairone.odataexample;
    using System;
    public class DataServiceContext : ODataExample
    {
        private readonly string serviceUri = string.Empty;

        public DataServiceContext() :
            base(new Uri(OData.Ejemplo.Properties.Settings.Default.webServiceURL))
        {
            this.SetDefaults();
        }

        private void SetDefaults()
        {
            // Always PUT method or operation
            /*
            this.SaveChangesDefaultOptions = Microsoft.OData.Client.SaveChangesOptions.ReplaceOnUpdate;

            // Uncomment the code below if the OData Service requires authentication
            this.SendingRequest2 += (sender, eventArgs) =>
            {
                eventArgs.RequestMessage.SetHeader(headerName: "Authorization",
                    headerValue: "Basic YWRta.....");
            };*/
        }
    }
}