using UnityEngine;
using System.Collections;

public class GenerateForDeathMacth : MonoBehaviour
{
	[SerializeField] private GameObject SpaceShip;
	public static GenerateForDeathMacth generate;

	public MapEntity[] mapEntity;

	public Transform[] spawnPositions;

	public int level;
	public int numberShip = 0;
	public MapState state;

	void Start ()
	{
		generate = this;
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("SpawnPoint");
		spawnPositions = new Transform[gos.Length];
		for (int i = 0; i < gos.Length; i++) {
			spawnPositions [i] = gos [i].transform;
		}
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.Space))
			KillAllEnemy ();


		SpawnByTurn ();
	}

	private void SpawnByTurn ()
	{
		switch (state) {
		case MapState.WaitingToSpawn:
			StartCoroutine (WaitToSpawn (1f));
			break;
		case MapState.StartSpawn:
			StartCoroutine (SpawnCreepByTurn (.5f));
			break;
		case MapState.SpawnDone:
		case MapState.WaitForNextSpawn:
			CheckForNewLevel ();
			break;
		case MapState.Spawning:
		case MapState.None:
		default:
			break;
		}
	}

	private void CheckForNewLevel ()
	{
		if (numberShip != 0)
			return;

		level++;
		state = MapState.WaitingToSpawn;
	}

	private IEnumerator SpawnCreepByTurn (float timePerShip)
	{

		state = MapState.Spawning;
		for (int i = 0; i < mapEntity [level].enemyID.Length; i++) {
			for (int j = 0; j < mapEntity [level].enemyNumber [i]; j++) {
				int rad = Random.Range (0, spawnPositions.Length);
				AIMoveController ship = CharacterPooling.pool.GetShip ();
				SetupShipByTurn (ship, spawnPositions [rad], mapEntity [level].enemyID [i]);
				yield return new WaitForSeconds (timePerShip);
			}
		}
		state = MapState.SpawnDone;
	}

	private void SetupShipByTurn (AIMoveController ship, Transform pos, int id)
	{
		ship.transform.position = pos.position;
		ship.transform.rotation = pos.rotation;
		ship.gameObject.SetActive (true);
		ship.GetComponentInChildren<AICharacter> ().character = new BaseCharacter (LoadCharacterXML.data.baseCharacterData [id]);
		ship.GetComponentInChildren<AIWeaponController> ().mainWeapon = new LaserWeaponEntity (LoadWeaponXml.data.GetLaserWeapon (id));
		ship.GetComponentInChildren<AIWeaponController> ().secondWeapon = new MissileWeaponEntity (LoadWeaponXml.data.GetMissileWeapon (id));
		numberShip++;
	}

	private IEnumerator WaitToSpawn (float timer)
	{
		state = MapState.None;
		yield return new WaitForSeconds (timer);
		state = MapState.StartSpawn;
	}

	public void KillAllEnemy ()
	{
//		numberShip = 0;
		Debug.LogError ("Kill all enemy func dont do any thing");
	}
}
