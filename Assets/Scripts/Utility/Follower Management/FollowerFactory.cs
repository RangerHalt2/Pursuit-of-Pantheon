using UnityEngine;
using System.Linq;

public static class FollowerFactory
{
    // Loads all FollowerClass assets from Resources/ClassStats
    public static FollowerClass GetRandomClass()
    {
        var allClasses = Resources.LoadAll<FollowerClass>("ClassStats");
        var baseClasses = allClasses.Where(fc => fc.isStartingClass).ToArray(); // Filter for base/starting classes
        if (baseClasses.Length == 0) return null;
        return baseClasses[Random.Range(0, baseClasses.Length)];
    }

    public static FollowerClass GetClassByID(int classID)
    {
        var allClasses = Resources.LoadAll<FollowerClass>("ClassStats");
        foreach (var fc in allClasses)
        {
            if (fc.classID == classID)
                return fc;
        }
        return null;
    }
}
