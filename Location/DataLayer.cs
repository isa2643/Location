using System;
using System.Collections.Generic;
using System.Text;

namespace Location
{
    internal class DataLayer : IDataLayer
    {
        public List<Client> Clients { get; private set; }

        public DataLayer()
        {
            this.Clients = new List<Client>();
            //Connect à la database...
        }
    }
}
