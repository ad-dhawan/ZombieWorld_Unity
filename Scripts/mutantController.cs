using UnityEngine;
using System.Collections;

public class mutantController : MonoBehaviour {

	public float lookRadius = 10f;
	public float attackRadius = 5f;

	Transform target;
	NavMeshAgent agent;

	private Animator mutantAnimator;

	public float health = 100f;



	void setRigidbodyState(bool state)
	{
		Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

		foreach(Rigidbody rigidbody in rigidbodies)
		{
			rigidbody.isKinematic = state;
		}

		// GetComponent<Rigidbody>().isKinematic = !state;
	}

	void setColliderState(bool state)
	{
		Collider[] colliders = GetComponentsInChildren<Collider>();

		foreach(Collider collider in colliders)
		{
			collider.enabled = state;
		}

		// GetComponent<Collider>().enabled = !state;
	}



	// Use this for initialization
	void Start () {
		target = PlayerManager.instance.player.transform;

		agent = GetComponent<NavMeshAgent>();
		mutantAnimator = GetComponent<Animator>();

		setRigidbodyState(true);
		setColliderState(true);

	
	}
	
	// Update is called once per frame
	void Update () {

		float distance = Vector3.Distance(target.position, transform.position);

		if (distance <= lookRadius)
		{
			FaceTarget();

			if(distance <= attackRadius)
			{
				mutantAnimator.SetBool("attack", true);
			}
			else{
				mutantAnimator.SetBool("attack", false);
				mutantAnimator.SetBool("aware", true);
				agent.SetDestination(target.position);
			}
		}
		else
		{
			mutantAnimator.SetBool("aware",false);
		}

		
	
	}

	public void TakeDamage(float amount)
	{
		if(mutantAnimator.GetBool("aware") == false)
		{
			return;
		}
		else{
		health -= amount;
			if (health <= 0f)
			{
				Die();
			}
		}
	}

	void Die()
	{
		StartCoroutine(deathConfirmed());
		mutantAnimator.SetTrigger("dead");
		
	}

	IEnumerator deathConfirmed()
	{
		yield return new WaitForSeconds(4f);

		Destroy(gameObject);
	}


	void FaceTarget() 
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}