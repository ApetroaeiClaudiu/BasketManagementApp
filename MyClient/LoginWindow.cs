using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyClient {
    public partial class LoginWindow : Form {
        private Client ctrl;
        public LoginWindow(Client ctrl) {
            InitializeComponent();
            this.ctrl = ctrl;
        }

        private void login_Click(object sender, EventArgs e) {
            string username = usernameBox.Text;
            string password = passwordBox.Text;
            try {
                Console.WriteLine("Apeleze ctrl login");
                ctrl.login(username, password);
                MainWindow main = new MainWindow(ctrl);
                main.Text = "Main Window for " + username;
                main.Show();
                this.Hide();
            }catch(Exception ex) {
                MessageBox.Show(this, "Login Error" + ex.Message, "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoginWindow_Load(object sender, EventArgs e) {

        }
    }
}
