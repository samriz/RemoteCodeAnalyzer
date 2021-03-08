﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCF
{
    class Host
    {
        //define endpoints and open them for the connections
        static ServiceHost CreateChannel(string url)
        {
            //"url" is the url we are going to listen on
            BasicHttpBinding binding = new BasicHttpBinding();
            Uri address = new Uri(url);
            Type service = typeof(Service);
            ServiceHost host = new ServiceHost(service, address);
            host.AddServiceEndpoint(typeof(IBasicService), binding, address);
            return host;
        }
    }
}
