using JetBrains.Annotations;
using UnityEngine;

public class Seed : MonoBehaviour
{
    
    public string Name;
    public Projectile_Elements Element;
    public float Speed;
    public float DurationTime;
    public float Damage;

    public Seed CopySeed(Projectile_Elements projectile_Elements)
    {
        Seed seed = new Seed();
        seed.Name = Name;
        seed.Speed = Speed;
        seed.DurationTime = DurationTime;
        seed.Damage = Damage;
        seed.CopySeed(projectile_Elements);
        return seed;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
