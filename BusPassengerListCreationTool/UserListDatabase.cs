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
using System.Reflection;

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
                // データベース接続を開く
                connection.Open();

                // データを挿入するSQL
                //★UPDATEと同じ記述にする
                string query = "INSERT INTO user_list (";
                foreach (var v in u.Column.Select((Entry, Index) => new { Entry, Index }))
                {
                    query += v.Entry.Key;

                    if ((u.Column.Count - 1) - v.Index != 0)
                    {
                        query += ", ";
                    }
                }
                query += ") VALUES (";
                foreach (var v in u.Column.Select((Entry, Index) => new { Entry, Index }))
                {
                    query += "@" + v.Entry.Key;

                    if ((u.Column.Count - 1) - v.Index != 0)
                    {
                        query += ", ";
                    }
                }
                query += ")";

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    // データを挿入
                    foreach (var c in u.Column)
                    {
                        string propertyName = c.Key;
                        PropertyInfo pi = typeof(User).GetProperty(propertyName);
                        object value = pi.GetValue(u);

                        cmd.Parameters.AddWithValue("@" + propertyName, value);
                    }

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
                string query = "UPDATE user_list SET ";
                foreach (var v in u.Column.Select((Entry, Index) => new { Entry, Index }))
                {
                    query += v.Entry.Key + " = @" + v.Entry.Key;

                    if ((u.Column.Count - 1) - v.Index != 0)
                    {
                        query += ", ";
                    }
                }
                query += " WHERE ID = @id";

                MessageBox.Show(query);

                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        // データを挿入
                        foreach (var c in u.Column)
                        {
                            string propertyName = c.Key;
                            PropertyInfo pi = typeof(User).GetProperty(propertyName);
                            object value = pi.GetValue(u);

                            cmd.Parameters.AddWithValue("@" + propertyName, value);
                        }
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
