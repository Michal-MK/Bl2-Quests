using System;
using Igor.TCP;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Bl2Common;

namespace Bl2Client {
	public class Connection {

		public TCPClient Client { get; }

		public event Action<Quest[]>? FullSync;

		private readonly Dispatcher ui;

		private Connection(TCPClient tcpClient, Dispatcher dispatcher) {
			Client = tcpClient;
			ui = dispatcher;
		}


		public static async Task<Connection?> Init(IPAddress host, ushort port) {
			Connection ret = new(new TCPClient(host, port), Application.Current.MainWindow.Dispatcher);

			bool success = await ret.Client.ConnectAsync(2000);
			if (success) {
				ret.Client.DefineCustomPacket<Quest[]>(Constants.PROPERTY_SYNC, ret.OnFullSync);
				return ret;
			}
			return null;
		}

		private void OnFullSync(byte sender, Quest[] data) {
			ui.Invoke(() => FullSync?.Invoke(data));
		}
	}
}
