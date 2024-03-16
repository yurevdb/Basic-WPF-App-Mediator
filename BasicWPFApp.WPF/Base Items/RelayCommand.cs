using System.Windows.Input;

namespace BasicWPFApp.WPF;

public class RelayCommand : ICommand
{
	private Action? _execute;
	private Func<object>? _func;

	public event EventHandler? CanExecuteChanged;

	public RelayCommand(Action action)
	{
		_execute = action;
	}
	public RelayCommand(Func<object> action)
	{
		_func = action;
	}

	public bool CanExecute(object? parameter)
	{
		return true;
	}

	public void Execute(object? parameter)
	{
		if (_execute != null)
			_execute();
		else if (_func != null)
			_func();
	}
}

public class RelayCommand<T> : ICommand
{
	private Action<T>? _execute;
	private Func<T, object>? _func;

	public event EventHandler? CanExecuteChanged;

	public RelayCommand(Action<T> action)
	{
		_execute = action;
	}
	public RelayCommand(Func<T, object> action)
	{
		_func = action;
	}

	public bool CanExecute(object? parameter)
	{
		return true;
	}

	public void Execute(object? parameter)
	{
		if (_execute != null && parameter != null)
			_execute((T)parameter);
		else if (_func != null && parameter != null)
			_func((T)parameter);
	}
}
