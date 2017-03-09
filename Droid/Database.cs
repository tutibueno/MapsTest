using System;
using SQLite;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace MapsTest.Droid
{
	public class Database
	{

		public static SQLiteConnection _conn;

		public static string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);


		public Database()
		{
		}

		public static SQLiteConnection conn
		{
			get {

				if (_conn == null)
				{

					string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					path = Path.Combine(path, "db.db");
					try
					{
						_conn = new SQLiteConnection(path);

						//cria a tabela
						_conn.CreateTable<MapPosition>();
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}

				}

				return _conn;

			}
		}

		public static void InsertData<T>(T value)
		{
			try
			{
				conn.Insert(value);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}

		public static List<MapPosition> GetAllPositions()
		{
			return conn.Query<MapPosition>("select * from MapPosition");
		}
	}
}
