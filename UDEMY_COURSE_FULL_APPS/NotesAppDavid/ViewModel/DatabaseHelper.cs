using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.ViewModel

    /*
     *With this database helper we are able of creating/inserting/updating/delting any kind of element
     * in any kind of table
     *
     */
{
    public class DatabaseHelper
    {
        //Path combine() will create a valid path...
        public static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        //GenericMethod -- 
        public static bool Insert<T>(T item)
        {
            bool result = false;

            //Automatic handling close connection and so on...
            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>(); //Make sure the table exist
                int numberOfRows = conn.Insert(item); //Returns an int, with the number of rows added
                if (numberOfRows > 0)
                    result = true;
            }

            return result;
        }

        public static bool Update<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                int numberOfRows = conn.Update(item);
                if (numberOfRows > 0)
                    result = true;
            }

            return result;
        }

        public static bool Delete<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                int numberOfRows = conn.Delete(item);
                if (numberOfRows > 0)
                    result = true;
            }

            return result;
        }
    }
}
