using System.Collections;
using System.Collections.Generic;
using static IObserver;
public class JumpLisener
{
    private List<IObserver> _observers = new List<IObserver>();
    // Start is called before the first frame update

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (IObserver observer in _observers)
        {
            observer.OnEvent("Jump");
        }
    }
}
