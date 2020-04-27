using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaDeAcasaCSNetwork.Domain;
using TemaDeAcasaCSNetwork.Repository;

namespace TemaDeAcasaCSNetwork.Service {
    public class TicketService {
        private ICrudRepository<int, Ticket> repo;
        public TicketService(ICrudRepository<int, Ticket> repo) {
            this.repo = repo;
        }
        public void add(Ticket ticket) {
            repo.save(ticket);
        }
        public void delete(int id) {
            repo.delete(id);
        }
        public void update(int id, Ticket ticket) {
            repo.update(id, ticket);
        }
        public Ticket findOne(int id) {
            return repo.findOne(id);
        }
        public List<Ticket> findAll() {
            return repo.findAll().ToList();
        }
        public int getBiggestId() {
            int max = -999;
            foreach (Ticket t in findAll()) {
                if (t.Id > max) {
                    max = t.Id;
                }
            }
            return max;
        }
    }
}
