using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using SQLite;
using SQLitePCL;

namespace QrcodeReader.Models
{
    public class SQLiteClient
    {
        private readonly string _mainDir = FileSystem.AppDataDirectory;
        private readonly string _sqlFile = "MyData.db";
        private readonly string _databasePath;

        public SQLiteClient()
        {
            // Get an absolute path to the database file
            _databasePath = Path.Combine(_mainDir, _sqlFile);

            var db = new SQLiteConnection(_databasePath);
            db.CreateTable<SendHistory>();
        }


        public void AddData(SendHistory history)
        {
            var conn = new SQLiteConnection(_databasePath);

            try
            {
                conn.Insert(history);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void ReadAllData()
        {
            var conn = new SQLiteConnection(_databasePath);
            var query = conn.Table<SendHistory>().Where(v => v.SentDate >= DateTimeOffset.Now.AddDays(-7));

            var result = query.ToList();

            foreach (var stock in result)
                Debug.WriteLine("Data ID: " + stock.DataId);
        }
    }

    public class SendHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int DataId { get; set; }
        public DateTimeOffset SentDate { get; set; }
        public int IsSent { get; set; }
    }
    
}
