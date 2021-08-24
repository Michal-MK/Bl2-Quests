namespace Bl2Common {
	/// <summary>
	/// Various constants used throughout the application
	/// </summary>
	public static class Constants {

		/// <summary>
		/// Packet ID for synchronizing the global quest container
		/// </summary>
		public const byte PROPERTY_SYNC = 6;

		/// <summary>
		/// Packet ID for relaying changes from connected clients about changes to the global quest container
		/// </summary>
		public const byte PACKET_ID = 7;

		/// <summary>
		/// The port this server is running on
		/// </summary>
		public const ushort PORT = 4245;
	}
}
