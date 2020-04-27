using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace TemaDeAcasaCSNetwork.Repository {
    public static class DBUtils {

        private static SQLiteConnection instance = null;

        public static SQLiteConnection getConnection() {
            if (instance == null || instance.State == System.Data.ConnectionState.Closed) {
                instance = getNewConnection();
                instance.Open();
            }
            return instance;
        }

        private static SQLiteConnection getNewConnection() {
            string co = @"URI=file:D:\A-FACULTATE\Anu 2\Sem2\Medii De Proiectare Si Programare\BazeDeDate\spersamearga.db";
            //string co = @"URI=file:D:\spersamearga.db";
            return new SQLiteConnection(co);
        }
    }
}
