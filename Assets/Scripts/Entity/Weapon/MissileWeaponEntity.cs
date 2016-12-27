using UnityEngine;
using System.Collections;

[System.Serializable]
public class MissileWeaponEntity : WeaponEntity
{
	public MissileEntity missile;

	public MissileWeaponEntity ()
	{
	}

	public MissileWeaponEntity (MissileWeaponEntity missileWeapon)
	{
		id = missileWeapon.id;
		damage = missileWeapon.damage;
		armorBreak = missileWeapon.armorBreak;
		timePerShoot = missileWeapon.timePerShoot;
		bulletID = missileWeapon.bulletID;
		number = missileWeapon.number;
		missile = new MissileEntity (missileWeapon.missile, missileWeapon.damage, missileWeapon.armorBreak);
	}
}
