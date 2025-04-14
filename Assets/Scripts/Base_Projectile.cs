using System.Linq;
using UnityEngine;

public class Base_Projectile : Old_Projectile
{
    public float LaunchForce;
    public float InstantiateDistance = 0.1f;
    public GameObject Source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override Old_Projectile Instantiate_Method(GameObject player, Vector3 LaunchPos)
    {
        Source = player;
        transform.position = LaunchPos + (player.transform.forward * InstantiateDistance);
        _dir = transform.forward;
        return this;
    }

    public override void Launch()
    {
        base.Launch();
        rb.AddForce(LaunchForce * _dir);

    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (HasCollided) return;
        if (IsDontInteract(other)) return;

        
        Base_Projectile _proj = other.GetComponent<Base_Projectile>();
        //Temporary Solution -- Checks for also childs of player
        Transform[] _sourceChildrens = Source.transform.GetChildren(true);
        bool IsNotSourceorChilds = !_sourceChildrens.Contains(other.gameObject.transform) && other.gameObject.name != Source.gameObject.name;

        if (IsNotSourceorChilds && _proj == null)
        {
            Debug.Log(other.gameObject.name);

            HasCollided = true;
            IDamageable damageable = other.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(Damage);
            }

            Destroy(gameObject);
        }
    }

}
