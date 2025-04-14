using System;
using Unity.VisualScripting;
using UnityEngine;

public class Old_Projectile : MonoBehaviour
{

    public float Damage;
    public Rigidbody rb;
    public Vector3 _dir;
    protected bool HasCollided = false;

    public event Action<Old_Projectile> OnProjectileDie;
    public virtual void Launch()
    {
        //Launch
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual Old_Projectile Instantiate_Method(GameObject player, Vector3 LaunchPos)
    {
        //Base Method
        return this;
    }

    public virtual void OnDestroy()
    {
        rb = null;
        OnProjectileDie?.Invoke(this);
        
    }

    protected bool IsDontInteract(Collider other)
    {
        IDontInteract interact = other.GetComponent<IDontInteract>();
        return interact != null;
    }

    public virtual void OnTriggerEnter(Collider other)
    {


    }

    public virtual void OnTriggerExit(Collider other)
    {
        HasCollided = false;
    }

}
