using MyServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using TemaDeAcasaCSNetwork.Domain;

namespace MyClient {
    public class Client :IClient{
        public event EventHandler<UserEventArgs> updateEvent;
        private readonly IServer server;
        private Seller currentSeller;
        
        public Client(IServer server) {
            this.server = server;
            currentSeller = null;
        }

        public void login(string username, string password) {
            Seller seller = new Seller(username, password);
            Console.WriteLine("Apelez server login");
            server.login(seller, this);
            currentSeller = seller;
        }

        public void logout() {
            server.logout(currentSeller, this);
            currentSeller = null;
        }
        public void sellTicket(string clientName,int nrOfSeats,int idGame) {
            server.sellTicket(clientName, nrOfSeats, idGame);
        }
        public IEnumerable<Game> GetGames() {
            return server.GetGames();
        }

        public void refresh(Game game) {
            UserEventArgs userArgs = new UserEventArgs(UserEvent.Refresh,game);
            OnUserEvent(userArgs);
        }
        protected virtual void OnUserEvent(UserEventArgs e) {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }
    }
}
