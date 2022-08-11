using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace JsonBenchmark;

[SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
[Orderer(SummaryOrderPolicy.Default, MethodOrderPolicy.Declared)]
[MinColumn, MaxColumn, AllStatisticsColumn, MemoryDiagnoser]
public class NewtonsoftVsSystem
{
    public NewtonsoftVsSystem()
        => Utilities.FillJson(this);
    
    public IEnumerable<List<int>> Integers()
    {
        yield return Entities.SmallPrimitives;
        yield return Entities.MediumPrimitives;
        yield return Entities.LargePrimitives;
        yield return Entities.ExtraLargePrimitives;
    }
    
    public IEnumerable<List<TestObject>> Objects()
    {
        yield return Entities.SmallObjects;
        yield return Entities.MediumObjects;
        yield return Entities.LargeObjects;
        yield return Entities.ExtraLargeObjects;
    }

    public IEnumerable<string> IntegerPaths()
    {
        yield return "SmallIntegers.json";
        yield return "MediumIntegers.json";
        yield return "LargeIntegers.json";
        yield return "ExtraLargeIntegers.json";
    }
    
    public IEnumerable<string> ObjectPaths()
    {
        yield return "SmallObjects.json";
        yield return "MediumObjects.json";
        yield return "LargeObjects.json";
        yield return "ExtraLargeObjects.json";
    }

    // ------------------- Load -------------------
    
    [Benchmark]
    [ArgumentsSource(nameof(IntegerPaths))]
    public List<int>? LoadIntegersNewtonsoft(string path)
    {
        string content = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<int>>(content);
    }
 
    [Benchmark]
    [ArgumentsSource(nameof(IntegerPaths))]
    public List<int>? LoadIntegersSystem(string path)
    {
        string content = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<int>>(content);
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(ObjectPaths))]
    public List<TestObject>? LoadObjectsNewtonsoft(string path)
    {
        string content = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<TestObject>>(content);
    }

    [Benchmark]
    [ArgumentsSource(nameof(ObjectPaths))]
    public List<TestObject>? LoadObjectsSystem(string path)
    {
        string content = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<TestObject>>(content);
    }
    
    // ------------------- Save -------------------
    
    [Benchmark]
    [ArgumentsSource(nameof(Integers))]
    public void SaveIntegersNewtonsoft(List<int> list)
    {
        var options = new JsonSerializerSettings { Formatting = Formatting.Indented };
        string jsonString = JsonConvert.SerializeObject(list, options);
        File.WriteAllText(Utilities.FilePathIntegers, jsonString);
    }

    [Benchmark]
    [ArgumentsSource(nameof(Integers))]
    public void SaveIntegersSystem(List<int> list)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(Utilities.FilePathIntegers, jsonString);
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(Objects))]
    public void SaveObjectsNewtonsoft(List<TestObject> list)
    {
        var options = new JsonSerializerSettings { Formatting = Formatting.Indented };
        string jsonString = JsonConvert.SerializeObject(list, options);
        File.WriteAllText(Utilities.FilePathObjects, jsonString);
    }

    [Benchmark]
    [ArgumentsSource(nameof(Objects))]
    public void SaveObjectsSystem(List<TestObject> list)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(Utilities.FilePathObjects, jsonString);
    }
}