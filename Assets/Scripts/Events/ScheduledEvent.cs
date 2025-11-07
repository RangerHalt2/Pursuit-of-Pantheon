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
}

[System.Serializable]
public class ScheduledEventList
{
    // List of all scheduled events
    public ScheduledEvent[] scheduledEvents;
}