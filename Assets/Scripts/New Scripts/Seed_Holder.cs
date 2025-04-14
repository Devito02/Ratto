using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Seed_Holder : MonoBehaviour
{
    public int MaxCapacity;
    public List<Seed> Seed_Reserve = new List<Seed>();
    public bool IsEmpty { get => Seed_Reserve.Count == 0; }

    public void Ingest(Seed _seed)
    {
        if (Seed_Reserve.Count == MaxCapacity)
            return;
        else
            Seed_Reserve.Add(_seed);
    }

    public Seed GiveSeed()
    {
        if (IsEmpty) return null;

        Seed _s = null;
        _s = Seed_Reserve[0];
        Seed_Reserve.RemoveAt(0);
        return _s;
    }

    public Seed GiveSeed(int seedNumber)
    {
        Seed _s = null;
        if (IsEmpty) return null;
        if(seedNumber >= Seed_Reserve.Count) return null;

        _s = Seed_Reserve[seedNumber];
        Seed_Reserve.RemoveAt(seedNumber);
        return _s;
    }
}
