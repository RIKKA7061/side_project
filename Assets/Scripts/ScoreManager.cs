using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public Text text;
	static public int score = 0;
	void FixedUpdate()
	{
		text.text = score.ToString(); 	
	}
}
