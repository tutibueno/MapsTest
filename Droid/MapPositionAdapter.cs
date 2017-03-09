
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MapsTest.Droid
{
	public class MapPositionAdapter : BaseAdapter<MapPosition>
	{

		MapPosition[] data;

		Activity context;


		public MapPositionAdapter(MapPosition[] data, Activity context)
		{
			this.data = data;
			this.context = context;
		}


		public override MapPosition this[int position]
		{
			get
			{
				return data[position];
			}
		}

		public override int Count
		{
			get
			{
				return data.Length;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var inflater = LayoutInflater.From(parent.Context);
			var view = inflater.Inflate(Resource.Layout.MapItem, parent, false);

			var txtvPosition = view.FindViewById<TextView>(Resource.Id.textPosition);


			txtvPosition.Text = "Posição: " + data[position].Latitude + " / " + data[position].Longitude;


			var button = view.FindViewById<Button>(Resource.Id.buttonOpenInMap);
			button.Click += delegate
			{

				//Voolta para MainActivity

				MainActivity.SetLocation(data[position].Latitude, data[position].Longitude);

				var i = new Intent(context, typeof(MainActivity));
				i.SetFlags(ActivityFlags.ReorderToFront);
				context.StartActivity(i);



			};

			return view;
		}
	}
}
