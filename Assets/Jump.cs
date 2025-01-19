using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Jump : MonoBehaviour
{

    private static Jump _instance;

    public static Jump Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Jump>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("Jump");
                    _instance = obj.AddComponent<Jump>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

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

    public void NotifyObservers(string message)
    {
        foreach (IObserver observer in _observers)
        {
            observer.OnEvent(message);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NotifyObservers("Jump"); 
        }
    }


}
