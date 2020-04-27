using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaDeAcasaCSNetwork.Domain;
using TemaDeAcasaCSNetwork.Repository;

namespace TemaDeAcasaCSNetwork.Service {
    public class GameService {
        private ICrudRepository<int, Game> repo;
        public GameService(ICrudRepository<int,Game> repo) {
            this.repo = repo;
        }
        public void add(Game game) {
            repo.save(game);
        }
        public void delete(int id) {
            repo.delete(id);
        }
        public void update(int id, Game game) {
            repo.update(id, game);
        }
        public Game findOne(int id) {
            return repo.findOne(id);
        }
        public List<Game> findAll() {
            return repo.findAll().ToList();
        }
        public IEnumerable<Game> sortDescending() {
            IEnumerable<Game> SortedList = findAll().OrderByDescending(g => g.NrOfEmptySeats);
            return SortedList;
        }
    }
}
