using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bl2Common;
using Igor.TCP;

namespace Bl2Server {
	/// <summary>
	/// Main class of the program
	/// </summary>
	internal class Borderlands {

		/// <summary>
		/// Reference to the server
		/// </summary>
		public TCPServer server;

		/// <summary>
		/// The structure holding the global state of all interesting quests in the game
		/// </summary>
		public List<Quest> list { get; set; } = new List<Quest>();

		public Borderlands() {
			server = new TCPServer(new ServerConfiguration());
			server.OnClientConnected += Server_OnClientConnected;
			server.OnClientDisconnected += Server_OnClientDisconnected;
			ParseQuests();
			server.Start(Constants.PORT);

			AppDomain.CurrentDomain.ProcessExit += (sender, args) => {
				foreach (TCPClientInfo serverConnectedClient in server.ConnectedClients) {
					server.DisconnectClient(serverConnectedClient.ID);
					server.Stop();
				}
			};
		}

		private void ParseQuests() {
			if (!File.Exists(Program.PATH)) {
				throw new Exception("Quest definition file is missing!");
			}

			string[] lines = File.ReadAllLines(Program.PATH);

			for (int i = 0; i < lines.Length; i++) {
				if (string.IsNullOrWhiteSpace(lines[i]) || lines[i].StartsWith("#")) {
					continue;
				}

				string[] splitLine = lines[i].Split(':');
				Quest q = new Quest {
					ID = int.Parse(splitLine[0].Trim()),
					Name = splitLine[1].Trim(),
					AcceptingLocation = splitLine[2].Trim(),
					GivenBy = splitLine[3].Trim(),
					AvailableSince = int.Parse(splitLine[4].Trim()),
					AcceptedBy = (PlayerCharacter)Enum.Parse(typeof(PlayerCharacter), splitLine[5].Trim()),
					QuestLevel = int.Parse(splitLine[6].Trim())
				};
				list.Add(q);
			}
		}

		private void Server_OnClientConnected(object sender, ClientConnectedEventArgs e) {
			TCPConnection connection = e.Server.GetConnection(e.ClientInfo.ID);
			e.Server.DefineCustomPacket<Packet>(e.ClientInfo.ID, Constants.PACKET_ID, OnPacketReceived);
			connection.SendData(Constants.PROPERTY_SYNC, list.ToArray());
			Console.WriteLine(e.ClientInfo.Name + "(" + e.ClientInfo.ID + ")" + " " + e.ClientInfo.Address);
		}

		private void Server_OnClientDisconnected(object sender, ClientDisconnectedEventArgs e) {
			Console.WriteLine($"{e.ClientInfo.Name} ({e.ClientInfo.Address}) Disconnected");
		}

		private void OnPacketReceived(byte senderID, Packet packet) {
			Quest q = list.First(other => other.ID == packet.QuestID);

			Console.WriteLine($"{q.Name} change {q.Status.ToString()} -> {packet.Status.ToString()}");
			q.Status = packet.Status;
			server.SendToAll(Constants.PROPERTY_SYNC, list.ToArray());
		}

		internal void Restart() {
			list.Clear();
			ParseQuests();
			server.SendToAll(Constants.PROPERTY_SYNC, list.ToArray());
		}
	}
}