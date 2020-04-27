using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaDeAcasaCSNetwork;

using System.Net.Sockets;

using System.Threading;
using TemaDeAcasaCSNetwork.Domain;
using TemaDeAcasaCSNetwork.Repository;
using TemaDeAcasaCSNetwork.Service;
using Interfaces;
using MyNetwork;

namespace MyServer {
    class StartServer {
        static void Main(string[] args) {
            ICrudRepository<int, Game> gameRepo = new GameRepository();
            ICrudRepository<int, Ticket> ticketRepo = new TicketRepository();
            ICrudRepository<int, Seller> sellerRepo = new SellerRepository();
            GameService gameService = new GameService(gameRepo);
            TicketService ticketService = new TicketService(ticketRepo);
            SellerService sellerService = new SellerService(sellerRepo);
            IServer serverImpl = new ServerImplement(gameService, ticketService, sellerService);
            foreach (Seller s in sellerService.findAll()) {
                Console.WriteLine(s);
                Console.WriteLine(s.Id);
            }
            MyServer server = new MyServer("127.0.0.1", 55560, serverImpl);
            server.Start();
            Console.WriteLine("Server started ...");
            Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine();
        }
    }
    public class MyServer : ConcurrentServer{
        private IServer server;
        private ClientWorker worker;
        public MyServer(string host,int port, IServer server):base(host,port) {
            this.server = server;
        }
        protected override Thread createWorker(TcpClient client) {
            worker = new ClientWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
}
