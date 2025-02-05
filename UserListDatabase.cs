using System;

using System.Data.SQLite;

public class UserListDatabase
{
	public UserListDatabase()
	{
        // データベースファイルへの接続文字列
        string connectionString = "Data Source=sample.db;Version=3;";

        public void openDB()
        {
            // SQLiteの接続を開く
            using (var connection = new SQLiteConnection(connectionString))
            {
                // データベース接続を開く
                connection.Open();

                // テーブルがなければ作成するSQL
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY, LastName TEXT, FirstName TEXT)";
                using (var cmd = new SQLiteCommand(createTableQuery, connection))
                {
                    // SQL文を実行してテーブルを作成
                    cmd.ExecuteNonQuery();
                }

                // データを取得するSQL
                string selectQuery = "SELECT * FROM Users";

                // データをDataTableに読み込む
                /*
                using (var cmd = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);  // データをDataTableに埋め込む

                        // DataGridViewにデータをバインドする
                        dataGridView1.DataSource = dataTable;
                    }
                }
                */

                // 接続を閉じる
                connection.Close();
            }
        }
        public void addDB()
        {

        }
        public void deleteDB()
        {

        }
        public void editBD()
        {

        }

    }
}
