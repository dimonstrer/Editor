using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IObservable
{
    void AddObserver(IObserver obs);
    void RemoveObserver(IObserver obs);
    void NotifyEveryone(string type, Object obj);
}
