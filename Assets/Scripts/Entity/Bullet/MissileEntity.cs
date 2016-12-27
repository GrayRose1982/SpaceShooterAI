using UnityEngine;
using System.Collections;

[System.Serializable]
public class MissileEntity:BulletEntity
{
	public float angleRotate;

	public MissileEntity ()
	{
	}

	public MissileEntity (string id, string name, int damage, int armorBreak, float speed, float angleRotate, Sprite sprite)
	{
		this.id = id;
		this.name = name;
		this.damage = damage;
		this.armorBreak = armorBreak;
		this.speed = speed;
		this.angleRotate = angleRotate;
		this.sprite = sprite;
	}

	public MissileEntity (MissileEntity m)
	{
		this.id = m.id;
		this.name = m.name;
		this.damage = m.damage;
		this.armorBreak = m.armorBreak;
		this.speed = m.speed;
		this.angleRotate = m.angleRotate;
		this.sprite = m.sprite;
	}

	public MissileEntity (MissileEntity m, int damage, int armorBreak)
	{
		this.id = m.id;
		this.name = m.name;
		this.damage = damage;
		this.armorBreak = armorBreak;
		this.speed = m.speed;
		this.angleRotate = m.angleRotate;
		this.sprite = m.sprite;
	}
}

