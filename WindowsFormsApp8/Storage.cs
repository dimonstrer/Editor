using System;
using System.Collections.Generic;
using System.IO;

public class Storage : IObservable
{
    List<IObserver> observers;
    private Shape[] arr;
    private int size;
    private int count;
    public Storage()
    {
        observers = new List<IObserver>();
        size = 0;
        count = 0;
        arr = new Shape[size];
    }
    public Storage(int i)
    {
        size = i;
        count = 0;
        arr = new Shape[size];
        for (i = 0; i < size; i++)
            arr[i] = null;
    }
    private void incSize()
    {
        int oldsize = size;
        Array.Resize(ref arr, size= size*2);
        for (int i = oldsize; i < size; i++)
            arr[i] = null;
    }
    public void addObj(Shape obj, int i)
    {
        if (count == size)
            incSize();
        count++;
        if (i > size | i < 0)
            i = 0;
        if (arr[i] == null)
        {
            arr[i] = obj;
            return;
        }
        for (int j = 0; j < size; j++)
            if (arr[j] == null)
            {
                arr[j] = obj;
                return;
            }
    }
    public int getSize()
    {
        return size;
    }
    public Shape getObject(int i)
    {
        return arr[i];
    }
    public void deleteObj(int i)
    {
        arr[i] = null;
        count--;
    }
    public void deleteObj(Shape s)
    {
        for (int i = 0; i < arr.Length; i++)
            if (arr[i] == s)
            {
                arr[i] = null;
                count--;
                return;
            }
    }
    public void save(StreamWriter sw)
    {
        sw.WriteLine(count.ToString());
        for (int i = 0; i < count; i++)
            if(arr[i]!=null)
                arr[i].save(sw);
    }
    public void load(StreamReader rw, Factory factory)
    {
        string type = string.Empty;
        count = int.Parse(rw.ReadLine());
        size = count;
        arr = new Shape[count];
        for (int i = 0; i < count; i++)
        {
            type = rw.ReadLine();
            arr[i] = factory.create(type);
            if (arr[i] != null)
                arr[i].load(rw, factory);
        }
    }

    public void AddObserver(IObserver obs)
    {
        observers.Add(obs);
    }

    public void RemoveObserver(IObserver obs)
    {
        observers.Remove(obs);
    }

    public void NotifyEveryone(string type, Object obj)
    {
        foreach (IObserver obser in observers)
            obser.Update(type, obj);
    }
}
    

