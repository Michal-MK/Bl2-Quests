﻿using System;

namespace Bl2Common {
	/// <summary>
	/// Packet data structure for relaying information to the server
	/// </summary>
	[Serializable]
	public struct Packet {
		/// <summary>
		/// The ID of the quest to modify
		/// </summary>
		public int QuestID { get; set; }

		/// <summary>
		/// The state to switch to
		/// </summary>
		public QuestStatus Status { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public Packet(int questID, QuestStatus status) {
			QuestID = questID;
			Status = status;
		}
	}
}
