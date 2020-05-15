using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	
	public float health = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void PlayerDamage(float amount)
	{
		health -= amount;
		if (health <= 0f)
		{
			PlayerDead();
		}
	}

	public void PlayerDead()
	{
		StartCoroutine("RestartGame");
	}

	public IEnumerator RestartGame()
	{
		yield return new WaitForSeconds(1f);

		Application.LoadLevel(Application.loadedLevel);
	}

}
