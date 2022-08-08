using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JsonBenchmark;

public class TestObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private string _name;
    private string _description;
    private ObservableCollection<TestObject>? _items;

    [System.Text.Json.Serialization.JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    public TestObject(string name, string description, ObservableCollection<TestObject>? items, Dictionary<int, bool>? exampleDictionary)
    {
        this.Name = _name = name;
        this.Description = _description = description;
        this.Items = _items = items;
        this.ExampleDictionary = exampleDictionary;
    }

    public string Name
    {
        get => _name;
        set => _name = value.Trim();
    }

    public string Description
    {
        get => _description;
        set => _description = value.Trim();
    }

    public ObservableCollection<TestObject>? Items
    {
        get => _items;
        set
        {
            _items = value;
            NotifyPropertyChanged();
        }
    }

    public Dictionary<int, bool>? ExampleDictionary { get; } = new();

    public void NotifyPropertyChanged([CallerMemberName] string propertyName = "") 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}