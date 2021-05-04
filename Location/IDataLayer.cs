using System;
using System.Collections.Generic;
using System.Text;

namespace Location
{
    public interface IDataLayer
    {
        List<Client> Clients { get; }

        //.... vehiclues , reservation..
    }
}
