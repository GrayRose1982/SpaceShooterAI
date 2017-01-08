using UnityEngine;
using System.Collections;

public class GenerateForFree : MonoBehaviour
{
	public static GenerateForFree generateMap;

	public Transform topEdge;
	public Transform leftEdge;

	public float[] percentGetShip;

	//	public MapEntity[] mapEntity;
	public int numberShipMax = 20;

	private int numberShip = 0;
	private float topEdgeSize;
	private float leftEdgeSize;


	void Start ()
	{
		generateMap = this;
//		topEdgeSize = topEdge.position.y;
//		leftEdgeSize = leftEdge.position.x;
	}

	public void StartSpawn ()
	{
		StartCoroutine (SpawnCreepWhenStart ());

		StartCoroutine (SpawnMeteor (200));
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.Space))
			KillAllEnemy ();

		if (!topEdge && !leftEdge) {
			if (!GameObject.FindGameObjectWithTag ("TopEdge"))
				return;
			
			topEdge = GameObject.FindGameObjectWithTag ("TopEdge").transform;
			leftEdge = GameObject.FindGameObjectWithTag ("LeftEdge").transform;

			topEdgeSize = topEdge.position.y;
			leftEdgeSize = leftEdge.position.x;
		}

	}

	private IEnumerator SpawnCreepWhenStart ()
	{
		while (!CharacterPooling.pool)
			yield return new WaitForSeconds (.5f);
		
		for (int i = numberShip; i < numberShipMax; i++) {
			if (CharacterPooling.pool.loadDone)
				SpawnACreep ();
			else
				yield return new WaitForSeconds (.5f);
		}
	}

	private void SpawnACreep ()
	{
		AIMoveController ship = CharacterPooling.pool.GetShip ();
		if (ship)
			SetupShipByTurn (ship, GetRandomSpace (), GetShipCharacterID ());
		else
			Debug.Log ("Missing ship");
	}

	public Vector2 GetRandomSpace ()
	{
		float posX = Random.Range (-leftEdgeSize, leftEdgeSize);
		float posY = Random.Range (-topEdgeSize, topEdgeSize);

		return new Vector2 (posX, posY);
	}

	public int GetShipCharacterID ()
	{
		float percent = Random.Range (0f, 100f);

		for (int i = 1; i <= percentGetShip.Length; i++) {
			if (percent <= percentGetShip [i])
				return i - 1;
		}

		return 0;
	}

	private void SetupShipByTurn (AIMoveController ship, Vector2 pos, int idCharacter)
	{
		ship.transform.position = pos;
		ship.gameObject.SetActive (true);
		ship.GetComponentInChildren<AICharacter> ().character = new BaseCharacter (LoadCharacterXML.data.baseCharacterData [idCharacter]);
		ship.GetComponentInChildren<AIWeaponController> ().mainWeapon = new LaserWeaponEntity (LoadWeaponXml.data.GetLaserWeapon (idCharacter));
		ship.GetComponentInChildren<AIWeaponController> ().secondWeapon = new MissileWeaponEntity (LoadWeaponXml.data.GetMissileWeapon (idCharacter));
		numberShip++;
	}

	public void KillAllEnemy ()
	{
		Debug.LogError ("Kill all enemy func dont do any thing");
	}

	public IEnumerator SpawnMeteor (int number)
	{
		while (!MeteorPooling.pool)
			yield return new WaitForSeconds (.5f);

		for (int i = 0; i < number; i++) {
			if (MeteorPooling.pool.loadDone)
				GetMeteor ();
			else
				yield return new WaitForSeconds (.5f);
		}
	}

	private void GetMeteor ()
	{
		AICharacter meteor = MeteorPooling.pool.GetMeteor ();
		if (meteor) {
			meteor.RandomForMeteor (leftEdgeSize, topEdgeSize);
			meteor.gameObject.SetActive (true);
		} else
			Debug.Log ("Missing meteor");
	}
}
