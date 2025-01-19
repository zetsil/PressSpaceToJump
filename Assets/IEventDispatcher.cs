using UnityEngine;

public interface IEventDispatcher
{
    void AddListener(IObserver listener);
    void RemoveListener(IObserver listener);
    void onNotify(string eventName, object data = null);
}