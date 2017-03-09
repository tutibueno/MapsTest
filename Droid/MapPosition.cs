using System;
using System.Collections.Generic;
using SQLite;

namespace MapsTest.Droid
{
	public class MapPosition
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public MapPosition(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		public MapPosition()
		{
			
		}

	}
}
