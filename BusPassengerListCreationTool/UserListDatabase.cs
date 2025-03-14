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
        private string _connection = "Data Source=users.db;Version=3;";
        
        // テーブルのカラム
        private string _column =
            "(" +
            "Id INTEGER PRIMARY KEY, " +
            "LastName TEXT, " +         // 姓
            "FirstName TEXT, " +        // 名
            "LastNameKana TEXT, " +     // セイ
            "FirstNameKana TEXT, " +    // メイ
            "Address TEXT, " +          // 住所
            "Tel TEXT, " +              // 固定電話番号
            "MobileNumber TEXT, " +     // 携帯電話番号
            "BusStop TEXT, " +          // バス停
            "Remarks TEXT" +            // 備考
            ")";

        // データベースの情報を読み込む
        public DataTable LoadTable()
        {
            // テーブルがなければ作成するSQL
            //★★★どこに保存されている…？
            ExecuteNonQuery("CREATE TABLE IF NOT EXISTS user_list" + _column);

            //★★★仮のデータを挿入
            //ExecuteNonQuery("INSERT INTO user_list (LastName, FirstName, Tel) VALUES ('last-name', 'first-name', '00-0000-0000')");

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



        //★★★要修正★★★
        public DataTable getUserData(int targetId)
        {
            //ExecuteNonQuery("CREATE TABLE IF NOT EXISTS user_list" + _column);

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




        /*
        public void addDB(string name, string address, string TEL, string busStop, string remarks)
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
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
        */

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

                        /*
                        // データを挿入
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@TEL", TEL);
                        cmd.Parameters.AddWithValue("@busStop", busStop);
                        cmd.Parameters.AddWithValue("@remarks", remarks);
                        cmd.Parameters.AddWithValue("@id", targetId);
                        */

                        //MessageBox.Show(query);
                        // SQL実行
                        cmd.ExecuteNonQuery();
                    }
                }

                // 接続を閉じる
                connection.Close();
            }
        }

        /*
        public void editDB(int targetId, string name, string address, string TEL, string busStop, string remarks)
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
            {
                // データベース接続を開く
                connection.Open();

                // データを取得するSQL
                string selectQuery = "UPDATE Users SET Name = @name, Address = @address, TEL = @TEL, BusStop = @busStop, Remarks = @remarks WHERE ID = @id";
                
                using (var cmd = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        // データを挿入
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@TEL", TEL);
                        cmd.Parameters.AddWithValue("@busStop", busStop);
                        cmd.Parameters.AddWithValue("@remarks", remarks);
                        cmd.Parameters.AddWithValue("@id", targetId);

                        //MessageBox.Show();
                        // SQL実行
                        cmd.ExecuteNonQuery();
                    }
                }

                // 接続を閉じる
                connection.Close();
            }
        }
        */

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


        /*
        public void deleteDB(int targetId)
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
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
        */

        public string[] getDB()
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
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
            using (var connection = new SQLiteConnection(_connection))
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

        public Dictionary<int, string> getUserInfo()
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
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

                // Dictionaryにデータを挿入
                var userInfo = new Dictionary<int, string>();

                // 文字配列にデータを挿入
                //string[] names = new string[0];
                //int count = 0;

                using (var cmd = new SQLiteCommand(connection))
                {
                    // 氏名を取得するSQL
                    cmd.CommandText = "SELECT Id, Name FROM Users";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Array.Resize(ref names, count + 1);
                            //names[count] = reader["name"].ToString();
                            //count++;

                            userInfo.Add(Int32.Parse(reader["Id"].ToString()), reader["Name"].ToString());
                        }
                    }
                }

                // 接続を閉じる
                connection.Close();

                return userInfo;
            }
        }
    }
}
