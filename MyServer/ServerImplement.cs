using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TemaDeAcasaCSNetwork.Domain;
using TemaDeAcasaCSNetwork.Service;

namespace MyServer {
    class ServerImplement : IServer{
        private GameService gameService;
        private TicketService ticketService;
        private SellerService sellerService;
        private readonly IDictionary<int, IClient> loggedClients;

        public ServerImplement(GameService gameService,TicketService ticketService,SellerService sellerService) {
            this.gameService = gameService;
            this.ticketService = ticketService;
            this.sellerService = sellerService;
            loggedClients = new Dictionary<int, IClient>();
        }

        public void login(Seller seller,IClient client) {
            Seller exist = null ;
            foreach(Seller s in sellerService.findAll()) {
                if(s.Username==seller.Username && s.Password == seller.Password) {
                    exist = s;
                }
            }
            if (exist != null) {
                if (loggedClients.ContainsKey(exist.Id)) {
                    throw new Exception("User Already Logged In !");
                }
                loggedClients[exist.Id] = client;
                foreach(int key in loggedClients.Keys) {
                    IClient value = loggedClients[key];
                    Console.WriteLine(key);
                }
            } else {
                throw new Exception("Failed Credentials !");
            }
        }
        
        public void logout(Seller seller, IClient client) {
            IClient localClient = loggedClients[seller.Id];
            if(localClient == null) {
                throw new Exception("The user is not logged in !");
            }
            Console.WriteLine(seller.Id);
            loggedClients.Remove(seller.Id);
        }
        public IEnumerable<Game> GetGames() {
            IEnumerable<Game> games = gameService.sortDescending();
            return games;
        }

        public void sellTicket(string clientName, int nrOfSeats, int idGame) {
            Thread.Sleep(1500);
            int id = ticketService.getBiggestId() + 1;
            Ticket ticket = new Ticket(id, idGame, clientName, nrOfSeats);
            ticketService.add(ticket);
            Game game = gameService.findOne(idGame);
            game.NrOfEmptySeats = game.NrOfEmptySeats - nrOfSeats;
            gameService.update(idGame, game);
            notifyAllRefresh(game);
        }
        public IDictionary<int, IClient> getClients() {
            return loggedClients;
        }
        private void notifyAllRefresh(Game game) {
            IEnumerable<Seller> sellers = sellerService.findAll();
            foreach (Seller us in sellers) {
                if (loggedClients.ContainsKey(us.Id)) {
                    IClient client = loggedClients[us.Id];
                    Task.Run(() => client.refresh(game));
                }
            }
        }
    }
}
