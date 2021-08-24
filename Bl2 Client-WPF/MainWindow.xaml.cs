using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using Bl2Common;

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
			Application.Current.Shutdown();
		}
	}
}
