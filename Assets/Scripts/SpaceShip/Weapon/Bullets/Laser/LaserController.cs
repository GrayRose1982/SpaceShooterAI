using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour
{
	[SerializeField] private LaserEntity _laser;
	[SerializeField] private SpriteRenderer sr;
	[SerializeField] private float errorAngle;
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private BoxCollider2D boxCollider;
	public Vector2 direction;

	public LaserEntity laser {
		get { return _laser; }
		set {
			_laser = value; 
			sr.sprite = _laser.sprite;
		}
	}

	void OnEnable ()
	{
		SetDirection ();
		SetVelo ();
		boxCollider = gameObject.AddComponent<BoxCollider2D> ();
		boxCollider.isTrigger = true;
	}

	void OnDisable ()
	{
		_laser = null;
		rigid.velocity = Vector2.zero;
		Destroy (boxCollider);
	}

	private void SetDirection ()
	{
		float angle = transform.rotation.eulerAngles.z - errorAngle;
		direction = new Vector2 (Mathf.Cos (angle * Mathf.Deg2Rad), Mathf.Sin (angle * Mathf.Deg2Rad));
	}

	private void SetVelo ()
	{
		rigid.velocity = direction * _laser.speed;
	}

	public void Disappeal ()
	{
		LaserPooling.pool.ReturnLaser (this);
//		gameObject.SetActive (false);
	}
}
