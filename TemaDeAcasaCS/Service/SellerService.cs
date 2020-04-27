using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaDeAcasaCSNetwork.Domain;
using TemaDeAcasaCSNetwork.Repository;

namespace TemaDeAcasaCSNetwork.Service {
    public class SellerService {
        private ICrudRepository<int, Seller> repo;
        public SellerService(ICrudRepository<int, Seller> repo) {
            this.repo = repo;
        }
        public void add(Seller seller) {
            repo.save(seller);
        }
        public void delete(int id) {
            repo.delete(id);
        }
        public void update(int id, Seller seller) {
            repo.update(id, seller);
        }
        public Seller findOne(int id) {
            return repo.findOne(id);
        }
        public List<Seller> findAll() {
            return repo.findAll().ToList();
        }
        public bool findUser(String username, String password) {
            bool ok = false;
            foreach (Seller s in findAll()) {
                if (s.Username.Equals(username) && s.Password.Equals(password)) {
                    ok = true;
                    break;
                }
            }
            return ok;
        }
    }
}
