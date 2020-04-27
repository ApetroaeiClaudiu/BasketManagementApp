using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaDeAcasaCSNetwork.Repository {
    public interface ICrudRepository<ID, E> {
        E findOne(ID id);
        //E this[ID id] { get; set;}
        IEnumerable<E> findAll();
        void save(E entity);
        void delete(ID id);
        void update(ID id, E entity);
    }
}
