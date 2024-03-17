using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BasicWPFApp.Presentation;

public class ViewModel : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;
	
	protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged!(this, new PropertyChangedEventArgs(propertyName));
	}
}
