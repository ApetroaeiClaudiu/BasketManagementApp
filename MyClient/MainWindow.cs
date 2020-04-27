using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemaDeAcasaCSNetwork.Domain;

namespace MyClient {
    public partial class MainWindow : Form {
        private readonly Client ctrl;


        public MainWindow(Client ctrl) {
            InitializeComponent();
            this.ctrl = ctrl;
            refreshData();
            ctrl.updateEvent += userUpdate;
        }
        private void refreshData() {
            gameData.Rows.Clear();
            var ordered = ctrl.GetGames();
            int i = 0;
            gameData.ColumnCount = 8;
            gameData.Columns[0].Name = "ID";
            gameData.Columns[1].Name = "HOME";
            gameData.Columns[2].Name = "AWAY";
            gameData.Columns[3].Name = "TYPE";
            gameData.Columns[4].Name = "TOTAL";
            gameData.Columns[5].Name = "EMPTY";
            gameData.Columns[6].Name = "PRICE";
            gameData.Columns[7].Name = "AVAIL";
            foreach (Game game in ordered) {
                string avaibility = "AVAILABLE";
                if (game.NrOfEmptySeats == 0) {
                    avaibility = "SOLD OUT";
                }
                gameData.Rows.Add(game.Id.ToString(), game.HomeTeam, game.AwayTeam, game.Type.ToString(), game.TotalNrOfSeats.ToString(), game.NrOfEmptySeats.ToString(), game.Price.ToString(), avaibility);
                i++;
            }
            foreach (DataGridViewRow row in gameData.Rows)
                if (row.Cells[7].Value == "SOLD OUT") {
                    row.Cells[7].Style.BackColor = Color.Red;
                } else if (row.Cells[7].Value == "AVAILABLE") {
                    row.Cells[7].Style.BackColor = Color.Green;
                }
            gameData.Refresh();
        }
        public void userUpdate(object sender, UserEventArgs e) {
            if (e.UserEventType == UserEvent.Refresh) {
                update((Game)e.Data);
            }
        }

        public void update(Game game) {
            for(int i = 0; i < gameData.RowCount; i++) {
                int id = Convert.ToInt32(gameData.Rows[i].Cells[0].Value);
                if(id == game.Id) {
                    gameData.Rows[i].Cells[5].Value = game.NrOfEmptySeats;
                    string avaibility = "AVAILABLE";
                    if (game.NrOfEmptySeats == 0) {
                        avaibility = "SOLD OUT";
                    }
                    gameData.Rows[i].Cells[7].Value = avaibility;
                    if (gameData.Rows[i].Cells[7].Value == "SOLD OUT") {
                        gameData.Rows[i].Cells[7].Style.BackColor = Color.Red;
                    } else if (gameData.Rows[i].Cells[7].Value == "AVAILABLE") {
                        gameData.Rows[i].Cells[7].Style.BackColor = Color.Green;
                    }
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e) {

        }

        private void sellTicketButton_Click(object sender, EventArgs e) {
            try {
                string client = nameBox.Text;
                int nr = int.Parse(seatsBox.Text);
                int idgame = Convert.ToInt32(gameData.Rows[gameData.SelectedCells[0].RowIndex].Cells[0].Value);
                ctrl.sellTicket(client, nr, idgame);
                MessageBox.Show("Ticket Sold !");
                //refreshData();
            }catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
