using System.Diagnostics;
namespace ToDo_Anwendung_WPF {
	public sealed class Library {
		private Library() {}
		public sealed class Time {
			private Time() {}
			public static long getSystemReferenceClockNanoSeconds() {
				return 10000L * Stopwatch.GetTimestamp() / TimeSpan.TicksPerMillisecond * 100L;
			}
		}
	}
}