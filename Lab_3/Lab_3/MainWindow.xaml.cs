using System;
using System.Data;
using System.Windows;
using System.Data.SQLite;

namespace Lab_3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateMonitorsTable();
            LoadMonitors();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
            {
                conn.Open();
                string sql = "INSERT INTO monitors(brand, model, size, resolution, refresh_rate) VALUES(@brand, @model, @size, @resolution, @refresh_rate)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@brand", brand.Text);
                    cmd.Parameters.AddWithValue("@model", model.Text);
                    cmd.Parameters.AddWithValue("@size", int.Parse(size.Text));
                    cmd.Parameters.AddWithValue("@resolution", resolution.Text);
                    cmd.Parameters.AddWithValue("@refresh_rate", int.Parse(refreshRate.Text));

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            LoadMonitors();

            // Очистка полей ввода
            brand.Text = "";
            model.Text = "";
            size.Text = "";
            resolution.Text = "";
            refreshRate.Text = "";
        }

        private void LoadMonitors()
        {
            DataTable dt = new DataTable();

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
            {
                conn.Open();
                string sql = "SELECT * FROM monitors";

                using (SQLiteCommand sqcmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader reader = sqcmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }

                conn.Close();
            }

            DataGrid.ItemsSource = dt.DefaultView;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(id.Text, out int idValue))
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
                {
                    conn.Open();
                    string sql = "UPDATE monitors SET brand = @brand, model = @model, size = @size, resolution = @resolution, refresh_rate = @refresh_rate WHERE id = @id";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@brand", brand.Text);
                        cmd.Parameters.AddWithValue("@model", model.Text);
                        cmd.Parameters.AddWithValue("@size", int.Parse(size.Text));
                        cmd.Parameters.AddWithValue("@resolution", resolution.Text);
                        cmd.Parameters.AddWithValue("@refresh_rate", int.Parse(refreshRate.Text));
                        cmd.Parameters.AddWithValue("@id", idValue);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadMonitors();
            }
            else
            {
                MessageBox.Show("ID must be a valid number.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(id.Text, out int idValue))
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
                {
                    conn.Open();
                    string sql = "DELETE FROM monitors WHERE id = @id";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idValue);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadMonitors();
            }
            else
            {
                MessageBox.Show("ID must be a valid number.");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CreateMonitorsTable()
        {
            string cs = "Data Source=mybd.db";

            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS monitors(
                                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                brand TEXT, 
                                model TEXT, 
                                size INTEGER, 
                                resolution TEXT, 
                                refresh_rate INTEGER)";
            cmd.ExecuteNonQuery();
        }

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(id.Text, out int idValue)) // Проверяем, является ли ID числом
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=mybd.db;Version=3;"))
                {
                    conn.Open();
                    string sql = "SELECT * FROM monitors WHERE id = @id";

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idValue);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Если запись найдена
                            {
                                // Заполняем поля данными из базы
                                brand.Text = reader["brand"].ToString();
                                model.Text = reader["model"].ToString();
                                size.Text = reader["size"].ToString();
                                resolution.Text = reader["resolution"].ToString();
                                refreshRate.Text = reader["refresh_rate"].ToString();
                            }
                            else // Если запись не найдена
                            {
                                MessageBox.Show("Запись не найдена.");
                            }
                        }
                    }

                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("ID должен быть числом.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //view tabl
            CreateMonitorsTable();
            LoadMonitors();
        }
    }
}
