using UnityEngine;
using System.Collections;

namespace VitalzeroGames {

	public class PlayerManager : MonoBehaviour {

		public enum WeaponType { Melee, Pistol, SMG, Shotgun, Rifle, Sniper, RPG };

		public string playerName	= "Player1";
		public int playerHealth		= 100;
		public int playerLevel		= 1;
		public int playerExperience	= 0;
		public float playerHunger	= 0;
		public float playerThrist	= 0;
		public float playerStamina	= 100;

		public bool isDead = false;
		public bool isPaused = false;
		public WeaponType currentWeaponType;

		public Texture CurrentAmmoIcon;
		public int CurrentWeaponAmmoCount = 100;
		public int CurrentWeaponClipCount = 12;

		protected float m_LastFireTime = 0.0f;

		public GameObject ProjectilePrefab = null;			// prefab with a mesh and projectile script
		public GameObject m_ProjectileSpawnPoint = null;
		public float ProjectileScale = 1.0f;				// scale of the projectile decal
		public float ProjectileFiringRate = 0.3f;			// delay between shots fired when fire button is held down
		public float ProjectileSpread = 0.0f;				// accuracy deviation in degrees (0 = spot on)

		// work variables for the current shot being fired
		protected Vector3 m_CurrentFirePosition = Vector3.zero;				// spawn position
		protected Quaternion m_CurrentFireRotation = Quaternion.identity;	// spawn rotation
		protected int m_CurrentFireSeed;									// unique number used to generate a random spread for every projectile

		public Vector3 FirePosition = Vector3.zero;
		public AudioClip SoundFire;
		private AudioSource Audio;

		// Use this for initialization
		void Start () {
			Audio = GetComponent<AudioSource> ();
		}
		
		// Update is called once per frame
		void Update () {
			if (!isPaused) {
				/*
				if (Input.GetButtonDown ("Fire1")) {
					if (Time.time > (m_LastFireTime + 2.0f)) {
						if (CurrentWeaponClipCount > 0) {
							//Shoot ();
						}
					}
				}
				//*/
				if (Input.GetKeyDown (KeyCode.R)) {
					Reload ();
				}
			}
		}

		void PlayFireSound() {
			Audio.clip = SoundFire;
			Audio.Play();
		}

		void Reload() {
			Animation anim = GameObject.Find ("1Pistol").GetComponent<Animation> ();
			anim.Play ("StandardReload");

			//anim.Play ("ReloadEmpty");
		}

		void Shoot() {
			m_LastFireTime = Time.time;
			Debug.Log (m_LastFireTime);
			Animation anim = GameObject.Find ("1Pistol").GetComponent<Animation> ();
			anim.Play ("Fire");

			if (anim.isPlaying) {
				//return;
			}

			if (ProjectilePrefab == null)
				return;

			PlayFireSound ();

			m_CurrentFirePosition = FirePosition;
			m_CurrentFireRotation = m_ProjectileSpawnPoint.transform.rotation;

			GameObject p = (GameObject) Instantiate(ProjectilePrefab,  m_CurrentFirePosition, m_CurrentFireRotation);
			p.AddComponent<Rigidbody> ().AddForce (Vector3.forward);
			Destroy (p, 5.0f);

			CurrentWeaponClipCount--;
			/*

			m_CurrentFireSeed = Random.Range(0, 100);

			GameObject p = null;

			int v = 0;
			p = (GameObject) Instantiate(ProjectilePrefab, m_CurrentFirePosition, m_CurrentFireRotation);
			p.transform.localScale = new Vector3(ProjectileScale, ProjectileScale, ProjectileScale);	// preset defined scale
			SetSpread(m_CurrentFireSeed * (v + 1), p.transform);
			//*/
		}

		/// <summary>
		/// applies conical twist to the target transform according
		/// to a certain seed and this shooter's 'ProjectileSpread'
		/// </summary>
		public void SetSpread(int seed, Transform target)
		{

			//vp_MathUtility.SetSeed(seed);

			//vp_MasterClient.DebugMsg = "Firing shot from '" + photonView.viewID + "' with seed: " + Random.seed + ".";
			target.Rotate(0, 0, Random.Range(0, 360));									// first, rotate up to 360 degrees around z for circular spread
			target.Rotate(0, Random.Range(-ProjectileSpread, ProjectileSpread), 0);		// then rotate around y with user defined deviation

		}

		public void SetPaused(bool v) {
			isPaused = v;
		}

	}

}
