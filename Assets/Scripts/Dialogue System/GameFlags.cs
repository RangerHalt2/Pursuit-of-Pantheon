using System.Collections.Generic;
using UnityEngine;

public static class GameFlags
{
    private static Dictionary<string, bool> boolFlags = new Dictionary<string, bool>();
    private static Dictionary<string, int> intFlags = new Dictionary<string, int>();
    private static Dictionary<string, string> stringFlags = new Dictionary<string, string>();

    public static void Set(string key, bool value)
    {
        boolFlags[key] = value;
    }

    public static void Set(string key, int value)
    {
        intFlags[key] = value;
    }

    public static void Set(string key, string value)
    {
        stringFlags[key] = value;
    }

    public static bool GetBool(string key)
    {
        return boolFlags.TryGetValue(key, out bool value) && value;
    }

    public static int GetInt(string key)
    {
        return intFlags.TryGetValue(key, out int value) ? value : 0;
    }

    public static string GetString(string key)
    {
        return stringFlags.TryGetValue(key, out string value) ? value : null;
    }
}
