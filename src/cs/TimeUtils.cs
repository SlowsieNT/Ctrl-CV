public class TimeUtils {
	public static double GetSec(double aAdd = 0) {
		return aAdd + (Get() / 1000.0);
	}
	public static double Get(double aAdd = 0) {
		return aAdd + (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds);
	}
}
