using BasicWPFApp.Core;

namespace BasicWPFApp.Persistence;

internal static class MemCache
{
	public static IList<Order> Orders { get; } = new List<Order>();
}
