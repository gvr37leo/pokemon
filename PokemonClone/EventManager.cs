public class Event {
    public string type;
    public object data;

}

public class Listener {
    public string name;
    public string type;
    public Action<object> cb;
}


public class EventManager {
    List<Listener> listeners = new List<Listener>();
    List<Event> eventqueue = new List<Event>();

    public void listen(string type, Action<object> cb, string name = "") {
        listeners.Add(new Listener() { 
            name = name,
            type = type,
            cb = cb,
        });
    }

    public void unlisten(string id) {
        listeners.RemoveAll(l => l.name == id);
    }

    public void trigger(string type,object data) {
        eventqueue.Add(new Event() {
            data = data,
            type = type,
        });
    }

    public void immediate(string type,object data) {
        listeners.FindAll(l => l.type == type).ForEach(l => l.cb(data));
    }

    public void process() {
        while(eventqueue.Count > 0) {
            var ev = eventqueue[0];
            eventqueue.RemoveAt(0);
            foreach (var listener in listeners.ToList()) {//tolist makes a copy to allow modification of original list
                if(listener.type == ev.type) {
                    listener.cb(ev.data);
                }
            }
            
        }
    }
}
