using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggLauncher : Weapon
{
    [SerializeField] private Transform _ProjectileSpawnPosition;

    protected ObjectPooler _ObjectPooler;

    public Transform ProjectileSpawnPosition { get => _ProjectileSpawnPosition; set => _ProjectileSpawnPosition = value; }
    public ObjectPooler ObjectPooler { get => _ObjectPooler; set => _ObjectPooler = value; }

    protected override void Start()
    {
        base.Start();
        ObjectPooler = GetComponent<ObjectPooler>();
    }

    protected override void UseWeapon()
    {
        SpawnProjectile();
    }

    protected void SpawnProjectile(){
        GameObject pooledProjectile = ObjectPooler.GetGameObjectFromPool();

        pooledProjectile.transform.position = ProjectileSpawnPosition.position;
        pooledProjectile.SetActive(true);

        Vector2 newDirection = WeaponOwner.IsFacingRight ? transform.right : -transform.right;

        //Projectile projectile = pooledProjectile.GetComponent<Projectile>();
        //projectile.SetDirection(newDirection, transform.rotation, WeaponOwner.IsFacingRight);
    }

}
