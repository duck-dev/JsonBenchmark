namespace JsonBenchmark;

public static class Utilities
{
    public const string FilePathIntegers = "Integers.json";
    public const string FilePathObjects = "Objects.json";
    
    public static void FillJson(NewtonsoftVsSystem instance)
    {
        instance.SaveIntegersSystem(Entities.ExtraLargePrimitives);
        instance.SaveObjectsSystem(Entities.ExtraLargeObjects);
    }
}