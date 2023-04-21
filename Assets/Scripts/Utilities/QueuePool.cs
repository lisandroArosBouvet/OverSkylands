using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QueuePool<T>:IPool<T>
{
    Func<T> produce;
    Func<T, bool> useTest;
    List<T> objects;

    public QueuePool(Func<T> factoryMethod, int initialSize, Func<T,bool> inUse)
    {
        produce = factoryMethod;
        useTest = inUse;
        objects = new List<T>(initialSize);
    }

    public T GetInstance()
    {
        foreach (var item in objects)
        {
            if (!useTest(item))
                return item;
        }
        var newItem = produce();
        objects.Add(newItem);
        return newItem;
    }
}
