using System;
using System.Linq;

namespace Location
{
    public class Locations
    {
        private IDataLayer _dataLayer;

        public bool UserConnected { get; private set; }

        public Locations()
        {
            /// si on utilisait la librairie au travers d'une application
            this._dataLayer = new DataLayer();
        }

        public Locations(IDataLayer dataLayer)
        {
            this._dataLayer = dataLayer;
        }

        public string ConnectUser(string identifiant, string mdp)
        {
            Client client = this._dataLayer.Clients.SingleOrDefault(_ => _.Identifiant == identifiant);
            if (client == null)
            {
                this.UserConnected = false;
                return "Identifiant non reconnu";
            }
            else
            {
                if (client.Mdp == mdp)
                {
                    this.UserConnected = true;
                }
                else
                {
                    this.UserConnected = false;
                    return "Mot de passe incorrect";
                }
            }

            return "";
        }
    }
}
