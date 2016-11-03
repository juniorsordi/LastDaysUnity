using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

namespace VitalzeroGames {

	public class GameManager : MonoBehaviour {

		public bool isPaused = false;

		public GameObject cnvPause;

		// Use this for initialization
		void Start () {

		}
		
		// Update is called once per frame
		void Update () {
			if (isPaused) {
				//Debug.Log ("Paused Game");
				Time.timeScale = 0;
				cnvPause.SetActive(true);
				Cursor.visible = true;
				GameObject.FindWithTag ("Player").GetComponent<FirstPersonController> ().enabled = false;
				GameObject.FindWithTag ("Player").SendMessage ("SetPaused", true);
				//GameObject.Find ("PauseMenu").SetActive (true);
			} else {
				//Debug.Log ("Unpaused Game");
				Time.timeScale = 1;
				cnvPause.SetActive(false);
				Cursor.visible = false;
				GameObject.FindWithTag ("Player").GetComponent<FirstPersonController> ().enabled = true;
				GameObject.FindWithTag ("Player").SendMessage ("SetPaused", false);
			}
			////
			/// 
			if (!isPaused && Input.GetKeyDown (KeyCode.Escape)) {
				isPaused = true;
			} else if (isPaused && Input.GetKeyDown (KeyCode.Escape)) {
				isPaused = false;
			}
			/// 
		}
	}

}