using System.Globalization;
using System.Windows.Data;

namespace BasicWPFApp.Presentation;

public abstract class ValueConverter : IValueConverter
{
	public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

	public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return new object();
	}
}
