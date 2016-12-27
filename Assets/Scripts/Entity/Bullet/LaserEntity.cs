using UnityEngine;
using System.Collections;

[System.Serializable]
public class LaserEntity:BulletEntity
{
	public LaserEntity ()
	{
	}

	public LaserEntity (string id, string name, int damage, int armorBreak, float speed, Sprite sprite)
	{
		this.id = id;
		this.name = name;
		this.damage = damage;
		this.armorBreak = armorBreak;
		this.speed = speed;
		this.sprite = sprite;
	}

	public LaserEntity (LaserEntity l)
	{
		id = l.id;
		name = l.name;
		damage = l.damage;
		armorBreak = l.armorBreak;
		speed = l.speed;
		sprite = l.sprite;
	}

	public LaserEntity (LaserEntity l, int damage, int armorBreak)
	{
		id = l.id;
		name = l.name;
		this.damage = damage;
		this.armorBreak = armorBreak;
		speed = l.speed;
		sprite = l.sprite;
	}
}
