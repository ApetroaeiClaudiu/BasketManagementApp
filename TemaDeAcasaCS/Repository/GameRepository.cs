using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaDeAcasaCSNetwork.Domain;
using System.Data;
using System.Data.SQLite;

namespace TemaDeAcasaCSNetwork.Repository {
    public class GameRepository : ICrudRepository<int, Game> {
        //private DBUtils dbutils;
        //private static readonly ILog log = LogManager.GetLogger("SortingTaskDbRepository");
        public GameRepository() {
            //log.Info("Creating SortingTaskDbRepository");
        }

        public Game findOne(int id) {
            //log.InfoFormat("Entering findOne with value {0}", id);
            SQLiteConnection con = DBUtils.getConnection();

            using (var comm = new SQLiteCommand("SELECT * FROM Games WHERE id=@id", con)) {
                comm.Parameters.AddWithValue("@id", id);
                using (var dataR = comm.ExecuteReader()) {
                    if (dataR.Read()) {
                        int idV = dataR.GetInt32(0);
                        String homeTeam = dataR.GetString(1);
                        String awayTeam = dataR.GetString(2);
                        TypeOfGame type = (TypeOfGame)Enum.Parse(typeof(TypeOfGame),dataR.GetString(3));
                        int nrOfSeats = dataR.GetInt32(4);
                        int emptySeats = dataR.GetInt32(5);
                        float price = dataR.GetFloat(6);
                        Game game = new Game(idV, homeTeam, awayTeam, type, nrOfSeats, emptySeats, price);
                        //log.InfoFormat("Exiting findOne with value {0}", task);
                        return game;
                    }
                }
            }
            //log.InfoFormat("Exiting findOne with value {0}", null);
            return null;
        }

        public IEnumerable<Game> findAll() {
            SQLiteConnection con = DBUtils.getConnection();
            IList<Game> games = new List<Game>();
            using (var comm = new SQLiteCommand("SELECT * FROM Games", con)) {
                using (SQLiteDataReader dataR = comm.ExecuteReader()) {
                    while (dataR.Read()) {
                        int idV = dataR.GetInt32(0);
                        String homeTeam = dataR.GetString(1);
                        String awayTeam = dataR.GetString(2);
                        TypeOfGame type = (TypeOfGame)Enum.Parse(typeof(TypeOfGame), dataR.GetString(3));
                        int nrOfSeats = dataR.GetInt32(4);
                        int emptySeats = dataR.GetInt32(5);
                        float price = dataR.GetFloat(6);
                        Game game = new Game(idV, homeTeam, awayTeam, type, nrOfSeats, emptySeats, price);
                        games.Add(game);
                    }
                }
            }
            return games;
        }

        public void save(Game entity) {
            var con = DBUtils.getConnection();
            using(var comm = new SQLiteCommand("INSERt INTO Games VALUES (@id,@home,@away,@type,@seats,@empty,@price)", con)) {
                comm.Parameters.AddWithValue("@id", entity.Id);
                comm.Parameters.AddWithValue("@home", entity.HomeTeam);
                comm.Parameters.AddWithValue("@away", entity.AwayTeam);
                comm.Parameters.AddWithValue("@type", entity.Type);
                comm.Parameters.AddWithValue("@seats", entity.TotalNrOfSeats);
                comm.Parameters.AddWithValue("@empty", entity.NrOfEmptySeats);
                comm.Parameters.AddWithValue("@price", entity.Price);
                var result = comm.ExecuteNonQuery();
            }
        }
        public void delete(int id) {
            SQLiteConnection con = DBUtils.getConnection();
            using (var comm = new SQLiteCommand("DELETE FROM * Games WHERE id=@id",con)) {
                comm.Parameters.AddWithValue("@id", id);
                var dataR = comm.ExecuteNonQuery();
            }
        }

        public void update(int id, Game entity) {
            var con = DBUtils.getConnection();
            using (var comm = new SQLiteCommand("UPDATE Games SET id=@id, homeTeam=@home, awayTeam=@away, type=@type, nrOfSeats=@seats,emptySeats=@empty,price=@price WHERE id=@idsearched", con)) {
                comm.Parameters.AddWithValue("@id", entity.Id);
                comm.Parameters.AddWithValue("@home", entity.HomeTeam);
                comm.Parameters.AddWithValue("@away", entity.AwayTeam);
                comm.Parameters.AddWithValue("@type", entity.Type.ToString());
                comm.Parameters.AddWithValue("@seats", entity.TotalNrOfSeats);
                comm.Parameters.AddWithValue("@empty", entity.NrOfEmptySeats);
                comm.Parameters.AddWithValue("@price", entity.Price);
                comm.Parameters.AddWithValue("@idsearched", id);
                var result = comm.ExecuteNonQuery();
            }
        }
    }
}