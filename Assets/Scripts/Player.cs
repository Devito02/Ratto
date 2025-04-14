using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour, IDamageable
{
    [BoxGroup("Stats")]
    public float Speed;
    [BoxGroup("Stats")]
    public float SpeedMultiplier;
    [BoxGroup("Stats")]
    [SerializeField] private float _maxHealth;
    [BoxGroup("Stats")]
    [SerializeField] private float _currentHealth;
    [BoxGroup("Debug")]
    public Vector3 _Dir;
    [BoxGroup("Reference")]
    public CharacterController CharacterController;
    [BoxGroup("Reference")]
    public MouthManager MouthManager;
    [BoxGroup("Reference")]
    public Transform Launch_Pos;
    [BoxGroup("Reference")]
    public Mouth_Helper Mouth_Helper;
    [BoxGroup("Reference")]
    public LayerMask FloorMask;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController.Move(_Dir * Speed * Time.deltaTime);
        Aim(); 

    }

    public void Player_Move(InputAction.CallbackContext callbackContext)
    {
        _Dir = new Vector3(callbackContext.ReadValue<Vector2>().x, 0, callbackContext.ReadValue<Vector2>().y);
    }

    public void Player_Rotate()
    {
        Vector2 ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 ScreenPlayerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 _mousePos = new Vector2(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width),
            Mathf.Clamp(Input.mousePosition.y, 0, Screen.height));
        Vector2 _dir = _mousePos - ScreenPlayerPos;
        Vector3 _3dir = new Vector3(_dir.x, 0, _dir.y);
        _dir.Normalize();
        this.transform.forward = _3dir;
    }

    public void Shoot(InputAction.CallbackContext callbackContext)
    {
        if (MouthManager.CurrentProjectiles <= 0) return;
        MouthManager.CurrentProjectiles--;

        Base_Projectile _proj = Instantiate(MouthManager.Projectile_Ref,
            Launch_Pos.position + (transform.forward * MouthManager.Projectile_Ref.InstantiateDistance),
            transform.rotation);


        _proj.Instantiate_Method(this.gameObject, Launch_Pos.position);
        _proj.Launch();

    }

    public void Swallow(InputAction.CallbackContext callbackContext)
    {
        if (Mouth_Helper.projectiles.Count > 0)
        {
            Destroy(Mouth_Helper.projectiles[0].gameObject);
            MouthManager.CurrentProjectiles++;

        }

    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {


            // Calculate the direction
            var direction = position - transform.position;

            // You might want to delete this line.
            // Ignore the height difference.
            direction.y = 0;

            // Make the transform look in the direction.
            transform.forward = direction;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, FloorMask))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }

    public void TakeDamage(float _damage)
    {
        _currentHealth -= _damage;
        if (_currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        Debug.Log("You Died");
        _currentHealth = _maxHealth;
    }
}
