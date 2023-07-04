using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public class WeightedRandom<T>
{
    [Serializable]
    public struct Pair
    {
        public T item;
        public float weight;

        public Pair(T item, float weight)
        {
            this.item = item;
            this.weight = weight;
        }
    }

    public List<Pair> list = new List<Pair>();

    public int count
    {
        get => list.Count;
    }

    public void Add(T item, float weight)
    {
        list.Add(new Pair(item, weight));
    }

    public T GetRandom()
    {
        float totalWeight = 0;

        foreach (Pair pair in list)
        {
            totalWeight += pair.weight;
        }

        float value = Random.value * totalWeight;

        float sumWeight = 0;

        foreach (Pair pair in list)
        {
            sumWeight += pair.weight;
            
            if (sumWeight >= value)
            {
                return pair.item;
            }
        }

        return default(T);
    }
}