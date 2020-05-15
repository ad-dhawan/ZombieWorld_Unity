using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float health = 100f;

	// public Transform platformGenerator;
	// private Vector3 platformStartPoint;

	// public GameObject thePlayer;
	// private Vector3 playerStartPoint;

	// Use this for initialization
	void Start () {

		// platformStartPoint = platformGenerator.position;
		// playerStartPoint = thePlayer.transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// public void PlayerDamage(float amount)
	// {
	// 	health -= amount;
	// 	if (health <= 0f)
	// 	{
	// 		PlayerDead();
	// 	}
	// }

	// public void PlayerDead()
	// {
	// 	StartCoroutine("RestartGame");
	// }

	// public IEnumerator RestartGame()
	// {
	// 	// thePlayer.gameObject.SetActive(false);
	// 	yield return new WaitForSeconds(1f);
	// 	// thePlayer.transform.position = playerStartPoint;
	// 	// platformGenerator.position = platformStartPoint;
	// 	// thePlayer.gameObject.SetActive(true);

	// 	Application.LoadLevel(Application.loadedLevel);
	// }
}
