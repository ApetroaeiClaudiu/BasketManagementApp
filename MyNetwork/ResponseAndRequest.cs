using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaDeAcasaCSNetwork.Domain;

namespace MyNetwork {

    public interface Request {
    }


    [Serializable]
    public class LoginRequest : Request {
        private Seller user;

        public LoginRequest(Seller user) {
            this.user = user;
        }

        public virtual Seller User {
            get {
                return user;
            }
        }
    }

    [Serializable]
    public class LogoutRequest : Request {
        private Seller user;

        public LogoutRequest(Seller user) {
            this.user = user;
        }

        public virtual Seller User {
            get {
                return user;
            }
        }
    }
    
    [Serializable]
    public class RequestGames : Request {
        public RequestGames() {

        }
        
    }
    
    [Serializable]
    public class RequestSell : Request {
        private string client;
        private int nr;
        private int idgame;
        public RequestSell(string client,int nr,int idgame) {
            this.client = client;
            this.nr = nr;
            this.idgame = idgame;
        }
        public virtual string Client {
            get { return client; }
        }
        public virtual int Nr {
            get { return nr; }
        }
        public virtual int Idgame {
            get { return idgame; }
        }
    }

    public interface Response {
    }

    [Serializable]
    public class OkResponse : Response {

    }

    [Serializable]
    public class ErrorResponse : Response {
        private string message;

        public ErrorResponse(string message) {
            this.message = message;
        }

        public virtual string Message {
            get {
                return message;
            }
        }
    }


    [Serializable]
    public class GetGames : Response
    {
        private Game[] games;

        public GetGames(IEnumerable<Game> games) {
            this.games = games.ToArray();
        }

        public virtual Game[] Games {
            get {
                return games;
            }
        }
    }

    public interface UpdateResponse : Response {
    }

    [Serializable]
    public class Refresh : UpdateResponse
    {
        private Game game;
        public Refresh(Game game) {
            this.game = game;
        }
        public virtual Game Game {
            get { return game; }
        }

    }

}
