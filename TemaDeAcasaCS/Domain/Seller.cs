using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaDeAcasaCSNetwork.Domain {
    [Serializable]
    public class Seller : Entity<int> {
        public Seller(string username, string password):this(-99, username, password) 
        { }
        public Seller(int id, string username, string password) {
            Id = id;
            Username = username;
            Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }



        public override string ToString() {
            return Username + Password;
        }
        public override bool Equals(object obj) {
            Seller seller = (Seller)obj;
            return Id == seller.Id;
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
