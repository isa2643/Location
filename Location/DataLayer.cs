using System;
using System.Collections.Generic;
using System.Text;

namespace Location
{
    internal class DataLayer : IDataLayer
    {
        public List<Client> Clients { get; private set; }

        public List<Vehicule> Vehicules { get; private set; }

        public List<Reservation> Reservations { get; private set; }

        public DataLayer()
        {
            this.Clients = new List<Client>();
            this.Vehicules = new List<Vehicule>();
            this.Reservations = new List<Reservation>();
            //Connect à la database...
        }
    }
}
