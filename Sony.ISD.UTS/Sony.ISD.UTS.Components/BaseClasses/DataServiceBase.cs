using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Configuration.Provider;
using Sony.ISD.WebToolkit.Components;
using Sony.ISD.WebToolkit.Configuration;


namespace Sony.ISD.UTS.Components
{
    public class DataServiceBase
    {
        private static CommonDataProvider _provider = null;
        private static CommonDataProviderCollection _providers = null;
        private static object _lock = new object();

        public static CommonDataProvider Provider
        {
            get { return _provider; }
        }

        public CommonDataProviderCollection Providers
        {
            get { return _providers; }
        }

        internal static void LoadProviders()
        {
            // Avoid claiming lock if providers are already loaded
            if (_provider == null)
            {
                lock (_lock)
                {
                    // Do this again to make sure _provider is still null
                    if (_provider == null)
                    {
                        // Get a reference to the <imageService> section
                        DataServiceSection section = (DataServiceSection)WebConfigurationManager.GetSection("system.web/dataService");

                        // Load registered providers and point _provider
                        // to the default provider
                        _providers = new CommonDataProviderCollection();
                        ProvidersHelper.InstantiateProviders(section.Providers, _providers, typeof(CommonDataProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                            throw new ProviderException("Unable to load default CommonDataProvider");
                    }
                }
            }
        }
    }
}
