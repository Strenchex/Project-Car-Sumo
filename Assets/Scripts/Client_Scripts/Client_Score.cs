using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Client_Score : NetworkBehaviour 
{
	[SyncVar(hook = "OnScoreChanged")][SerializeField]private int PlayerScore = 0;
	[SerializeField]private Text PlayerScoreText;

	void Start()
	{
		PlayerScoreText = GameObject.Find("Score").GetComponent<Text>();
	}

	void SetScoreText()
	{
		if (isLocalPlayer) 
		{
			PlayerScoreText.text = PlayerScore.ToString ();
		}
	}

	public void ScoreToAdd(int score)
	{
		if (isLocalPlayer) 
		{
			PlayerScore += score;
		}
	}

	void OnScoreChanged(int scor)
	{
		PlayerScore = scor;
		SetScoreText ();
	}
}
