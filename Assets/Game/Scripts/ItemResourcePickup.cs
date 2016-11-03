using UnityEngine;
using System.Collections;

namespace VitalzeroGames {

	public class ItemResourcePickup : MonoBehaviour {

		public ResourceManager.ResourceTypes itemType;
		public string nameItem;
		public int amountResource;

		private bool canPickup = false;
		private string msgAction;

		// Use this for initialization
		void Start () {
			if (itemType == ResourceManager.ResourceTypes.Wood) 	{ msgAction = "addWood"; }
			if (itemType == ResourceManager.ResourceTypes.Plastic) 	{ msgAction = "addPlastic"; }
			if (itemType == ResourceManager.ResourceTypes.Metal) 	{ msgAction = "addMetal"; }
			if (itemType == ResourceManager.ResourceTypes.Fuel) 	{ msgAction = "addFuel"; }
			if (itemType == ResourceManager.ResourceTypes.Food) 	{ msgAction = "addFood"; }
			if (itemType == ResourceManager.ResourceTypes.Water) 	{ msgAction = "addWater"; }
		}
		
		// Update is called once per frame
		void Update () {
			if (canPickup && Input.GetKeyDown (KeyCode.G)) {
				GameObject.Find ("GameManager").SendMessage (msgAction, amountResource);
				Destroy (gameObject);
			}
		}

		void OnTriggerEnter(Collider other) {
			if(other.CompareTag("Player")) {
				Debug.Log("Player can pickup");
				canPickup = true;
			}
		}

		void OnTriggerExit(Collider other) {
			if(other.CompareTag("Player")) {
				Debug.Log("Player is going far");
				canPickup = false;
			}
		}
	}

}