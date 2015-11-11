using UnityEngine;
using System.Collections;

public class Fade_image : MonoBehaviour {

	public bool A_StartFromZero = false;
	public SpriteRenderer s_Renderer;
	public Sprite[] s_Sequence;
	public bool loop = true;
	public float fadeSpeed = 1;
	public bool destroyAtEnd = false;
	int curSeq = 0;
	int dir = -1;
	[HideInInspector]public bool seqEnded = false;
	Color curAlpha = new Color();
	// Use this for initialization
	void Start () {
		if (A_StartFromZero) {
			dir = 1;
			curAlpha.r = curAlpha.g = curAlpha.b = 1;
			curAlpha.a = 0;
			s_Renderer.color = curAlpha;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!seqEnded)
			doSequence ();
		else if(destroyAtEnd)
			this.gameObject.SetActive (false);
	}
	
	void doSequence()
	{
		curAlpha.a += fadeSpeed * Time.deltaTime * dir;
		s_Renderer.color = curAlpha;
		if (curAlpha.a <= 0 || curAlpha.a >= 1) {
			if(curAlpha.a >= 0)
				dir = -1;
			else if(curAlpha.a <= 0)
			{
				dir = 1;
				if(curSeq >= s_Sequence.Length-1 && !loop)
					seqEnded = true;
				else if(s_Sequence.Length > 1)
				{
					curSeq++;
					s_Renderer.sprite = s_Sequence[curSeq];
				}
			}
		}
	}
}
