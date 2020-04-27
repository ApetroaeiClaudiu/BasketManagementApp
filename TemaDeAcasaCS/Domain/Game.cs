using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaDeAcasaCSNetwork.Domain {
    [Serializable]
    public class Game : Entity<int> {
        public Game(int id,string homeTeam,string awayTeam, TypeOfGame type,int totalNrOfSeats, int nrOfEmptySeats,float price) {
            Id = id;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Type = type;
            TotalNrOfSeats = totalNrOfSeats;
            NrOfEmptySeats = nrOfEmptySeats;
            Price = price;
        }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public TypeOfGame Type { get; set; }
        public int TotalNrOfSeats { get; set; }
        public int NrOfEmptySeats { get; set; }
        public float Price { get; set; }



        public override string ToString() {
            return HomeTeam + AwayTeam + Type + Price;
        }
        public override bool Equals(object obj) {
            Game game = (Game)obj;
            return Id == game.Id;
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
