using System.Collections.ObjectModel;

namespace JsonBenchmark;

internal static class Entities
{
    private static readonly TestObject _subObject = 
        new ("Sub Object", "This is an item for a test object.", null, null);
    
    private static readonly ObservableCollection<TestObject> _items = new(Enumerable.Repeat(_subObject, 200));
    private static readonly Dictionary<int, bool> _dictionary = new()
    {
        { 1, true },
        { 2, false },
        { 4, true },
        { 8, true },
        { 16, false }
    };
    
    private static readonly TestObject _testingObject = 
        new ("Test Object", "This is the a test object for the Benchmark.", _items, _dictionary);

    internal static readonly List<int> SmallPrimitives = Enumerable.Range(1, 2000).ToList();
    internal static readonly List<int> MediumPrimitives = Enumerable.Range(1, 20_000).ToList();
    internal static readonly List<int> LargePrimitives = Enumerable.Range(1, 80_000).ToList();
    internal static readonly List<int> ExtraLargePrimitives = Enumerable.Range(1, 2_000_000).ToList();

    internal static readonly List<TestObject> SmallObjects = Enumerable.Repeat(_testingObject, 10).ToList();
    internal static readonly List<TestObject> MediumObjects = Enumerable.Repeat(_testingObject, 50).ToList();
    internal static readonly List<TestObject> LargeObjects = Enumerable.Repeat(_testingObject, 100).ToList();
    internal static readonly List<TestObject> ExtraLargeObjects = Enumerable.Repeat(_testingObject, 10_000).ToList();
}