using UnityEngine;
using System.Collections;

public class PlayerLook : MonoBehaviour {

	public float mouseSensitivity = 100f;

	public Transform playerBody;

	float xRotation = 0f;

	// Use this for initialization
	void Start () {

		Cursor.lockState = CursorLockMode.Locked;
	
	}
	
	// Update is called once per frame
	void Update () {

		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -25f, 50f);

		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

		playerBody.Rotate(Vector3.up * mouseX);

	}
}