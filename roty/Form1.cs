using MySql.Data.MySqlClient;   // MySQL adatbázishoz szükséges könyvtár
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;              // DataTable, DataSet osztályok
using System.Data.SqlClient;    // SQL Server-hez tartozó névtér (itt nem használjuk, de importálva van)
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;     // Windows Forms GUI komponensek
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace roty
{
    public partial class Form1 : Form
    {
        string connectionString = "server=127.0.0.1;uid=root;pwd=;database=szuperhosok;";
        public Form1()
        {
            InitializeComponent();
        }

        public DataTable LoadDataToDataGrid(MySqlConnection conn)
        {
            string query = "SELECT * FROM szuperhosok";            // SQL lekérdezés: összes diák lekérése
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);  // Adapter, ami lefuttatja a lekérdezést
            DataTable dt = new DataTable();                 // Üres táblázat létrehozása a memóriában
            da.Fill(dt);                                   // Az adapter feltölti az adatokat a DataTable-be

            return dt;                                     // Visszatérünk a betöltött adatokkal
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString)) // Kapcsolat létrehozása
                {
                    conn.Open(); 
                    dataGridView1.DataSource = LoadDataToDataGrid(conn);
                }
            }
            catch (Exception ex)
            {
                // Ha hiba történik az adatbázis kapcsolat során, megjelenítjük az üzenetet
                MessageBox.Show("Hiba az adatbázis elérésénél: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open(); // Kapcsolat megnyitása

                    // SQL beszúró lekérdezés – új diák hozzáadása
                    string query = "INSERT INTO `szuperhosok`(`szuperhos_nev`, `valodi_nev`, `kiado`, `szekhely`, `elso_megjelenes`) VALUES (@nev, @valonev,@kiado,@szekhely,@megjel)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Paraméterek átadása az SQL lekérdezésnek
                        command.Parameters.AddWithValue("@nev", textBox1.Text);
                        command.Parameters.AddWithValue("@valonev", textBox2.Text);
                        command.Parameters.AddWithValue("@kiado", textBox3.Text);
                        command.Parameters.AddWithValue("@szekhely", textBox4.Text);
                        command.Parameters.AddWithValue("@megjel", Convert.ToInt32(textBox5.Text));

                        // Lekérdezés végrehajtása
                        command.ExecuteNonQuery();

                        // Frissítjük a DataGridView tartalmát az új adattal
                        dataGridView1.DataSource = LoadDataToDataGrid(connection);
                    }
                }
            }
            catch
            {
                // Ha a százalék nem 0–100 között van, hibát jelezünk
                MessageBox.Show("Nem jól adtad meg a megjelenést");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open(); // Kapcsolat megnyitása

                    // SQL beszúró lekérdezés – új diák hozzáadása
                    string query1 = "UPDATE `szuperhosok` SET `szuperhos_nev`=@nev,`valodi_nev`=@valonev,`kiado`=@kiado,`szekhely`=@szekhely,`elso_megjelenes`=@megjel WHERE `id` LIKE @id";

                    using (MySqlCommand command = new MySqlCommand(query1, connection))
                    {
                        // Paraméterek átadása az SQL lekérdezésnek
                        command.Parameters.AddWithValue("@nev", textBox1.Text);
                        command.Parameters.AddWithValue("@valonev", textBox2.Text);
                        command.Parameters.AddWithValue("@kiado", textBox3.Text);
                        command.Parameters.AddWithValue("@szekhely", textBox4.Text);
                        command.Parameters.AddWithValue("@megjel", Convert.ToInt32(textBox5.Text));
                        command.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView1.CurrentCell.Value));

                        // Lekérdezés végrehajtása
                        command.ExecuteNonQuery();

                        // Frissítjük a DataGridView tartalmát az új adattal
                        dataGridView1.DataSource = LoadDataToDataGrid(connection);
                    }
                }
            }
            catch
            {
                // Ha a százalék nem 0–100 között van, hibát jelezünk
                MessageBox.Show("Nem jól adtad meg a megjelenést");
            }
        }
    }
}
