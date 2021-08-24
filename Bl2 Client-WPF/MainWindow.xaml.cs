using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Bl2Common;
using Igor.TCP;

namespace Bl2Client {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		private string Hostname => IPBox.Text;
		private string Port => PortBox.Text;

		private Connection? connection;

		public MainWindow() {
			InitializeComponent();
		}

		private async void OnConnect(object sender, RoutedEventArgs e) {

			if (!IPAddress.TryParse(Hostname, out IPAddress? address)) {
				// TODO Log
				return;
			}
			if (!ushort.TryParse(Port, out ushort port)) {
				// TODO Log
				return;
			}

			connection = await Connection.Init(address, port);

			if (connection == null) {
				// TODO Log
				return;
			}

			connection.FullSync += OnFullSync;

			LoginGrid.FadeOut(200, postAction: () => MainGrid.FadeIn(400));
		}

		private void OnFullSync(Quest[] obj) {
			AvailableSource.ItemsSource = obj;
		}

		private void OnExit(object sender, RoutedEventArgs e) {
			Application.Current.Shutdown();
		}
	}
}
