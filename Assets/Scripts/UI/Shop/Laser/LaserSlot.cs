using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LaserSlot : MonoBehaviour
{
	[SerializeField] private LaserWeaponEntity _weapon;

	public LaserWeaponEntity weapon {
		get { return _weapon; }
		set {
			_weapon = value;
			ChangeDescription ();
		}
	}

	public Image mainImg;
	public Text nameTxt;
	public Text DamageTxt;
	public Text timePerShootTxt;
	public Text speedTxt;

	public Slider damageFill;
	public Slider speedFill;

	private void ChangeDescription ()
	{
		mainImg.sprite = _weapon.laser.sprite;
		mainImg.SetNativeSize ();
		nameTxt.text = _weapon.laser.name;

		timePerShootTxt.text = _weapon.timePerShoot.ToString ();
		DamageTxt.text = _weapon.damage.ToString ();
		speedTxt.text = _weapon.laser.speed.ToString ();
		damageFill.value = (float)_weapon.damage / ConstNumber.LaserEnergyDamageMax;
		speedFill.value = (float)_weapon.laser.speed / ConstNumber.LaserSpeedMax;
	}
}
