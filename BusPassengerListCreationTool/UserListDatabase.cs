using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using System.Data;
using System.Net;
using System.Xml.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using System.ComponentModel;
using static System.Data.Entity.Infrastructure.Design.Executor;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace BusPassengerListCreationTool
{
    internal class UserListDatabase
    {
        // データベースファイルへの接続文字列
        private string _connection = "Data Source=BusPassengerListCreationTool.db;Version=3;";

        // データベースの情報を読み込む
        public DataTable LoadTable()
        {
            // テーブルがなければ作成する
            CreateTable();

            //// テーブルのデータを取得
            DataTable dataTable = new DataTable();

            // SQL文
            string query = "SELECT * FROM user_list";
            
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
            {
                // データベース接続を開く
                connection.Open();

                //// DataTableにデータを挿入
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        // DataTableにデータを埋め込む
                        adapter.Fill(dataTable);  
                    }
                }

                // 接続を閉じる
                connection.Close();
            }

            return dataTable;
        }

        // データベースに追加する
        public void Insert(User u)
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
            {
                connection.Open(); // データベース接続を開く

                // データを挿入するSQL
                string insertQuery = "INSERT INTO user_list (LastName, FirstName, LastNameKana, FirstNameKana, Address, Tel, MobileNumber, BusStop, Remarks) VALUES (@LastName, @FirstName, @LastNameKana, @FirstNameKana, @Address, @Tel, @MobileNumber, @BusStop, @Remarks)";
                using (var cmd = new SQLiteCommand(insertQuery, connection))
                {
                    //★もう少しマートにしたい foreach などで。
                    // データを挿入
                    cmd.Parameters.AddWithValue("@LastName", u.LastName);
                    cmd.Parameters.AddWithValue("@FirstName", u.FirstName);
                    cmd.Parameters.AddWithValue("@LastNameKana", u.LastNameKana);
                    cmd.Parameters.AddWithValue("@FirstNameKana", u.FirstNameKana);
                    cmd.Parameters.AddWithValue("@Address", u.Address);
                    cmd.Parameters.AddWithValue("@Tel", u.Tel);
                    cmd.Parameters.AddWithValue("@MobileNumber", u.MobileNumber);
                    cmd.Parameters.AddWithValue("@BusStop", u.BusStop);
                    cmd.Parameters.AddWithValue("@Remarks", u.Remarks);

                    // SQL実行
                    cmd.ExecuteNonQuery();
                }

                // 接続を閉じる
                connection.Close();
            }
        }

        public void Update(int targetId, User u)
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
            {
                // データベース接続を開く
                connection.Open();

                // データを取得するSQL
                string query =
                    "UPDATE user_list SET " +
                    "LastName = @LastName, " +
                    "FirstName = @FirstName, " +
                    "LastNameKana = @LastNameKana, " +
                    "FirstNameKana = @FirstNameKana, " +
                    "Address = @Address, " +
                    "Tel = @Tel, " +
                    "MobileNumber = @MobileNumber, " +
                    "BusStop = @BusStop, " +
                    "Remarks = @Remarks " +
                    "WHERE ID = @id";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        //★もう少しマートにしたい foreach などで。
                        // データを挿入
                        cmd.Parameters.AddWithValue("@LastName", u.LastName);
                        cmd.Parameters.AddWithValue("@FirstName", u.FirstName);
                        cmd.Parameters.AddWithValue("@LastNameKana", u.LastNameKana);
                        cmd.Parameters.AddWithValue("@FirstNameKana", u.FirstNameKana);
                        cmd.Parameters.AddWithValue("@Address", u.Address);
                        cmd.Parameters.AddWithValue("@Tel", u.Tel);
                        cmd.Parameters.AddWithValue("@MobileNumber", u.MobileNumber);
                        cmd.Parameters.AddWithValue("@BusStop", u.BusStop);
                        cmd.Parameters.AddWithValue("@Remarks", u.Remarks);
                        cmd.Parameters.AddWithValue("@id", targetId);

                        // SQL実行
                        cmd.ExecuteNonQuery();
                    }
                }

                // 接続を閉じる
                connection.Close();
            }
        }

        public void Delete(int targetId)
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
            {
                // データベース接続を開く
                connection.Open();

                // データを取得するSQL
                string query = "DELETE FROM user_list WHERE ID = @id";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        // データを挿入
                        cmd.Parameters.AddWithValue("@id", targetId);

                        // SQL実行
                        cmd.ExecuteNonQuery();
                    }
                }

                // 接続を閉じる
                connection.Close();
            }
        }

        //★★★要修正★★★
        public DataTable getUserData(int targetId)
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
            {
                // データベース接続を開く
                connection.Open();

                // データを取得するSQL
                string query = "SELECT * FROM user_list WHERE ID = @id";

                // データテーブルにデータを挿入
                DataTable dt = new DataTable();
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        // データを挿入
                        cmd.Parameters.AddWithValue("@id", targetId);

                        // SQL実行
                        cmd.ExecuteNonQuery();

                        // データをDataTableに埋め込む
                        adapter.Fill(dt);
                    }
                }

                // 接続を閉じる
                connection.Close();

                return dt;
            }
        }

        // 結果を返さないクエリを実行する
        public void ExecuteNonQuery(string query)
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
            {
                // データベース接続を開く
                connection.Open();

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    // SQL文を実行
                    cmd.ExecuteNonQuery();
                }

                // データベース接続を閉じる
                connection.Close();
            }
        }

        public Dictionary<int, string> getUserInfo()
        {
            // テーブルがなければ作成する
            CreateTable();

            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
            {
                // データベース接続を開く
                connection.Open();

                // Dictionaryにデータを挿入
                var userInfo = new Dictionary<int, string>();

                using (var cmd = new SQLiteCommand(connection))
                {
                    // 氏名を取得するSQL
                    cmd.CommandText = "SELECT Id, LastName, FirstName FROM user_list";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userInfo.Add(Int32.Parse(reader["Id"].ToString()), reader["LastName"].ToString() + " " + reader["FirstName"].ToString());
                        }
                    }
                }

                // 接続を閉じる
                connection.Close();

                return userInfo;
            }
        }

        // テーブルがなければ作成する
        public void CreateTable()
        {
            User u = new User();

            string query = "Id INTEGER PRIMARY KEY, ";
            foreach (var v in u.Column.Select((Entry, Index) => new {Entry, Index}))
            {
                query += v.Entry.Key + " " + v.Entry.Value;

                if ((u.Column.Count - 1) - v.Index != 0)
                {
                    query += ", ";
                }
            }

            ExecuteNonQuery("CREATE TABLE IF NOT EXISTS user_list (" + query + ")");
        }
    }
}
