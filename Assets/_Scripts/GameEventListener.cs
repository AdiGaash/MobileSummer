using UnityEngine;
using UnityEngine.Events;

// This script listens to a specific GameEvent and reacts (invokes a UnityEvent).
public class GameEventListener : MonoBehaviour
{
    // Reference to the GameEvent this listener is listening to
    public GameEvent gameEvent;

    // Response to invoke when the event is raised
    public UnityEvent response;

    private void OnEnable()
    {
        // Register this listener when the GameObject becomes active
        if (gameEvent != null)
            gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        // Unregister when the GameObject is deactivated
        if (gameEvent != null)
            gameEvent.UnregisterListener(this);
    }

    // This method will be called by the GameEvent when it is raised
    public void OnEventRaised()
    {
        response?.Invoke(); // Call the assigned UnityEvent actions
    }
}