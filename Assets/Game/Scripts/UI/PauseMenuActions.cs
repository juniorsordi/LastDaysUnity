using UnityEngine;
using System.Collections;

namespace VitalzeroGames {

	public class PauseMenuActions : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void resumeGame() {

		}

		public void showOptionsMenu() {

		}

		public void quitGame() {
			
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit ();
			#endif
		}

	}

}