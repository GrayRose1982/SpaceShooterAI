using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeaponEntity
{
	public string id;
	public int damage = 10;
	public int armorBreak = 10;

	public float timePerShoot = 1f;

	public string bulletID;

	public int number;
}
