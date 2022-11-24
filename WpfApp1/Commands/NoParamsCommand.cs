using System;
using System.Windows.Input;

namespace WpfApp1.Commands;

public class NoParamsCommand : ICommand
{
    private readonly Action _execute;

    public NoParamsCommand(Action execute)
    {
        _execute = execute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => _execute();
}
