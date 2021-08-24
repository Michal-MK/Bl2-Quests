using System.Windows;
using System.Windows.Controls;
using Bl2Common;

namespace Bl2Client {
	public partial class QuestControl : UserControl {

		private MainWindow Win => (Application.Current.MainWindow as MainWindow)!;

		private Quest Ctx => (DataContext as Quest)!;

		public QuestControl() {
			InitializeComponent();
			DataContextChanged += OnDataContextChanged;
		}

		private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
			if (Ctx.Status == QuestStatus.Accepted) {
				TriggerBtn.Visibility = Visibility.Visible;
				AcceptBtn.Visibility = Visibility.Collapsed;
				DeleteBtn.Visibility = Visibility.Collapsed;
			}
			if (Ctx.Status == QuestStatus.NotAccepted) {
				TriggerBtn.Visibility = Visibility.Collapsed;
				AcceptBtn.Visibility = Visibility.Visible;
			}
		}

		private void OnAccept(object sender, RoutedEventArgs e) {
			Ctx.Status = QuestStatus.Accepted;
			Win.AvailableQuests.Add(Ctx);
			Win.OpenQuests.Remove(Ctx);
		}

		private void OnDelete(object sender, RoutedEventArgs e) {
			Ctx.Status = QuestStatus.Hidden;
			Win.OpenQuests.Remove(Ctx);
			Win.AvailableQuests.Remove(Ctx);
		}

		private void OnTrigger(object sender, RoutedEventArgs e) {
			Ctx.Status = QuestStatus.Triggered;
			Win.AvailableQuests.Remove(Ctx);
		}
	}
}
