using Bl2Common;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;

namespace Bl2Client {
	public partial class MainWindow : Window {

		private string Hostname => IPBox.Text;
		private string Port => PortBox.Text;

		private Connection? connection;

		public ObservableCollection<Quest> OpenQuests { get; } = new();
		public ObservableCollection<Quest> AvailableQuests { get; } = new();

		public MainWindow() {
			InitializeComponent();
			DataContext = this;
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

		public void OnNewAvailableQuest(Quest q) {
			connection.Client.Connection.SendData(Constants.PACKET_ID, new Packet(q.ID, QuestStatus.Accepted));
		}

		public void OnQuestTrigger(Quest q) {
			connection.Client.Connection.SendData(Constants.PACKET_ID, new Packet(q.ID, QuestStatus.Triggered));
		}

		public void OnQuestDelete(Quest q) {
			connection.Client.Connection.SendData(Constants.PACKET_ID, new Packet(q.ID, QuestStatus.Hidden));
		}

		private void OnFullSync(Quest[] obj) {
			OpenQuests.Clear();
			AvailableQuests.Clear();

			foreach (Quest quest in obj) {
				if (quest.Status == QuestStatus.NotAccepted) {
					OpenQuests.Add(quest);
				}
				if (quest.Status == QuestStatus.Accepted) {
					AvailableQuests.Add(quest);
				}
			}
		}

		private void OnExit(object sender, RoutedEventArgs e) {
			connection?.Client.Dispose();
			Application.Current.Shutdown();
		}

		protected override void OnClosing(CancelEventArgs e) {
			base.OnClosing(e);
			connection?.Client.Dispose();
		}
	}
}
