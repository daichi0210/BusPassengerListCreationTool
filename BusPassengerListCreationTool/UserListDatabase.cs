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
using DocumentFormat.OpenXml.Office.Word;

namespace BusPassengerListCreationTool
{
    internal class UserListDatabase
    {
        // データベースのファイル名
        private string _databaseFileName = "BusPassengerListCreationTool.db";
        // テーブル名
        private string _tableName = "user_list";
        // データベースファイルへの接続文字列
        private string _connection;

        public UserListDatabase()
        {
            // データベースファイルへの接続文字列を代入
            _connection = "Data Source=" + _databaseFileName + ";Version=3;";
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

        // テーブルがなければ作成する
        public void CreateTable()
        {
            // クエリを作成
            User u = new User();
            string query = "CREATE TABLE IF NOT EXISTS " + _tableName + "(";
            query += "Id INTEGER PRIMARY KEY, ";
            foreach (var v in u.Column.Select((Entry, Index) => new { Entry, Index }))
            {
                query += v.Entry.Key + " " + v.Entry.Value;

                if ((u.Column.Count - 1) - v.Index != 0)
                {
                    query += ", ";
                }
            }
            query += ")";

            // SQL実行
            ExecuteNonQuery(query);
        }

        // データベースに追加する
        public void Insert(User u)
        {
            // クエリを作成
            string query = "INSERT INTO " + _tableName + "(";
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
                PropertyInfo pi = typeof(User).GetProperty(v.Entry.Key);
                object value = pi.GetValue(u);
                query += "'" + value + "'";

                if ((u.Column.Count - 1) - v.Index != 0)
                {
                    query += ", ";
                }
            }
            query += ")";

            // SQL実行
            ExecuteNonQuery(query);
        }

        // データベースを更新する
        public void Update(int targetId, User u)
        {
            // クエリを作成
            string query = "UPDATE " + _tableName + " SET ";
            foreach (var v in u.Column.Select((Entry, Index) => new { Entry, Index }))
            {
                query += v.Entry.Key + " = ";

                PropertyInfo pi = typeof(User).GetProperty(v.Entry.Key);
                object value = pi.GetValue(u);
                query += "'" + value + "'";

                if ((u.Column.Count - 1) - v.Index != 0)
                {
                    query += ", ";
                }
            }
            query += " WHERE ID = " + targetId.ToString();

            // SQL実行
            ExecuteNonQuery(query);
        }

        // データベースから削除する
        public void Delete(int targetId)
        {
            // クエリを作成
            string query = "DELETE FROM " + _tableName + " WHERE ID = " + targetId.ToString();

            // SQL実行
            ExecuteNonQuery(query);
        }

        // DataTableにデータを挿入するクエリを実行する
        public DataTable AdapterFill(string query)
        {
            DataTable dt = new DataTable();

            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(_connection))
            {
                // データベース接続を開く
                connection.Open();

                // DataTableにデータを挿入
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }

                // 接続を閉じる
                connection.Close();
            }

            return dt;
        }

        // データベースのテーブルを読み込む
        public DataTable LoadTable()
        {
            // テーブルがなければ作成する
            CreateTable();

            // クエリを作成
            string query = "SELECT * FROM " + _tableName;

            // SQL実行
            DataTable dt = AdapterFill(query);

            return dt;
        }

        // 指定したIdのデータを取得する
        public DataTable getUserData(int targetId)
        {
            // クエリを作成
            string query = "SELECT * FROM " + _tableName + " WHERE ID = " + targetId.ToString();

            // SQL実行
            DataTable dt = AdapterFill(query);

            return dt;
        }

        //★★★
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
                    cmd.CommandText = "SELECT Id, LastName, FirstName FROM " + _tableName;

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

    }
}
