using UnityEngine;
using System.Collections;

public class SlidingDoors : MonoBehaviour {

	public float moveSpeed = 1;
	public Vector3 offsetPositionL = Vector3.zero;
	public Vector3 offsetPositionR = Vector3.zero;

	public Transform leftDoor;
	public Transform rightDoor;

	public bool open = false;

	// Use this for initialization
	void Start () {
		offsetPositionL = leftDoor.position;
		offsetPositionR = rightDoor.position;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			StopCoroutine ("MoveDoorL");
			StopCoroutine ("MoveDoorR");
			Vector3 endposL = offsetPositionL + new Vector3 (0f, 0f, -1f);
			Vector3 endposR = offsetPositionR + new Vector3 (0f, 0f, 1f);
			StartCoroutine ("MoveDoorL", endposL);
			StartCoroutine ("MoveDoorR", endposR);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Player")) {
			StopCoroutine ("MoveDoorL");
			StopCoroutine ("MoveDoorR");
			StartCoroutine ("MoveDoorL", offsetPositionL);
			StartCoroutine ("MoveDoorR", offsetPositionR);
		}
	}

	IEnumerator MoveDoorL(Vector3 endpos) {
		float t = 0f;
		Vector3 startPos = leftDoor.position;
		while (t < 1f) {
			t += Time.deltaTime * moveSpeed;
			leftDoor.position = Vector3.Slerp (startPos, endpos, t);
			yield return null;
		}
	}

	IEnumerator MoveDoorR(Vector3 endpos) {
		float t = 0f;
		Vector3 startPos = rightDoor.position;
		while (t < 1f) {
			t += Time.deltaTime * moveSpeed;
			rightDoor.position = Vector3.Slerp (startPos, endpos, t);
			yield return null;
		}
	}
}
