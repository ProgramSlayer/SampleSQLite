using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using WpfApp1.Base;
using WpfApp1.Commands;
using WpfApp1.Context;
using WpfApp1.Models;

namespace WpfApp1.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private readonly AkimLibDbContext _context = new();
    private ObservableCollection<AkimLibItem> _items = new();
    private NoParamsCommand? _loadAkimLibItemsCommand;
    private NoParamsCommand? _addAkimLibItemCommand;
    private OneParamCommand? _editAkimLibItemCommand;
    private OneParamCommand? _deleteAkimLibItemCommand;

    public ReadOnlyObservableCollection<AkimLibItem> AkimLibItems { get; init; }

    public MainWindowViewModel()
    {
        _context.Database.EnsureCreated();
        AkimLibItems = new(_items);
    }

    public NoParamsCommand LoadAkimLibItemsCommand
    {
        get
        {
            return _loadAkimLibItemsCommand ??
                new NoParamsCommand(async () =>
                {
                    var items = await _context.AkimLibItems.ToListAsync();
                    _items.Clear();
                    foreach (var item in items)
                    {
                        _items.Add(item);
                    }
                });
        }
    }

    public NoParamsCommand AddAkimLibItemCommand
    {
        get
        {
            return _addAkimLibItemCommand ??
                new NoParamsCommand(async () =>
                {
                    AkimLibItemWindow itemWindow = new();
                    if (itemWindow.ShowDialog() == true)
                    {
                        var newItem = itemWindow.AkimLibItem;
                        _context.AkimLibItems.Add(itemWindow.AkimLibItem);
                        await _context.SaveChangesAsync();
                        _items.Add(newItem);
                    }
                });
        }
    }

    public OneParamCommand EditAkimLibItemCommand
    {
        get
        {
            return _editAkimLibItemCommand ??
                new OneParamCommand(async selectedAkimLibItem =>
                {
                    if (selectedAkimLibItem is AkimLibItem editItem)
                    {
                        AkimLibItemWindow editItemWindow = new(editItem);
                        if (editItemWindow.ShowDialog() == true)
                        {
                            _context.Update(editItem);
                            await _context.SaveChangesAsync();
                        }
                    }
                }, null);
        }
    }

    public OneParamCommand DeleteAkimLibItemCommand
    {
        get
        {
            return _deleteAkimLibItemCommand ??
                new OneParamCommand(async selectedAkimLibItem =>
                {
                    if (selectedAkimLibItem is AkimLibItem itemDelete)
                    {
                        _context.Remove(itemDelete);
                        await _context.SaveChangesAsync();
                        _items.Remove(itemDelete);
                    }
                }, null);
        }
    }
}
