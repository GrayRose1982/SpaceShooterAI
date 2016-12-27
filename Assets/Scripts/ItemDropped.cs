using UnityEngine;
using System.Collections;

public class ItemDropped : MonoBehaviour
{
	[SerializeField] private LaserWeaponEntity _lwEntity;
	[SerializeField] private MissileWeaponEntity _mwEntity;
	[SerializeField] private BaseCharacter _baseCharacter;

	[SerializeField] private SpriteRenderer sr;
	[SerializeField] private Color[] colors;
	public TypeItemDrop type;

	public LaserWeaponEntity lwEntity {
		get {
			return _lwEntity;
		}
		set {
			_lwEntity = value;
			if (_lwEntity != null)
			if (_lwEntity.laser != null)
				SetSprite (_lwEntity.laser.sprite);
			
			type = TypeItemDrop.LaserWeapon;
		}
	}

	public MissileWeaponEntity mwEntity {
		get {
			return _mwEntity;
		}
		set {
			_mwEntity = value;
			if (_mwEntity != null)
			if (_mwEntity.missile != null)
				SetSprite (_mwEntity.missile.sprite);
			type = TypeItemDrop.MissileWeapon;
		}
	}

	public BaseCharacter baseCharacter {
		get {
			return _baseCharacter;
		}
		set {
			_baseCharacter = value;
			if (_baseCharacter != null)
				SetSprite (_baseCharacter.sprite);
			type = TypeItemDrop.Character;
		}
	}

	private void ClearStore ()
	{
		lwEntity = null;
		mwEntity = null;
		baseCharacter = null;
	}

	private void SetSprite (Sprite sprite)
	{
		sr.sprite = sprite;
	}

	void OnEnable ()
	{
		
	}

	void OnDisable ()
	{
		ClearStore ();
	}
}
