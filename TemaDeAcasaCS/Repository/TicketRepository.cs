using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaDeAcasaCSNetwork.Domain;

namespace TemaDeAcasaCSNetwork.Repository {
    public class TicketRepository : ICrudRepository<int, Ticket> {
        //private DBUtils dbutils;
        //private static readonly ILog log = LogManager.GetLogger("SortingTaskDbRepository");
        public TicketRepository() {
            //log.Info("Creating SortingTaskDbRepository");
        }

        public Ticket findOne(int id) {
            //log.InfoFormat("Entering findOne with value {0}", id);
            SQLiteConnection con = DBUtils.getConnection();

            using (var comm = new SQLiteCommand("SELECT * FROM Tickets WHERE id=@id", con)) {
                comm.Parameters.AddWithValue("@id", id);
                using (var dataR = comm.ExecuteReader()) {
                    if (dataR.Read()) {
                        int idV = dataR.GetInt32(0);
                        int gameId = dataR.GetInt32(1);
                        String client = dataR.GetString(2);
                        int nrOfSeats = dataR.GetInt32(3);
                        Ticket ticket = new Ticket(idV, gameId, client, nrOfSeats);
                        //log.InfoFormat("Exiting findOne with value {0}", task);
                        return ticket;
                    }
                }
            }
            //log.InfoFormat("Exiting findOne with value {0}", null);
            return null;
        }

        public IEnumerable<Ticket> findAll() {
            SQLiteConnection con = DBUtils.getConnection();
            IList<Ticket> tickets = new List<Ticket>();
            using (var comm = new SQLiteCommand("SELECT * FROM Tickets", con)) {
                using (SQLiteDataReader dataR = comm.ExecuteReader()) {
                    while (dataR.Read()) {
                        int idV = dataR.GetInt32(0);
                        int gameId = dataR.GetInt32(1);
                        String client = dataR.GetString(2);
                        int nrOfSeats = dataR.GetInt32(3);
                        Ticket ticket = new Ticket(idV, gameId, client, nrOfSeats);
                        //log.InfoFormat("Exiting findOne with value {0}", task);
                        tickets.Add(ticket);
                    }
                }
            }
            return tickets;
        }

        public void save(Ticket entity) {
            var con = DBUtils.getConnection();
            using (var comm = new SQLiteCommand("INSERT INTO Tickets VALUES (@id,@gameid,@client,@nr)", con)) {
                comm.Parameters.AddWithValue("@id", entity.Id);
                comm.Parameters.AddWithValue("@gameid", entity.GameId);
                comm.Parameters.AddWithValue("@client", entity.ClientName);
                comm.Parameters.AddWithValue("@nr", entity.NrOfSeats);
                var result = comm.ExecuteNonQuery();
            }
        }
        public void delete(int id) {
            SQLiteConnection con = DBUtils.getConnection();
            using (var comm = new SQLiteCommand("DELETE FROM * Tickets WHERE id=@id", con)) {
                comm.Parameters.AddWithValue("@id", id);
                var dataR = comm.ExecuteNonQuery();
            }
        }

        public void update(int id, Ticket entity) {
            var con = DBUtils.getConnection();
            using (var comm = new SQLiteCommand("UPDATE Tickets SET id=@id, gameId=@gameid, client=@client, nrOfSeats=@nr WHERE id=@idsearched", con)) {
                comm.Parameters.AddWithValue("@id", entity.Id);
                comm.Parameters.AddWithValue("@gameid", entity.GameId);
                comm.Parameters.AddWithValue("@client", entity.ClientName);
                comm.Parameters.AddWithValue("@nr", entity.NrOfSeats);
                comm.Parameters.AddWithValue("@idsearched", id);
                var result = comm.ExecuteNonQuery();
            }
        }
    }
}
