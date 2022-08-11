using System.Text.Json;

namespace JsonBenchmark;

public static class Utilities
{
    public const string FilePathIntegers = "Integers.json";
    public const string FilePathObjects = "Objects.json";

    public static void FillJson(NewtonsoftVsSystem instance)
    {
        IReadOnlyList<List<int>> integers = instance.Integers().ToList();
        IReadOnlyList<List<TestObject>> objects = instance.Objects().ToList();
        
        IReadOnlyList<string> integerPaths = instance.IntegerPaths().ToList();
        IReadOnlyList<string> objectPaths = instance.ObjectPaths().ToList();

        for(int i = 0; i < integers.Count; i++)
            SaveData(integerPaths[i], integers[i]);
        for(int i = 0; i < objects.Count; i++)
            SaveData(objectPaths[i], objects[i]);
    }

    private static void SaveData<T>(string path, List<T> list)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(path, jsonString);
    }
}