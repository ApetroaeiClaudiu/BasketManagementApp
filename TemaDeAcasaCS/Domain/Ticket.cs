using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaDeAcasaCSNetwork.Domain {
    [Serializable]
    public class Ticket : Entity<int> {
        public Ticket(int id, int gameId, string clientName, int nrOfSeats) {
            Id = id;
            GameId = gameId;
            ClientName = clientName;
            NrOfSeats = nrOfSeats;
        }
        public string ClientName { get; set; }
        public int GameId { get; set; }
        public int NrOfSeats { get; set; }



        public override string ToString() {
            return GameId + ClientName + NrOfSeats;
        }
        public override bool Equals(object obj) {
            Ticket ticket = (Ticket)obj;
            return Id == ticket.Id;
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
