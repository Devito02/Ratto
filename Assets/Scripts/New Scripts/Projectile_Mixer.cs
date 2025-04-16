using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Projectile_Mixer : SerializedMonoBehaviour
{
    public Seed_Holder seedHolder;
    

    public struct Combination
    {
        public Projectile_Elements Projectile_Elements1;
        public Projectile_Elements Projectile_Elements2;
    }

    public List<ValueTuple<Combination, Projectile_Elements>> PossibleCombinations = new List<(Combination, Projectile_Elements)> ();
    public Dictionary<Projectile_Elements,ElementalProjectile> Projectile_Dictionary = new Dictionary<Projectile_Elements, ElementalProjectile> ();

    void Start()
    {
        seedHolder = GetComponent<Seed_Holder>();
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            Combine();
            
        }
    }

    public Projectile_Elements OutputCombination()
    {
        if(seedHolder.Seed_Reserve.Count < 2) return Projectile_Elements.None;


        Projectile_Elements _seed1 = seedHolder.Seed_Reserve[0];
        Projectile_Elements _seed2 = seedHolder.Seed_Reserve[1];

        foreach((Combination, Projectile_Elements) kvp in PossibleCombinations)
        {
            if((kvp.Item1.Projectile_Elements1 == _seed1 && kvp.Item1.Projectile_Elements2 == _seed2) ||
                    (kvp.Item1.Projectile_Elements2 == _seed1 && kvp.Item1.Projectile_Elements1 == _seed2))
            {
                return kvp.Item2;
            }
        }
        return Projectile_Elements.None;

    }

    public void Combine()
    {
        Seed _seed = new Seed();
        Projectile_Elements projectile_Elements = OutputCombination();

        if (projectile_Elements != Projectile_Elements.None)
        {
            seedHolder.Seed_Reserve.Clear();

            seedHolder.Seed_Reserve.Add(projectile_Elements);
        }


    }

    public ElementalProjectile GetProjectilePref(Projectile_Elements projectile_Elements)
    {
        return Projectile_Dictionary[projectile_Elements];
    }

}
public enum Projectile_Elements
{
    None,
    Fire,
    Water,
    Air,
    Earth,
    Vapor,
    Thunder,
    Metal,
    Mud
}