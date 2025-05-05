using UnityEngine;
using System.Collections.Generic;

// This ScriptableObject acts like a "broadcaster" for an event.
[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Events/Game Event")]
public class GameEvent : ScriptableObject
{
    // List of all listeners currently listening to this event
    private List<GameEventListener> listeners = new List<GameEventListener>();

    // This method is called to trigger (raise) the event
    public void Raise()
    {
        // Notify all listeners
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    // Called by listeners to start listening
    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    // Called by listeners to stop listening
    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
