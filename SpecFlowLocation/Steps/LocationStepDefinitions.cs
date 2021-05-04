using FluentAssertions;
using Location;
using SpecFlowLocation.Fake;
using TechTalk.SpecFlow;

namespace SpecFlowLocation.Steps
{
    [Binding]
    public sealed class LocationStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;

        private string _identifiant;
        private string _mdp;
        private string _lastErrorMessage;
        private Locations _target;
        private FakeDataLayer _fakeDataLayer;

        public LocationStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._fakeDataLayer = new FakeDataLayer();
            this._target = new Locations(this._fakeDataLayer);
        }

        #region Background

        [Given(@"suivant les clients existants")]
        public void GivenFollowingExistingClients(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                this._fakeDataLayer.Clients.Add(new Client(row[0], row[1]));
            }
        }

        #endregion

        [Given(@"mon identifiant est ""(.*)""")]
        public void GivenMyUsernameIs(string identifiant)
        {
            this._identifiant = identifiant;
        }

        [Given(@"mon mot de passe est ""(.*)""")]
        public void GivenMyPasswordIs(string mdp)
        {
            this._mdp = mdp;
        }

        [When(@"J'essaie de me connecter avec mon compte")]
        public void WhenITryToConnectToMyAccount()
        {
            this._lastErrorMessage = this._target.ConnectUser(this._identifiant, this._mdp);
        }

        [Then(@"la connexion est refusée")]
        public void ThenTheConnectionIsRefused()
        {
            this._target.UserConnected.Should().BeFalse();
        }

        [Then(@"le message d'erreur est ""(.*)""")]
        public void ThenTheErrorMessageIs(string errorMessage)
        {
            this._lastErrorMessage.Should().Be(errorMessage);
        }

        [Then(@"la connexion est etabli")]
        public void ThenTheConnectionIsEstablished()
        {
            this._target.UserConnected.Should().BeTrue();
        }
    }
}
