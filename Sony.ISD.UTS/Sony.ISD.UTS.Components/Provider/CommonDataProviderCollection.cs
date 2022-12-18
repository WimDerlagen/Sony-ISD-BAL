using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Configuration.Provider;

namespace Sony.ISD.UTS.Components
{
    public class CommonDataProviderCollection : ProviderCollection
    {
        public new CommonDataProvider this[string name]
        {
            get { return (CommonDataProvider)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (!(provider is CommonDataProvider))
                throw new ArgumentException
                    ("Invalid provider type", "provider");

            base.Add(provider);
        }

    }
}
