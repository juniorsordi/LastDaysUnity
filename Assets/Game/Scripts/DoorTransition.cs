using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTransition : MonoBehaviour {

	public bool isPlayerNear;
	public string sceneName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlayerNear && Input.GetKeyDown(KeyCode.F)) {
			SceneManager.LoadScene (sceneName);
		}
	}

	void OnGUI() {
		if (isPlayerNear) {
			GUI.Label (new Rect (Screen.width/2-100,Screen.height/2-15, 200, 30), "Press F to exit");
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			isPlayerNear = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.CompareTag("Player")) {
			isPlayerNear = false;
		}
	}
}
