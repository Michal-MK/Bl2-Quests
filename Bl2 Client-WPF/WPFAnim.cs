using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Bl2Client {
	public static class WPFAnim {
		public static void FadeOut(this FrameworkElement source, int durationMs, bool collapse = true, Action? postAction = null) {
			DoubleAnimation myDoubleAnimation = new() {
				From = 1.0,
				To = 0.0,
				Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
			};

			Storyboard s = new();
			s.Children.Add(myDoubleAnimation);
			Storyboard.SetTarget(myDoubleAnimation, source);
			Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(UIElement.OpacityProperty));
			s.Completed += (_, _) => {
				if (collapse) {
					source.Visibility = Visibility.Collapsed;
				}
				else {
					source.Visibility = Visibility.Hidden;
				}
				postAction?.Invoke();
			};
			s.Begin();
		}

		public static void FadeIn(this FrameworkElement source, int durationMs, Action? postAction = null) {
			DoubleAnimation myDoubleAnimation = new() {
				From = 0.0,
				To = 1.0,
				Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
			};

			Storyboard s = new();
			s.Children.Add(myDoubleAnimation);
			Storyboard.SetTarget(myDoubleAnimation, source);
			Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(UIElement.OpacityProperty));
			s.Completed += (_, _) => postAction?.Invoke();
			if (source.Opacity != 0) {
				source.Opacity = 0;
			}
			if (source.Visibility != Visibility.Visible) {
				source.Visibility = Visibility.Visible;
			}
			s.Begin();
		}
	}
}
