public interface IObserver
{
    //subject uses this method to comunicate with the observer
    public void OnEvent(string eventName, object data = null);
}
