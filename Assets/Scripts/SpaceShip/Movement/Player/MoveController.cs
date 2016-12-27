using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour
{
	//	public PanelMovePlayer left;
	//	public PanelMovePlayer right;

	public JoyStick joystick;

	public float speedMove = 3f;
	//	public float minSpeed = 1f;
	//	public float maxSpeed = 10f;
	//	public float speedRotate = 50f;

	public EngineEntity engine;

	public ParticleSystem engineParticle;

	public Vector2 direction;

	private Transform _trans;
	[SerializeField] private float errorAngle;
	[SerializeField] private Rigidbody2D rigid;

	void Start ()
	{
		_trans = transform;
		SetDirection ();
	}

	void Update ()
	{
//		if (left.isSelect && !right.isSelect) {
//			_trans.Rotate (0, 0, engine.angleRotate * Time.deltaTime);
//		} else if (!left.isSelect && right.isSelect) {
//			_trans.Rotate (0, 0, -engine.angleRotate * Time.deltaTime);
//		}
		JoystickController ();
		SetDirection ();
		MoveToward ();
	}

	private void SetDirection ()
	{
		float angle = _trans.rotation.eulerAngles.z - errorAngle;
		direction = new Vector2 (Mathf.Cos (angle * Mathf.Deg2Rad), Mathf.Sin (angle * Mathf.Deg2Rad));
		SetParticleDirection (angle - errorAngle);
	}

	private void SetParticleDirection (float angle)
	{
		float s = angle;
		while (s < -180 || s > 180)
			if (s < -180)
				s += 360;
			else
				s -= 360;

		if (s <= 180)
			s = -(180 + s);
		else
			s = 180 - s;
		s += 180;
		engineParticle.startRotation = Mathf.Deg2Rad * (s);
	}

	private void MoveToward ()
	{
//		Vector2 current = _trans.position;
//		_trans.position = Vector2.MoveTowards (current, current + direction.normalized, Time.deltaTime * speedMove);

		rigid.velocity = direction * speedMove;
	}

	private void JoystickController ()
	{
		Vector2 vec = joystick.finalDifference;

		_trans.Rotate (0, 0, -engine.angleRotate * Time.deltaTime * vec.x);
		IncreaseSpeed (vec.y);
	}

	public void IncreaseSpeed (float number)
	{
		if (Mathf.Abs (number) > 1f)
			return;

		number += 1;
		engineParticle.startLifetime = number * .8f;
		speedMove = (engine.maxSpeed - engine.minSpeed) / 2 * number + engine.minSpeed;
	}
}
