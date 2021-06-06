using System;
using System.Collections.Generic;
using System.Text;

namespace Location
{
    public interface IDataLayer
    {
        List<Client> Clients { get; }

        List<Vehicule> Vehicules { get; }

        List<Reservation> Reservations { get; }

        //.... vehicules , reservation..
    }
}
