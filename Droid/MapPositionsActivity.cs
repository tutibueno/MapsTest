﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MapsTest.Droid
{
	[Activity(Label = "Posições Salvas no Mapa")]
	public class MapPositionsActivity : Activity
	{

		ListView listViewItems;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here

			SetContentView(Resource.Layout.MapLocationsLayout);

			listViewItems = FindViewById<ListView>(Resource.Id.listView1);

			MapPositionAdapter.OnDeleteItem += RefreshListItems;

		}

		protected override void OnResume()
		{
			base.OnResume();

			RefreshListItems();

		}

		public void RefreshListItems()
		{
			var positions = Database.GetAllPositions();
			MapPositionAdapter adapter = new MapPositionAdapter(positions.ToArray(), this);
			listViewItems.Adapter = adapter;
		}
	}
}
