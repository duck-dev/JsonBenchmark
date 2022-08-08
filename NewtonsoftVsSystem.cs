using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace JsonBenchmark;

[SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
[RankColumn, MinColumn, MaxColumn, AllStatisticsColumn, MemoryDiagnoser]
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
    
    // ------------------- Load -------------------
    
    [Benchmark]
    public List<int>? LoadIntegersNewtonsoft()
    {
        string content = File.ReadAllText(Utilities.FilePathIntegers);
        return JsonConvert.DeserializeObject<List<int>>(content);
    }
 
    [Benchmark]
    public List<int>? LoadIntegersSystem()
    {
        string content = File.ReadAllText(Utilities.FilePathIntegers);
        return JsonSerializer.Deserialize<List<int>>(content);
    }
    
    [Benchmark]
    public List<TestObject>? LoadObjectsNewtonsoft()
    {
        string content = File.ReadAllText(Utilities.FilePathObjects);
        return JsonConvert.DeserializeObject<List<TestObject>>(content);
    }

    [Benchmark]
    public List<TestObject>? LoadObjectsSystem()
    {
        string content = File.ReadAllText(Utilities.FilePathObjects);
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