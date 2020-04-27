using Interfaces;
using MyNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyClient {
    static class StartClient {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IServer server = new ServerProxy("127.0.0.1", 55560);
            Client ctrl = new Client(server);
            LoginWindow form = new LoginWindow(ctrl);
            Application.Run(form);
        }
    }
}
