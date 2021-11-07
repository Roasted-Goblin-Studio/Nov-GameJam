using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObjectPooler : ObjectPooler
{
    private Weapon _Weapon;
    public Weapon Weapon { get => _Weapon; set => _Weapon = value; }

    protected override string ObjectPoolerName()
    {
        return Weapon.WeaponOwner.CharacterType + " " + Weapon.WeaponName + " " + _ObjectPrefab.name + " Pool";
    }
}
