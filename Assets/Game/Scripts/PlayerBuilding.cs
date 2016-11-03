using UnityEngine;
using System.Collections;

namespace VitalzeroGames {

	public class PlayerBuilding : MonoBehaviour {

		public GameObject prefab;
		public float someDistance;

		private GameObject player;

		// Use this for initialization
		void Start () {
			player = GameObject.FindWithTag("Player");
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		void Build() {
			Vector3 pos = transform.position + transform.forward * someDistance;
			pos.y = Terrain.activeTerrain.SampleHeight(pos);
			prefab.transform.position = pos + Vector3.up * 1;
			prefab.transform.rotation = Quaternion.LookRotation(transform.forward);
			GameObject obj = (GameObject) Instantiate(prefab, pos, player.transform.rotation);
		}
	}

}
