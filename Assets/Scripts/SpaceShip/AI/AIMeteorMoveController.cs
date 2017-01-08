using UnityEngine;
using System.Collections;

public class AIMeteorMoveController : MonoBehaviour
{
	public float speedMove = 3f;
	public float speedRotate = 50f;

	public Vector2 direction;

	private Transform _trans;
	private float sizeDeltaMove = 3f;

	[SerializeField] private Vector2 posMoveTo;

	[SerializeField] private Rigidbody2D rigid;

	void OnEnable ()
	{
		//		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Start ()
	{
		if (!rigid)
			rigid = GetComponent<Rigidbody2D> ();

		_trans = transform;
		GetNewPosToMove ();

		#region remove after test in garage
		SetDirection ();
		MoveToward ();
		#endregion
	}

	void Update ()
	{
		Vector2 dif;

		dif = posMoveTo - (Vector2)_trans.position;
		if (dif.magnitude <= sizeDeltaMove)
			GetNewPosToMove ();
		
		Rotate ();
	}

	private void Rotate ()
	{
		_trans.Rotate (0, 0, speedRotate * Time.deltaTime);
	}

	private void SetDirection ()
	{
		direction = posMoveTo - (Vector2)_trans.position;
		direction.Normalize ();
	}

	private void MoveToward ()
	{
		rigid.velocity = direction * speedMove;
	}

	private void GetNewPosToMove ()
	{
		if (!GenerateForFree.generateMap)
			return;

		posMoveTo = GenerateForFree.generateMap.GetRandomSpace ();
		SetDirection ();
		MoveToward ();
	}

	private void FreeGo ()
	{
		posMoveTo = new Vector2 (Random.Range (-100f, 100f), Random.Range (-100f, 100f));

		SetDirection ();
		MoveToward ();
	}

}