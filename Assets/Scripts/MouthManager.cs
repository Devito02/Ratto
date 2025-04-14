using UnityEngine;

public class MouthManager : MonoBehaviour
{
    public float MaxProjectile;
    public float CurrentProjectiles;

    public Base_Projectile Projectile_Ref;

    public void Recharge()
    {
        CurrentProjectiles = MaxProjectile;
    }

    void Start()
    {
        Recharge();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
