using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using System;
using System.Collections.Generic;
using Android.Content;

namespace MapsTest.Droid
{
	[Activity(Label = "MapsTest", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity, IOnMapReadyCallback
	{
		int mapTypeIndex = 0; //mapa normal

		static GoogleMap googleMap;

		public static void SetLocation(double latitude, double longitude)
		{
			//googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new Android.Gms.Maps.Model.LatLng(latitude, longitude), 14.0f));

			googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(new Android.Gms.Maps.Model.LatLng(latitude, longitude), 14.0f));
		}

		public void OnMapReady(GoogleMap map)
		{
			googleMap = map;

			map.MapType = GoogleMap.MapTypeNormal;
			map.MyLocationEnabled = true;
			map.TrafficEnabled = true;
			map.SetIndoorEnabled(true);
			map.BuildingsEnabled = true;
			map.UiSettings.ZoomControlsEnabled = true;
			map.UiSettings.CompassEnabled = true;

			map.MapClick += OnMapClick;

		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += ChangeMapType;


			//Bota que abre os items salvos
			Button buttonItems = FindViewById<Button>(Resource.Id.buttonOpenItems);

			buttonItems.Click += delegate {



				var i = new Intent(this, typeof(MapPositionsActivity));
				i.SetFlags(ActivityFlags.ReorderToFront);
				StartActivity(i);


			};


			MapFragment mapFrag = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);

			mapFrag.GetMapAsync(this);

		}

		void ChangeMapType(object sender, EventArgs e)
		{
			mapTypeIndex++;

			if (mapTypeIndex > 2)
				mapTypeIndex = 0;

			switch (mapTypeIndex)
			{
				case 0:
					googleMap.MapType = GoogleMap.MapTypeNormal;
					break;
				case 1:
					googleMap.MapType = GoogleMap.MapTypeSatellite;
					break;
				case 2:
					googleMap.MapType = GoogleMap.MapTypeHybrid;
					break;
				default:
					break;
			}

		}

		void OnMapClick(object sender, GoogleMap.MapClickEventArgs e)
		{
			//Mensagem toast
			Toast.MakeText(this, "Nova Posição Salva: " + e.Point.Latitude.ToString() + ", " 
			               + e.Point.Longitude.ToString(), ToastLength.Short).Show();

			//Atualiza a label com a última posição salva
			TextView text =  FindViewById<TextView>(Resource.Id.textView1);

			text.Text = "Última Posição Salva: " + e.Point.Latitude.ToString() + ", "  + e.Point.Longitude.ToString();

			//Salva na base
			MapPosition pos = new MapPosition(e.Point.Latitude, e.Point.Longitude);

			Database.InsertData<MapPosition>(pos);

			text.Text = "Última Posição Salva: " + e.Point.Latitude.ToString() + ", " + e.Point.Longitude.ToString();



		}

	}


}

