using UnityEngine;

public class EventListener : MonoBehaviour
{
    public TriggerEvent triggerSource;

    private void Start()
    {
        if (triggerSource != null)
        {
            // Subscribe a custom method to the UnityEvent
            triggerSource.onTriggered.AddListener(ReactToTrigger);
        }
    }

    void ReactToTrigger()
    {
        Debug.Log("The trigger event was received by EventListener!");
        // You can add anything here: sound, animation, UI feedback, etc.
    }
}