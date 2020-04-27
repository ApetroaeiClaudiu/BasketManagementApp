using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaDeAcasaCSNetwork.Domain;
using System.Data.SQLite;

namespace TemaDeAcasaCSNetwork.Repository {
    public class SellerRepository : ICrudRepository<int, Seller> {
        //private DBUtils dbutils;
        //private static readonly ILog log = LogManager.GetLogger("SortingTaskDbRepository");
        public SellerRepository() {
            //log.Info("Creating SortingTaskDbRepository");
        }

        public Seller findOne(int id) {
            //log.InfoFormat("Entering findOne with value {0}", id);
            SQLiteConnection con = DBUtils.getConnection();

            using (var comm = new SQLiteCommand("SELECT * FROM Sellers WHERE id=@id", con)) {
                comm.Parameters.AddWithValue("@id", id);
                using (var dataR = comm.ExecuteReader()) {
                    if (dataR.Read()) {
                        int idV = dataR.GetInt32(0);
                        String user = dataR.GetString(2);
                        String pass = dataR.GetString(3);
                        Seller seller  = new Seller(idV, user,pass);
                        //log.InfoFormat("Exiting findOne with value {0}", task);
                        return seller;
                    }
                }
            }
            //log.InfoFormat("Exiting findOne with value {0}", null);
            return null;
        }

        public IEnumerable<Seller> findAll() {
            SQLiteConnection con = DBUtils.getConnection();
            IList<Seller> sellers = new List<Seller>();
            using (var comm = new SQLiteCommand("SELECT * FROM Sellers", con)) {
                using (SQLiteDataReader dataR = comm.ExecuteReader()) {
                    while (dataR.Read()) {
                        int idV = dataR.GetInt32(0);
                        String user = dataR.GetString(1);
                        String pass = dataR.GetString(2);
                        Seller seller = new Seller(idV, user, pass);
                        //log.InfoFormat("Exiting findOne with value {0}", task);
                        sellers.Add(seller);
                    }
                }
            }
            return sellers;
        }

        public void save(Seller entity) {
            var con = DBUtils.getConnection();
            using (var comm = new SQLiteCommand("INSERT INTO Seller VALUES (@id,@user,@pass)", con)) {
                comm.Parameters.AddWithValue("@id", entity.Id);
                comm.Parameters.AddWithValue("@user", entity.Username);
                comm.Parameters.AddWithValue("@pass", entity.Password);
                var result = comm.ExecuteNonQuery();
            }
        }
        public void delete(int id) {
            SQLiteConnection con = DBUtils.getConnection();
            using (var comm = new SQLiteCommand("DELETE FROM * Sellers WHERE id=@id", con)) {
                comm.Parameters.AddWithValue("@id", id);
                var dataR = comm.ExecuteNonQuery();
            }
        }

        public void update(int id, Seller entity) {
            var con = DBUtils.getConnection();
            using (var comm = new SQLiteCommand("UPDATE Sellers SET id=@id, username=@user, password=@pass WHERE id=@idsearched", con)) { 
                comm.Parameters.AddWithValue("@id", entity.Id);
                comm.Parameters.AddWithValue("@user", entity.Username);
                comm.Parameters.AddWithValue("@pass", entity.Password);
                comm.Parameters.AddWithValue("@idsearched", id);
                var result = comm.ExecuteNonQuery();
            }
        }
    }
}
