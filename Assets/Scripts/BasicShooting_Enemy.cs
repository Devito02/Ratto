using UnityEngine;

public class BasicShooting_Enemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Base_Projectile Projectile_Ref;
    public Transform Launch_Pos;
    public float FireRate = 1, FireInitialPause = 2f;

    void Start()
    {
        InvokeRepeating("Shoot", FireInitialPause, FireRate);
    }

    private void OnDisable()
    {
        CancelInvoke("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        Base_Projectile _proj = Instantiate(Projectile_Ref,
            Launch_Pos.position + (transform.forward * Projectile_Ref.InstantiateDistance),
            transform.rotation);


        _proj.Instantiate_Method(this.gameObject, Launch_Pos.position);
        _proj.Launch();

    }
}
