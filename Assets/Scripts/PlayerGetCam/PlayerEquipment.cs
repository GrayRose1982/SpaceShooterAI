using UnityEngine;
using System.Collections;

public class PlayerEquipment : MonoBehaviour
{
	public static PlayerEquipment equip;

	public Character character;
	public MoveController moveController;
	public WeaponController weaponController;

	void Start ()
	{
		equip = this;
	}
}
