using System.Windows.Media;

namespace Bl2Client {
	public static class Colors {
		public static readonly SolidColorBrush HAMMERLOCK = new(Color.FromRgb(10, 200, 240));
		public static readonly SolidColorBrush BBOARD = new(Color.FromRgb(100, 100, 100));
		public static readonly SolidColorBrush LILITH = new(Color.FromRgb(230, 0, 130));
		public static readonly SolidColorBrush MORDECAI = new(Color.FromRgb(255, 0, 0));
		public static readonly SolidColorBrush SCOOTER = new(Color.FromRgb(50, 100, 0));
		public static readonly SolidColorBrush TINYTINA = new(Color.FromRgb(220, 160, 0));
		public static readonly SolidColorBrush TANNIS = new(Color.FromRgb(130, 0, 200));
		public static readonly SolidColorBrush BRICK = new(Color.FromRgb(100, 40, 20));
		public static readonly SolidColorBrush ELLIE = new(Color.FromRgb(0, 25, 200));

		public static SolidColorBrush GetColor(string name) {
			return (SolidColorBrush)typeof(Colors).GetField(name.ToUpper())!.GetValue(null)!;
		}
	}
}
