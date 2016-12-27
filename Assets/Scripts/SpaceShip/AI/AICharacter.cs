using UnityEngine;
using System.Collections;

public class AICharacter : MonoBehaviour
{
	[SerializeField] private BaseCharacter _character;

	[SerializeField] private SpriteRenderer sr;

	public BaseCharacter character {
		get { return _character; }
		set {
			_character = value;
			SetCharacter ();
		}
	}

	public bool isMeteor;
	public PolygonCollider2D polygon;

	void OnEnable ()
	{
		polygon = gameObject.AddComponent <PolygonCollider2D> ();
		polygon.isTrigger = true;
	}

	void OnDisable ()
	{
		Destroy (polygon);
	}

	public void TakeDamage (int damage, int armorBreak)
	{
		Debug.Log ("TakeDamage " + damage + " " + armorBreak);
		_character.armor -= armorBreak;
		if (_character.armor < 0) {
			_character.hp -= damage;
			_character.armor = 0;
		} else {
			_character.armor -= Mathf.RoundToInt (damage / 2);
			if (_character.armor < 0) {
				_character.hp += _character.armor * 2;
				_character.armor = 0;
			}
		}

		if (_character.hp <= 0)
			DestroyPlayer ();
	}

	public void DestroyPlayer ()
	{
		GameController.gc.AddCoin (_character.coin);

		ItemDroppedPooling.pool.GetDropBox (transform.position);
		CharacterPooling.pool.ReturnShip (GetComponentInParent<AIMoveController> ());

		Debug.LogWarning ("Kill " + transform.name + " or" + character.name);
	}

	void OnTriggerEnter2D (Collider2D hit)
	{
		string tag = hit.tag;
		int damage = 0, armorBreak = 0;
		switch (tag) {
		case "Laser":
			LaserController laser = hit.GetComponent<LaserController> ();
			damage = laser.laser.damage;
			armorBreak = laser.laser.armorBreak;

			laser.Disappeal ();
			break;
		case "Missile":
			MissileController missile = hit.GetComponent<MissileController> ();
			damage = missile.missile.damage;
			armorBreak = missile.missile.armorBreak;

			missile.Disappeal ();
			break;

		default:
			break;
		}

		if (damage > 0 || armorBreak > 0) {
			TakeDamage (damage, armorBreak);
		}
	}

	//	private void GetItem (ItemDropped itemDropped)
	//	{
	//		if (!itemDropped)
	//			return;
	//
	//		switch (itemDropped.type) {
	//		case TypeItemDrop.Character:
	//		case TypeItemDrop.LaserWeapon:
	//		case TypeItemDrop.MissileWeapon:
	//		default:
	//			break;
	//		}
	//	}

	private void SetCharacter ()
	{
		sr.sprite = _character.sprite;

		if (polygon)
			Destroy (polygon);

		polygon = gameObject.AddComponent <PolygonCollider2D> ();
		polygon.isTrigger = true;
	}
}
