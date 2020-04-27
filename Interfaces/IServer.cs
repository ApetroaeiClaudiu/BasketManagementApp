using Interfaces;
using MyServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaDeAcasaCSNetwork.Domain;

namespace Interfaces {
    public interface IServer {
        void login(Seller seller, IClient client);
        void logout(Seller seller, IClient client);
        IEnumerable<Game> GetGames();
        void sellTicket(string clientName, int nrOfSeats, int idGame);
        //IDictionary<int, IClient> getClients();
    }
}
