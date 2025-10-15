using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    //This should store variables and change their counts.
    //Public functions to get/set should be the core of this script.

    public List<string> items;

    public void Start()
    {
        items = new List<string>();
    }
}
