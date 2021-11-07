using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    protected override void Start()
    {
        base.Start();
    }

    override protected void MoveProjectile(){
        _ProjectileMovement = Direction * _TotalSpeed * Time.deltaTime;
        _ProjectileRigidBody2D.MovePosition(_ProjectileRigidBody2D.position + _ProjectileMovement);

        // REMINDER: Having Acceleration and Speed at the same value will result in immediate force
        if(_StartingSpeed != 0) _TotalSpeed = _StartingSpeed += _Acceleration * Time.deltaTime;
        else _TotalSpeed += _Acceleration * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            CharacterHealth characterHealth = other.GetComponent<CharacterHealth>();
            characterHealth.Damage(_ProjectileDamage);
            _ReturnToPool.DestroyObject();
        }
        
        foreach (var tag in _TagsToAvoid.TagsToAvoidStrings)
        {
            if(other.tag == tag){
                return;
            }
        }
        if(other.tag == "Non Hitable") {
            return;
        }

        _ReturnToPool.DestroyObject();
    }

    
}
