using Interfaces;
using MyServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TemaDeAcasaCSNetwork.Domain;

namespace MyNetwork {
    public class ClientWorker : IClient {
        private IServer server;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;
        public ClientWorker(IServer server, TcpClient connection) {
            this.server = server;
            this.connection = connection;
            try {

                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void refresh(Game game) {
            try {
                sendResponse(new Refresh(game));
            }catch(Exception e) {
                Console.WriteLine(e);
            }
        }

        public virtual void run() {
            while (connected) {
                try {
                    object request = formatter.Deserialize(stream);
                    object response = handleRequest((Request)request);
                    if (response != null) {
                        sendResponse((Response)response);
                    }
                } catch (Exception e) {
                    Console.WriteLine(e.StackTrace);
                }

                try {
                    Thread.Sleep(1000);
                } catch (Exception e) {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try {
                stream.Close();
                connection.Close();
            } catch (Exception e) {
                Console.WriteLine("Error " + e);
            }
        }

        private Response handleRequest(Request request) {
            Response response = null;
            if (request is LoginRequest) {
                Console.WriteLine("Login request ...");
                LoginRequest logReq = (LoginRequest)request;
                Seller seller  = logReq.User;
                try {
                    lock (server) {
                        server.login(seller, this);
                    }
                    return new OkResponse();
                } catch (Exception e) {
                    connected = false;
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is LogoutRequest) {
                Console.WriteLine("Logout Request ...");
                LogoutRequest logReq = (LogoutRequest)request;
                Seller seller = logReq.User;
                try {
                    lock (server) {
                        server.logout(seller, this);
                    }
                    connected = false;  

                    return new OkResponse();
                } catch (Exception e) {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is RequestGames) {
                Console.WriteLine("GetGames Request ...");
                RequestGames getReq = (RequestGames)request;
                try {
                    IEnumerable<Game> games;
                    lock (server) {
                        games = server.GetGames();
                    }
                    return new GetGames(games);
                } catch (Exception e) {
                    return new ErrorResponse(e.Message);
                }
            }
            if(request is RequestSell) {
                Console.WriteLine("Requesting to sell");
                RequestSell getReq = (RequestSell)request;
                string clientName = getReq.Client;
                int nrOfSeats = getReq.Nr;
                int idGame = getReq.Idgame;
                try {
                    lock (server) {
                        server.sellTicket(clientName, nrOfSeats, idGame);
                    }
                    return new OkResponse();
                }catch(Exception e) {
                    return new ErrorResponse(e.Message);
                }
            }
            return response;
        }

        private void sendResponse(Response response) {
            Console.WriteLine("sending response " + response);
            formatter.Serialize(stream, response);
            stream.Flush();

        }
    }
}
