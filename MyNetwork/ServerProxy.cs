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
    public class ServerProxy:IServer {
        private string host;
        private int port;
        private IClient client;
        private NetworkStream stream;
        private IFormatter formatter;
        private TcpClient connection;
        private Queue<Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;

        public ServerProxy(string host,int port) {
            this.host = host;
            this.port = port;
            responses = new Queue<Response>();
        }
        
        public void login(Seller seller, IClient client) {
            initializeConnection();
            Console.WriteLine("trimit request de login");
            sendRequest(new LoginRequest(seller));
            Response response = readResponse();
            if(response is OkResponse) {
                this.client = client;
                return;
            }
            if(response is ErrorResponse) {
                ErrorResponse err = (ErrorResponse)response;
                closeConnection();
                throw new Exception(err.Message);
            }
        }

        public void logout(Seller seller, IClient client) {
            sendRequest(new LogoutRequest(seller));
            Response response = readResponse();
            closeConnection();
            if(response is ErrorResponse) {
                ErrorResponse err = (ErrorResponse)response;
                throw new Exception(err.Message);
            }
        }

        public IEnumerable<Game> GetGames() {
            sendRequest(new RequestGames());
            Response response = readResponse();
            if(response is ErrorResponse) {
                ErrorResponse err = (ErrorResponse)response;
                throw new Exception(err.Message);
            }
            GetGames resp = (GetGames)response;
            IEnumerable<Game> games = resp.Games.ToList();
            return games;
        }
        
        public void sellTicket(string clientName, int nrOfSeats, int idGame) {
            sendRequest(new RequestSell(clientName, nrOfSeats, idGame));
            Response response = readResponse();
            if(response is ErrorResponse) {
                ErrorResponse err = (ErrorResponse)response;
                throw new Exception(err.Message);
            }
        }

        public void initializeConnection() {
            try {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(true);
                startReader();
            }catch(Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void startReader() {
            Thread tw = new Thread(run);
            tw.Start();
        }

        private void closeConnection() {
            finished = true;
            try {
                stream.Close();
                connection.Close();
                _waitHandle.Close();
                client = null;
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void sendRequest(Request request) {
            try {
                formatter.Serialize(stream, request);
                stream.Flush();
            }catch(Exception e) {
                Console.WriteLine("Error sending object !" + e);
            }
        }

        private Response readResponse() {
            Response response = null;
            try {
                //_waitHandle.WaitOne();
                Thread.Sleep(1500);
                lock (responses) {
                    response = responses.Dequeue();
                }
            }catch(Exception e) {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        public virtual void run() {
            while (!finished) {
                try {
                    object response = formatter.Deserialize(stream);
                    Console.WriteLine("response received" + response);
                    if(response is UpdateResponse) {
                        handleUpdate((UpdateResponse)response);
                    } else {
                        lock (responses) {
                            responses.Enqueue((Response)response);
                        }
                        _waitHandle.Set();
                    }
                }catch(Exception e) {
                    Console.WriteLine(e);
                }
            }
        }

        private void handleUpdate(UpdateResponse response) {
            if(response is Refresh) {
                try {
                    client.refresh(((Refresh)response).Game);
                }catch(Exception e) {
                    Console.WriteLine(e.StackTrace);
                }
            }
            
        }
    }
}
