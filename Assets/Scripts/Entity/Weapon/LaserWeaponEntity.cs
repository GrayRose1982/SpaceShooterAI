using UnityEngine;
using System.Collections;

[System.Serializable]
public class LaserWeaponEntity : WeaponEntity
{
	public LaserEntity laser;

	public LaserWeaponEntity ()
	{
	}

	public LaserWeaponEntity (LaserWeaponEntity laserWeapon)
	{
		if (laserWeapon == null)
			return;

		id = laserWeapon.id;
		damage = laserWeapon.damage;
		armorBreak = laserWeapon.armorBreak;
		timePerShoot = laserWeapon.timePerShoot;
		bulletID = laserWeapon.bulletID;
		number = laserWeapon.number;
		laser = new LaserEntity (laserWeapon.laser, laserWeapon.damage, laserWeapon.armorBreak);
	}
}
