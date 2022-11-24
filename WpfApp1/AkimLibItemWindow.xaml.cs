using System.Windows;
using WpfApp1.Models;

namespace WpfApp1;

public partial class AkimLibItemWindow : Window
{
    public AkimLibItem AkimLibItem { get; init; } = new();

    public AkimLibItemWindow()
    {
        InitializeComponent();
    }

    public AkimLibItemWindow(AkimLibItem akimLibItem) : this()
    {
        AkimLibItem = akimLibItem;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        DataContext = AkimLibItem;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}
