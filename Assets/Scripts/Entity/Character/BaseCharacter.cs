using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseCharacter
{
	public string name;
	public string id;
	public int hp;
	public int armor;
	public Sprite sprite;
	public int coin;

	public BaseCharacter ()
	{
	}

	public BaseCharacter (BaseCharacter bc)
	{
		if (bc == null)
			return;

		name = bc.name;
		id = bc.id;
		hp = bc.hp;
		armor = bc.armor;
		sprite = bc.sprite;
		coin = bc.coin;
	}
}
