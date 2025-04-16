using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Seed_Holder : MonoBehaviour
{
    public Seed_Launcher Seed_Launcher;
    public int MaxCapacity;
    public List<Projectile_Elements> Seed_Reserve = new List<Projectile_Elements>();
    public bool IsEmpty { get => Seed_Reserve.Count == 0; }

    private void Start()
    {
        Seed_Launcher = GetComponent<Seed_Launcher>();
    }

    public void Ingest(Projectile_Elements _seed)
    {
        if (Seed_Reserve.Count == MaxCapacity)
            return;
        else
            Seed_Reserve.Add(_seed);
    }

    public Projectile_Elements GiveSeed()
    {
        if (IsEmpty) return Projectile_Elements.None;

        Projectile_Elements _s = Seed_Reserve[0];
        Seed_Reserve.RemoveAt(0);
        return _s;
    }

    public Projectile_Elements GiveSeed(int seedNumber)
    {
        if (IsEmpty) return Projectile_Elements.None;
        if(seedNumber >= Seed_Reserve.Count) return Projectile_Elements.None;

        Projectile_Elements _s = Seed_Reserve[seedNumber];
        Seed_Reserve.RemoveAt(seedNumber);
        return _s;
    }
}
