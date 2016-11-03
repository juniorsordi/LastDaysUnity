using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VitalzeroGames {

	public class PlayerGUI : MonoBehaviour {

		// crosshair texture
		public Texture m_ImageCrosshair = null;

		protected PlayerManager m_Player = null;

		// gui
		public Font BigFont;
		public Font SmallFont;
		public Font MessageFont;
		public float BigFontOffset = 69;
		public float SmallFontOffset = 56;
		public Texture Background = null;
		protected Vector2 m_DrawPos = Vector2.zero;
		protected Vector2 m_DrawSize = Vector2.zero;
		protected Rect m_DrawLabelRect = new Rect(0, 0, 0, 0);
		protected Rect m_DrawShadowRect = new Rect(0, 0, 0, 0);
		protected float m_TargetHealthOffset = 0;
		protected float m_CurrentHealthOffset = 0;
		protected float m_TargetAmmoOffset = 200;
		protected float m_CurrentAmmoOffset = 200;

		// health
		public Texture2D HealthIcon = null;
		public float HealthMultiplier = 10.0f;
		public Color HealthColor = Color.white;
		public float HealthLowLevel = 2.5f;
		public Color HealthLowColor = new Color(0.75f, 0, 0, 1);
		public AudioClip HealthLowSound = null;
		public float HealthLowSoundInterval = 1.0f;
		protected float m_FormattedHealth = 0.0f;
		protected float m_NextAllowedPlayHealthLowSoundTime = 0.0f;
		protected float m_HealthWidth { get { return ((HealthStyle.CalcSize(new GUIContent(FormattedHealth)).x)); } }
		protected AudioSource m_Audio = null;

		// ammo
		public Color AmmoColor = Color.white;
		public Color AmmoLowColor = new Color(0, 0, 0, 1);

		public float ammo_posx = 100;
		public float ammo_posy = 100;

		// message
		protected string m_PickupMessage = "";
		protected Color m_MessageColor = new Color(1.0f, 1.0f, 1.0f, 2);

		// misc colors
		protected Color m_InvisibleColor = new Color(0, 0, 0, 0);
		protected Color m_TranspBlack = new Color(0, 0, 0, 0.5f);
		protected Color m_TranspWhite = new Color(1, 1, 1, 0.5f);

		// ---- styles ---

		protected static GUIStyle m_MessageStyle = null;
		public GUIStyle MessageStyle
		{
			get
			{
				if (m_MessageStyle == null)
				{
					m_MessageStyle = new GUIStyle("Label");
					m_MessageStyle.alignment = TextAnchor.MiddleCenter;
					m_MessageStyle.font = MessageFont;
				}
				return m_MessageStyle;
			}
		}

		protected GUIStyle m_HealthStyle = null;
		public GUIStyle HealthStyle
		{
			get
			{
				if (m_HealthStyle == null)
				{
					m_HealthStyle = new GUIStyle("Label");
					m_HealthStyle.font = BigFont;
					m_HealthStyle.alignment = TextAnchor.MiddleRight;
					m_HealthStyle.fontSize = 28;
					m_HealthStyle.wordWrap = false;
				}
				return m_HealthStyle;
			}
		}
		protected GUIStyle m_AmmoStyle = null;
		public GUIStyle AmmoStyle
		{
			get
			{
				if (m_AmmoStyle == null)
				{
					m_AmmoStyle = new GUIStyle("Label");
					m_AmmoStyle.font = BigFont;
					m_AmmoStyle.alignment = TextAnchor.MiddleRight;
					m_AmmoStyle.fontSize = 28;
					m_AmmoStyle.wordWrap = false;
				}
				return m_AmmoStyle;
			}
		}

		protected GUIStyle m_AmmoStyleSmall = null;
		public GUIStyle AmmoStyleSmall
		{
			get
			{
				if (m_AmmoStyleSmall == null)
				{
					m_AmmoStyleSmall = new GUIStyle("Label");
					m_AmmoStyleSmall.font = SmallFont;
					m_AmmoStyleSmall.alignment = TextAnchor.UpperLeft;
					m_AmmoStyleSmall.fontSize = 15;
					m_AmmoStyleSmall.wordWrap = false;
				}
				return m_AmmoStyleSmall;
			}
		}


		// Use this for initialization
		void Start () {
			m_Player = transform.GetComponent<PlayerManager>();
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		string FormattedHealth
		{

			get
			{
				m_FormattedHealth = (m_Player.playerHealth) * HealthMultiplier;
				if(m_FormattedHealth < 1.0f)
					m_FormattedHealth = (m_Player.isDead ? Mathf.Min(m_FormattedHealth, 0.0f) : 1.0f);
				if (m_Player.isDead && m_FormattedHealth > 0.0f)
					m_FormattedHealth = 0.0f;
				return ((int)m_FormattedHealth).ToString();
			}

		}

		void OnGUI() {

			DrawCrosshair ();
			DrawHealth ();
			DrawAmmo ();
			DrawText();
		}

		void DrawCrosshair() {
			if (m_ImageCrosshair == null)
				return;

			GUI.color = new Color(1, 1, 1, 0.8f);
			GUI.DrawTexture(new Rect((Screen.width * 0.5f) - (m_ImageCrosshair.width * 0.5f),
				(Screen.height * 0.5f) - (m_ImageCrosshair.height * 0.5f), m_ImageCrosshair.width,
				m_ImageCrosshair.height), m_ImageCrosshair);
			GUI.color = Color.red;
		}

		void DrawHealth()
		{

			DrawLabel("", new Vector2(m_CurrentHealthOffset, Screen.height - 68), new Vector2(80 + m_HealthWidth, 52), AmmoStyle, Color.white, m_TranspBlack, null);	// background
			if (HealthIcon != null)
				DrawLabel("", new Vector2(m_CurrentHealthOffset + 10, Screen.height - 58), new Vector2(32, 32), AmmoStyle, Color.white, HealthColor, HealthIcon);			// icon
			DrawLabel(FormattedHealth, new Vector2(m_CurrentHealthOffset - 18 - (45 - m_HealthWidth), Screen.height - BigFontOffset), new Vector2(110, 60), HealthStyle, HealthColor, Color.clear, null);	// value
			DrawLabel("%", new Vector2(m_CurrentHealthOffset + 50 + m_HealthWidth, Screen.height - SmallFontOffset), new Vector2(110, 60), AmmoStyleSmall, HealthColor, Color.clear, null);	// percentage mark
			GUI.color = Color.white;

		}

		/// <summary>
		/// displays a simple 'Ammo' HUD
		/// </summary>
		void DrawAmmo()
		{
			float width = 260;
			float height = 60;
			float posx = Screen.width - width;
			float posy = (Screen.height - height)-15;

			//DrawLabel ("", new Vector2 (posx, posy), new Vector2 (width, height), AmmoStyle, Color.white, m_TranspBlack, null);
			//DrawLabel("", new Vector2(posx + 10, posy + 10), new Vector2(54, 54), AmmoStyle, Color.white, AmmoColor, m_Player.CurrentAmmoIcon);	// icon
			Rect rect1 = new Rect (posx, posy, width, height);
			GUI.color = m_TranspBlack;
			GUI.DrawTexture(rect1, Background);
			GUI.color = Color.white;
			GUI.Label(rect1, "", AmmoStyle);
			GUI.DrawTexture (new Rect (posx + 5, posy, 58, 58), m_Player.CurrentAmmoIcon);
			GUI.color = AmmoColor;
			GUI.skin.label.fontSize = 40;
			GUI.skin.label.font = HealthStyle.font;
			GUI.skin.label.alignment = TextAnchor.MiddleRight;
			GUI.Label (new Rect (posx+5+68, posy, 60, 58), ""+m_Player.CurrentWeaponClipCount);
			GUI.Label (new Rect (posx+5+68+60, posy, 20, 58), "/");
			GUI.skin.label.alignment = TextAnchor.MiddleLeft;
			GUI.Label (new Rect (posx+5+68+80, posy, 120, 58), ""+m_Player.CurrentWeaponAmmoCount);
			/*
			GUILayout.BeginArea (rect1);
			GUILayout.BeginHorizontal ();
			GUILayout.Label (m_Player.CurrentAmmoIcon, GUILayout.Width (54), GUILayout.Height (54));
			GUILayout.Label (""+m_Player.CurrentWeaponClipCount, GUILayout.Width (54), GUILayout.Height (54));
			GUILayout.Label ("/", GUILayout.Width (54), GUILayout.Height (54));
			GUILayout.Label (""+m_Player.CurrentWeaponAmmoCount, GUILayout.Width (54), GUILayout.Height (54));
			GUILayout.EndHorizontal ();
			GUILayout.EndArea ();
			//*/

			/*
			if ((m_Player.currentWeaponType == vp_Weapon.Type.Thrown))
			{
				DrawLabel("", new Vector2(m_CurrentAmmoOffset + Screen.width - 93 - (AmmoStyle.CalcSize(new GUIContent(m_Player.CurrentWeaponAmmoCount.ToString())).x), Screen.height - 68), new Vector2(200, 52), AmmoStyle, AmmoColor, m_TranspBlack, null);	// background
				if (m_Player.CurrentAmmoIcon != null)
					DrawLabel("", new Vector2(m_CurrentAmmoOffset + Screen.width - 83 - (AmmoStyle.CalcSize(new GUIContent(m_Player.CurrentWeaponAmmoCount.ToString())).x), Screen.height - 58), new Vector2(32, 32), AmmoStyle, Color.white, AmmoColor, m_Player.CurrentAmmoIcon);	// icon
				DrawLabel((m_Player.CurrentWeaponAmmoCount + m_Player.CurrentWeaponClipCount).ToString(), new Vector2(m_CurrentAmmoOffset + Screen.width - 145, Screen.height - BigFontOffset), new Vector2(110, 60), AmmoStyle, AmmoColor, Color.clear, null);		// value
			}
			else
			{///
				DrawLabel("", new Vector2(m_CurrentAmmoOffset + Screen.width - 115 - (AmmoStyle.CalcSize(new GUIContent(m_Player.CurrentWeaponAmmoCount.ToString())).x), Screen.height - 68), new Vector2(200, 52), AmmoStyle, AmmoColor, m_TranspBlack, null);	// background
				if (m_Player.CurrentAmmoIcon != null)
					DrawLabel("", new Vector2(m_CurrentAmmoOffset + Screen.width - 105 - (AmmoStyle.CalcSize(new GUIContent(m_Player.CurrentWeaponAmmoCount.ToString())).x), Screen.height - 58), new Vector2(32, 32), AmmoStyle, Color.white, AmmoColor, m_Player.CurrentAmmoIcon);	// icon
				DrawLabel(m_Player.CurrentWeaponAmmoCount.ToString(), new Vector2(m_CurrentAmmoOffset + Screen.width - 177, Screen.height - BigFontOffset), new Vector2(110, 60), AmmoStyle, AmmoColor, Color.clear, null);		// value
				DrawLabel("/ " + m_Player.CurrentWeaponClipCount.ToString(), new Vector2((m_CurrentAmmoOffset + Screen.width - 60), Screen.height - SmallFontOffset), new Vector2(110, 60), AmmoStyleSmall, AmmoColor, Color.clear, null);		// total ammo count
			//}
			//*/
		}

		/// <summary>
		/// shows a message in the middle of the screen and fades it out
		/// </summary>
		void DrawText()
		{

			if (m_PickupMessage == null)
				return;

			if(m_MessageColor.a < 0.01f)
				return;

			m_MessageColor = Color.Lerp(m_MessageColor, m_InvisibleColor, Time.deltaTime * 0.4f);
			GUI.color = m_MessageColor;
			GUI.Box(new Rect(200, 150, Screen.width - 400, Screen.height - 400), m_PickupMessage, MessageStyle);
			GUI.color = Color.white;

		}

		/// <summary>
		/// a simple standard method for drawing labels with text and / or textures
		/// </summary>
		void DrawLabel(string text, Vector2 position, Vector2 scale, GUIStyle textStyle, Color textColor, Color bgColor, Texture texture)
		{

			if (texture == null)
				texture = Background;

			if (scale.x == 0)
				scale.x = textStyle.CalcSize(new GUIContent(text)).x;
			if (scale.y == 0)
				scale.y = textStyle.CalcSize(new GUIContent(text)).y;

			m_DrawLabelRect.x = m_DrawPos.x = position.x;
			m_DrawLabelRect.y = m_DrawPos.y = position.y;
			m_DrawLabelRect.width = m_DrawSize.x = scale.x;
			m_DrawLabelRect.height = m_DrawSize.y = scale.y;

			if (bgColor != Color.clear)
			{
				GUI.color = bgColor;
				if (texture != null)
					GUI.DrawTexture(m_DrawLabelRect, texture);
			}

			GUI.color = textColor;
			GUI.Label(m_DrawLabelRect, text, textStyle);
			GUI.color = Color.white;

			m_DrawPos.x += m_DrawSize.x;
			m_DrawPos.y += m_DrawSize.y;

		}


	}

}