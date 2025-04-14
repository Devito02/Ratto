using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [BoxGroup("Stats")]
    [SerializeField] private float _maxHealth;
    [BoxGroup("Stats")]
    [SerializeField] private float _currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float _damage)
    {
        _currentHealth -= _damage;
        if (_currentHealth <= 0)
            Die();
    }

    public void Die()
    {

        Destroy(gameObject);
    }
}
