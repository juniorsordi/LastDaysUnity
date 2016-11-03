using UnityEngine;
using System.Collections;

public class HighlightObject : MonoBehaviour {

	private Renderer rend;
	private Color startColor;
	private Shader shader;
	private bool playerNear = false;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;
		shader = rend.material.shader;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if (playerNear) {
			float posx = Screen.width / 2 - 75;
			float posy = Screen.height / 2 - 15;
			GUI.Label (new Rect (posx, posy, 150, 30), "Press F to pickup");
		}
	}

	void OnMouseOver() {
		
		//rend.material.shader = Shader.Find ("Outlined/Silhouetted Bumped Diffuse");
		//rend.material.color = startColor;
		//rend.material.SetColor ("_OutlineColor", Color.red);
		//rend.material.SetFloat ("_Outline", 0.005f);
	}

	void OnMouseExit() {
		
		//rend.material.shader = shader;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			rend.material.color = Color.yellow;
			playerNear = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Player")) {
			playerNear = false;
			rend.material.color = startColor;
		}
	}
}
