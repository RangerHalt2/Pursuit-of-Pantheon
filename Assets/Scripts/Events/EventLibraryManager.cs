using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomEventData
{
    public string eventName = "";
    public bool hasOccured = false;
    //public bool canRepeat; //Currently no implementation in the JSON's to read this information, moving on for now.
}


public class EventLibraryManager : MonoBehaviour
{

    [SerializeField] private string[] EventAddressableNames; //An array of strings of all the addressable events, the strings are their names.

    Dictionary<string, RandomEventData> eventsHash = new Dictionary<string, RandomEventData>();
    
    private void Start()
    {
        foreach (string eventAddressable in EventAddressableNames)
        {
            eventsHash.Add(eventAddressable, new RandomEventData
                {
                    eventName = eventAddressable
                }
            );
        }
    }

    public bool LoadRandomEvent()
    {
        List<RandomEventData> eligibleEvents = eventsHash.Values.Where(e => !e.hasOccured).ToList();
        if (eligibleEvents.Count == 0)
        {
            Debug.Log("No eligible events remaining.");
            return false;
        }

        RandomEventData randomEvent = eligibleEvents[Random.Range(0, eligibleEvents.Count)];

        if (randomEvent.eventName == "")
        {
            Debug.LogError("Error: The random event could not be selected. Nothing executed.");
            return false;
        }

        PlayerPrefs.SetString("NextDialogueToLoad", randomEvent.eventName);
        PlayerPrefs.Save();
        randomEvent.hasOccured = true;

        return true;
    }
}
