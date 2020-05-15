using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	public float damage = 20f;
	public float range = 100f;

	public Camera fpsCam;

	public ParticleSystem muzzleFlash;
	public GameObject zombieHit;
	public float impactForce;

	private AudioSource gunFire;

	public Animator animator;
	private bool isScoped = false;

	public GameObject weaponCamera;

	public GameObject scopeOverlay;

	public Camera mainCamera;
	public float scopedFOV = 5f;
	private float normalFOV = 60f;

	// Use this for initialization
	void Start () {

		gunFire = GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}

		if(Input.GetButtonDown("Fire2"))
		{
			isScoped = !isScoped;
			animator.SetBool("Scoped", isScoped);

			scopeOverlay.SetActive(isScoped);

			
		}

		if(isScoped)
			{
				StartCoroutine(OnScoped());
			}
			else
			{
				OnUnScoped();
			}
	
	}

	void OnUnScoped()
	{
		weaponCamera.SetActive(true);
		scopeOverlay.SetActive(false);

		mainCamera.fieldOfView = normalFOV;
	}

	IEnumerator OnScoped()
	{
		yield return new WaitForSeconds(0.15f);

		scopeOverlay.SetActive(true);
		weaponCamera.SetActive(false);

		mainCamera.fieldOfView = scopedFOV;

	}

	void Shoot()
	{
		muzzleFlash.Play();
		gunFire.Play();

		RaycastHit hit;
		if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit,range))
		{
			Debug.Log(hit.transform.name);

			ZombieController target = hit.transform.GetComponent<ZombieController>();
			if(target != null)
			{
				target.TakeDamage(damage);
			}

			mutantController target2 = hit.transform.GetComponent<mutantController>();
			if(target2 != null)
			{
				target2.TakeDamage(damage);
			}


			if(hit.rigidbody != null)
			{
				hit.rigidbody.AddForce(-hit.normal * impactForce);
			}

			Object impactGO = Instantiate(zombieHit,hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(impactGO, 0.3f);
		}
	}
}
