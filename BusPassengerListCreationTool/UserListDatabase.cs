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

namespace BusPassengerListCreationTool
{
    internal class UserListDatabase
    {
        // データベースファイルへの接続文字列
        string connectionString = "Data Source=users.db;Version=3;";

        //public DataTable loadDB()
        public DataTable loadDB()
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(connectionString))
            {
                // データベース接続を開く
                connection.Open();

                // テーブルがなければ作成するSQL
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY, Name TEXT, Address TEXT, TEL TEXT, BusStop TEXT, Remarks TEXT)";
                using (var cmd = new SQLiteCommand(createTableQuery, connection))
                {
                    // SQL文を実行してテーブルを作成
                    cmd.ExecuteNonQuery();
                }

                // データを取得するSQL
                string selectQuery = "SELECT * FROM Users";

                // データテーブルにデータを挿入
                DataTable dataTable = new DataTable();
                using (var cmd = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        // データをDataTableに埋め込む
                        adapter.Fill(dataTable);  
                    }
                }

                // 接続を閉じる
                connection.Close();

                return dataTable;
            }
        }
        public void addDB(string name, string address, string TEL, string busStop, string remarks)
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open(); // データベース接続を開く

                // データを挿入するSQL
                string insertQuery = "INSERT INTO Users (Name, Address, TEL, BusStop, Remarks) VALUES (@name, @address, @TEL, @busStop, @remarks)";
                using (var cmd = new SQLiteCommand(insertQuery, connection))
                {
                    // データを挿入
                    cmd.Parameters.AddWithValue("@name", name); 
                    cmd.Parameters.AddWithValue("@address", address); 
                    cmd.Parameters.AddWithValue("@TEL", TEL); 
                    cmd.Parameters.AddWithValue("@busStop", busStop); 
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    // SQL実行
                    cmd.ExecuteNonQuery();
                }

                // 接続を閉じる
                connection.Close();
            }
        }
        public void editDB()
        {

        }

        public void deleteDB(int targetId)
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(connectionString))
            {
                // データベース接続を開く
                connection.Open();

                // データを取得するSQL
                string selectQuery = "DELETE FROM Users WHERE ID = @id";

                using (var cmd = new SQLiteCommand(selectQuery, connection))
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

        public string[] getDB()
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(connectionString))
            {
                // データベース接続を開く
                connection.Open();

                // データを取得するSQL
                string selectQuery = "SELECT * FROM Users";

                // データテーブルにデータを挿入
                DataTable dataTable = new DataTable();
                using (var cmd = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        // データをDataTableに埋め込む
                        adapter.Fill(dataTable);
                    }
                }

                // 接続を閉じる
                connection.Close();

                //
                string[] userData = new string[0];
                int count = 0;

                // データを一行ずつ取り出す
                foreach (DataRow dr in dataTable.Rows)
                {
                    string rowData = "";
                    rowData +=
                        dr["Name"].ToString() + "," +
                        dr["Address"].ToString() + "," +
                        dr["TEL"].ToString() + "," +
                        dr["BusStop"].ToString() + "," +
                        dr["Remarks"].ToString();

                    Array.Resize(ref userData, count + 1);
                    userData[count] = rowData;
                    count++;
                }

                return userData;
            }
        }




        public string[] loadName()
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(connectionString))
            {
                // データベース接続を開く
                connection.Open();

                // テーブルがなければ作成するSQL
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY, Name TEXT, Address TEXT, TEL TEXT, BusStop TEXT, Remarks TEXT)";
                using (var cmd = new SQLiteCommand(createTableQuery, connection))
                {
                    // SQL文を実行してテーブルを作成
                    cmd.ExecuteNonQuery();
                }

                // 文字配列にデータを挿入
                string[] names = new string[0];
                int count = 0;

                using (var cmd = new SQLiteCommand(connection))
                {
                    // 氏名を取得するSQL
                    cmd.CommandText = "SELECT Name FROM Users";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Array.Resize(ref names, count + 1);
                            names[count] = reader["name"].ToString();
                            count++;
                        }
                    }
                }

                // 接続を閉じる
                connection.Close();

                return names;
            }
        }
    }
}
