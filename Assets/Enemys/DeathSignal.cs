using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSignal : MonoBehaviour, IEventDispatcher
{
    //This script send a message to WaveManager to caunt remaning enemys
    private List<IObserver> listeners = new List<IObserver>();


    void Start(){
        listeners.Add(WaveManager.Instance);
    }

    private void OnDestroy()
    {
        // Notify listeners about the enemy's death
        onNotify("EnemyDied", this); 
    }

    public void AddListener(IObserver listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(IObserver listener)
    {
        listeners.Remove(listener);
    }

    public void onNotify(string eventName, object data = null)
    {
        foreach (IObserver listener in listeners)
        {
            listener.OnEvent(eventName, data);
        }
    }
}