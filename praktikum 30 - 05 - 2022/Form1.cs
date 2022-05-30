using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace praktikum_30___05___2022
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        MySqlConnection sqlConnect = new MySqlConnection("server=localhost;uid=root;pwd=;database=premier_league");
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlAdapter;
        String sqlQuery;
        DataTable dtpremiere_league = new DataTable();
        int PosisiSekarang = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlQuery = "SELECT t.team_id as `ID Tim`, t.team_name as `Nama Tim`, CONCAT(m.manager_name, ' ', '(', n.nation, ')'), CONCAT(home_stadium, ',', ' ', city, ' ', '(', capacity, ')') FROM team t, manager m, player p, nationality n WHERE t.manager_id = m.manager_id AND m.nationality_id = n.nationality_id";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtpremiere_league);
            
            //sqlQuery = "select concat(p.player_name, " ", SUM(if(d.`type` = "GO" or d.type = "GP", 1, 0)), '(', SUM(if(d.`type` = "GP", 1, 0)), ')') as "Top Scorer" from player p, dmatch d where p.player_id = d.player_id group by player_name order by 1";
            //sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            //sqlAdapter = new MySqlDataAdapter(sqlCommand);
            //sqlAdapter.Fill(dtpremiere_league);

            IsiDataPemain(0);
        }

        public void IsiDataPemain(int Posisi)
        {

            Lbl_Output_TeamName.Text = dtpremiere_league.Rows[Posisi][0].ToString();
            Lbl_Output_Manager.Text = dtpremiere_league.Rows[Posisi][1].ToString();
            Lbl_Output_Stadium.Text = dtpremiere_league.Rows[Posisi][2].ToString();
            Lbl_Output_TopScorer.Text = dtpremiere_league.Rows[Posisi][0].ToString();
        }

        private void Btn_First_Click(object sender, EventArgs e)
        {
            IsiDataPemain(0);
        }

        private void Btn_Prev_Click(object sender, EventArgs e)
        {
            if (PosisiSekarang > 0)
            {
                PosisiSekarang--;
                IsiDataPemain(PosisiSekarang);
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            if (PosisiSekarang < dtpremiere_league.Rows.Count - 1)
            {
                PosisiSekarang++;
                IsiDataPemain(PosisiSekarang);
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }

        private void Btn_Last_Click(object sender, EventArgs e)
        {
            IsiDataPemain(dtpremiere_league.Rows.Count - 1);
        }
    }

}
