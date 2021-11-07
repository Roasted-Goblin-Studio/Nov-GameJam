using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : Projectile
{
    // Eggs should fall until they collide with something. 
    // If the egg collides with a player it should do damage and return to the pool

    private ReturnToPool returnToPool;
    [SerializeField] private float _EggTilt = 0f;

    protected override void Start()
    {
        base.Start();
        returnToPool = GetComponent<ReturnToPool>();
        float eggTilt = Random.Range(_EggTilt, -_EggTilt);
        Direction = new Vector2(eggTilt * 10, -1);
    }

    protected override void MoveProjectile()
    {
        _ProjectileMovement = Direction * _TotalSpeed * Time.deltaTime;
        _ProjectileRigidBody2D.MovePosition(_ProjectileRigidBody2D.position + _ProjectileMovement);

        // REMINDER: Having Acceleration and Speed at the same value will result in immediate force
        if(_StartingSpeed != 0) _TotalSpeed = _StartingSpeed += _Acceleration * Time.deltaTime;
        else _TotalSpeed += _Acceleration * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            Debug.Log("Player entered Egg");
            CharacterHealth characterHealth = other.GetComponent<CharacterHealth>();
            characterHealth.Damage(_ProjectileDamage);
        }

        foreach (var tag in _TagsToAvoid.TagsToAvoidStrings)
        {
            if(other.tag == tag){
                return;
            }
        }

        returnToPool.DestroyObject();
    }
}
