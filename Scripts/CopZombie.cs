using UnityEngine;
using System.Collections;

public class CopZombie : MonoBehaviour {

	public float lookRadius = 25f;
	public float attackRadius = 3f;

	Transform target;
	NavMeshAgent agent;

	private Animator zombieAnimator;
	
	public float Health = 100f;



	void setRigidbodyState(bool state)
	{
		Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

		foreach(Rigidbody rigidbody in rigidbodies)
		{
			rigidbody.isKinematic = state;
		}

		GetComponent<Rigidbody>().isKinematic = !state;
	}

	void setColliderState(bool state)
	{
		Collider[] colliders = GetComponentsInChildren<Collider>();

		foreach(Collider collider in colliders)
		{
			collider.enabled = state;
		}

		GetComponent<Collider>().enabled = !state;
	}

	// Use this for initialization
	void Start () {

		target = PlayerManager.instance.player.transform;

		agent = GetComponent<NavMeshAgent>();
		zombieAnimator = GetComponent<Animator>();

		setRigidbodyState(true);
		setColliderState(true);
	
	}
	
	// Update is called once per frame
	void Update () {

		float distance = Vector3.Distance(target.position, transform.position);

		if(distance <= lookRadius)
		{
			FaceTarget();
			if(distance <= attackRadius)
			{
				zombieAnimator.SetBool("Attack",true);
			}
			else{
				zombieAnimator.SetBool("Attack",false);
				zombieAnimator.SetBool("Aware", true);
				agent.SetDestination(target.position);
			}	
		}
		else
		{
			zombieAnimator.SetBool("Aware",false);
		}
	
	}

	public void TakeDamage(float amount)
	{
		Health -= amount;
		if(Health <= 0f)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject,1f);
		zombieAnimator.enabled = false;
		setRigidbodyState(false);
		setColliderState(true);
	}


	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0,direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
