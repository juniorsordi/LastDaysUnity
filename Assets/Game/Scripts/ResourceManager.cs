using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

namespace VitalzeroGames {

	public class ResourceManager : MonoBehaviour {

		public enum ResourceTypes { Wood, Plastic, Metal, Fuel, Water, Food };

		public int woodResource;
		public int plasticResource;
		public int metalResource;
		public int fuelResource;
		public int waterResource;
		public int foodResource;

		public GUISkin skin;
		public Texture2D[] uiIcons;

		// Use this for initialization
		void Start () {
			//uiIconWood = Resources.Load ("Game/Textures/UI/WoodUI") as Texture2D;
		}

		void OnGUI() {
			GUI.skin = skin;
			GUILayout.BeginArea (new Rect (0, 0, Screen.width, 80));
			GUILayout.BeginHorizontal (EditorStyles.helpBox);

			GUILayout.BeginVertical ();
			GUILayout.Label ("" + woodResource,  GUILayout.Width(50), GUILayout.Height(20));
			GUILayout.Label (uiIcons[0],  GUILayout.Width(50), GUILayout.Height(50));
			GUILayout.EndVertical ();

			GUILayout.BeginVertical ();
			GUILayout.Label ("" + metalResource,  GUILayout.Width(50), GUILayout.Height(20));
			GUILayout.Label (uiIcons[1],  GUILayout.Width(50), GUILayout.Height(50));
			GUILayout.EndVertical ();

			GUILayout.BeginVertical ();
			GUILayout.Label ("" + plasticResource,  GUILayout.Width(50), GUILayout.Height(20));
			GUILayout.Label (uiIcons[2],  GUILayout.Width(50), GUILayout.Height(50));
			GUILayout.EndVertical ();

			GUILayout.BeginVertical ();
			GUILayout.Label ("" + fuelResource,  GUILayout.Width(50), GUILayout.Height(20));
			GUILayout.Label (uiIcons[3],  GUILayout.Width(50), GUILayout.Height(50));
			GUILayout.EndVertical ();

			GUILayout.BeginVertical ();
			GUILayout.Label ("" + foodResource,  GUILayout.Width(50), GUILayout.Height(20));
			GUILayout.Label (uiIcons[4],  GUILayout.Width(50), GUILayout.Height(50));
			GUILayout.EndVertical ();

			GUILayout.BeginVertical ();
			GUILayout.Label ("" + waterResource,  GUILayout.Width(50), GUILayout.Height(20));
			GUILayout.Label (uiIcons[5],  GUILayout.Width(50), GUILayout.Height(50));
			GUILayout.EndVertical ();

			GUILayout.EndHorizontal ();
			GUILayout.EndArea ();
		}
		
		// Update is called once per frame
		void Update () {
			/*
			uiWoodText.text 	= "" + woodResource;
			uiPlasticText.text	= ""+plasticResource;
			uiMetalText.text	= ""+metalResource;
			uiFuelText.text		= ""+fuelResource;

			uiFoodText.text		= ""+foodResource;
			uiWaterText.text	= "" + waterResource;
			//*/
		}

		Text getComponenteResourceUI(string nome) {
			return GameObject.Find (nome).GetComponent<Text> ();
		}

		public void addWood(int q) 		{ woodResource += q; }
		public void addMetal(int q) 	{ metalResource += q; }
		public void addPlastic(int q) 	{ plasticResource += q; }
		public void addFuel(int q) 		{ fuelResource += q; }
		public void addFood(int q) 		{ foodResource += q; }
		public void addWater(int q) 	{ waterResource += q; }
	}

}
