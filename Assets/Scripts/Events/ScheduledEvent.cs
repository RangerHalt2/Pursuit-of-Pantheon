using UnityEngine;

[System.Serializable]
public class ScheduledEvent
{
    // The act the event occurs in
    public int act;
    // The turn in the act the event occurs on
    public int turn;
    // The name of the event
    public string eventName;
    // Determines whether or not to load a dialogue scene
    public bool loadDialogueScene;
    // The name of the Dialogue JSON file to be loaded
    public string dialogueFileName;
}

[System.Serializable]
public class ScheduledEventList
{
    // List of all scheduled events
    public ScheduledEvent[] scheduledEvents;
}