using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Private

    // Protected
    protected Character _Character;
    protected float _OriginalMaxHealth;

    // Serialized
    [SerializeField] protected Slider _HealthIndicatorBar;  // Put the slider object here
    [SerializeField] protected float _CurrentHealth;
    [SerializeField] protected float _MaxHealth;

    // Public
    public float CurrentHealth => _CurrentHealth; //READONLY

    // Start is called before the first frame update
    protected virtual void Start()
    {        
        _CurrentHealth = _MaxHealth;
        _OriginalMaxHealth = _MaxHealth;
    }

    // Health indicator controller
    public void SetHealth(float Health)
    {
        _HealthIndicatorBar.value = Health;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Each enemy object needs a DamagePoints script with a "Points" value.
        // This could be done another way, but for now this works in dev.
        if (other.transform.CompareTag("EnemyDamage"))
        {
            _CurrentHealth -= other.gameObject.GetComponent<DamagePoints>().Points;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        SetHealth(_CurrentHealth / _MaxHealth);
    }

    public virtual void Heal(float amount){
        float newHealth = _CurrentHealth + amount;

        if (newHealth > _MaxHealth)
        {
            _CurrentHealth = _MaxHealth;
        }
        else
        {
            _CurrentHealth += amount;
        }
    }

    public virtual void Damage(float amount){
        if(!_Character.IsHitable){ return; }

        float newHealth = _CurrentHealth - amount;

        if (newHealth <= 0)
        {
            _CurrentHealth = 0;
            Die();
        }
        else
        {
            _CurrentHealth -= amount;
        }
    }

    public virtual void Die(){
        // Check if _Character is not null
        if(_Character){ _Character.IsAlive = false; }
    }
}
