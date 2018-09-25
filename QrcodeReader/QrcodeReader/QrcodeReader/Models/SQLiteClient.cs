using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using SQLite;
using System.Collections.ObjectModel;

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
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<SendHistory> ReadAllData()
        {
            var conn = new SQLiteConnection(_databasePath);

            try
            {
                var query = conn.Table<SendHistory>();

                return query.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }

}
