using UnityEngine;
using UnityEngine.Events;

/*
 * Listens if an event has been raised and executes an specific function depending on the event
 * 
 */

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised()
    { Response.Invoke(); }
}
