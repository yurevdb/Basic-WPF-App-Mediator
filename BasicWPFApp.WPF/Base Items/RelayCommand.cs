using System.Windows.Input;

namespace BasicWPFApp.WPF;

public class RelayCommand<T> : ICommand
{
	private Action? _execute;
	private Action<T>? _execute2;
	private Predicate<T>? _canExecute = null;

	public event EventHandler? CanExecuteChanged;

	public RelayCommand(Action action)
	{
		_execute = action;
	}
	public RelayCommand(Action<T> action)
	{
		_execute2 = action;
	}
	public RelayCommand(Action action, Predicate<T> predicate)
	{
		_execute = action;
		_canExecute = predicate;
	}
	public RelayCommand(Action<T> action, Predicate<T> predicate)
	{
		_execute2 = action;
		_canExecute = predicate;
	}

	public bool CanExecute(object? parameter)
	{
		return parameter == null || _canExecute == null || _canExecute((T)parameter);
	}

	public void Execute(object? parameter)
	{
		if (parameter == null && _execute != null)
			_execute();
		else if (parameter != null && _execute2 != null)
			_execute2((T)parameter);
	}
}
