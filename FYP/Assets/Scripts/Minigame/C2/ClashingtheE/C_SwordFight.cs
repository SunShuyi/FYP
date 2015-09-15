using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C_SwordFight : MonoBehaviour {

	C_Input theInput = null;
	
	public float E_Strength;
	public string nextScene = "";
	public GameObject EGuy;
	public Sprite[] EGuyArr = new Sprite [3];
	private Image E_Image;
	private int E_Count;

	public float O_Strength;
	public GameObject Odysseus;
	public Sprite[] OdysseusArr = new Sprite [3];
	private Image O_Image;
	private int O_Count;

	private bool playGame;
	private bool instructions;

	public Slider StrengthBar;
	float StrengthGauge;

	public GameObject theInstrucPage;

	// Use this for initialization
	void Start () {
		theInput = C_Input.getInstance;

		playGame = false;
		instructions = true;

		O_Count = 1;
		E_Count = 1;

		O_Image = Odysseus.GetComponent<Image> ();
		E_Image = EGuy.GetComponent<Image> ();

		O_Image.sprite = OdysseusArr [O_Count];
		E_Image.sprite = EGuyArr [E_Count];

		StrengthGauge = StrengthBar.maxValue / 2;
	}
	
	// Update is called once per frame
	void Update () {

		theInput.InputUpdate ();

		if (!instructions) 
		{ StrengthGauge -= E_Strength * Time.deltaTime; }

		StrengthBar.value = StrengthGauge;
		
		ChangeFace ();
		Exit ();

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
		if(Input.GetKeyDown(KeyCode.Space)){
			if(instructions)
			{ instructions = false; }
			else
			{ StrengthGauge += O_Strength;}
		}

#else
		if(theInput.I_Down) {
			if(instructions)
			{ instructions = false; }
			else
			{ StrengthGauge += O_Strength; }
		}

#endif

	}

	void ChangeFace () {
		if (StrengthBar.value > 35 && StrengthBar.value < 65) {
			O_Count = 1;
			E_Count = 1;
		}

		if (StrengthBar.value >= 65) {
			O_Count = 2;
			E_Count = 0;
		}

		if (StrengthBar.value <= 35) {
			O_Count = 0;
			E_Count = 2;
		}

		O_Image.sprite = OdysseusArr [O_Count];
		E_Image.sprite = EGuyArr [E_Count];
	}

	void Exit ()
	{
		if (StrengthBar.value == StrengthBar.minValue) 
		{ Application.LoadLevel("SwordClash"); }
		
		if (StrengthBar.value == StrengthBar.maxValue) 
		{ Application.LoadLevel(nextScene); }
	}
}
