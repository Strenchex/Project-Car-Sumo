using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Client_Vehicle_Respawn : NetworkBehaviour 
{
	public bool TouchedTrigger = false;
	private GameObject MyPlayer;
	[SerializeField]GameObject[] SpawnPoints;

	void Start()
	{
		MyPlayer = this.gameObject;
		SpawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPoint");
	}

	// Update is called once per frame
	void Update () 
	{

		if(Input.GetButtonDown("Respawn"))
			RespawnPlayer ();
	}

	void OtherPlayerScores()
	{
		if(!isLocalPlayer)
		{

		}
	}

	[ClientCallback]
	public void RespawnPlayer()
	{
		if (GetComponent<Client_Score_Collision> ().MyAttacker != "")
			CmdTellPlayerToAddScore (GetComponent<Client_Score_Collision> ().MyAttacker);
		//Debug.Log("Touched Trigger");
		CmdRespawnSvr(MyPlayer);
		//MyPlayer.GetComponent<Client_Vehicle_Control>().enabled = false;
			//TouchedTrigger = false;

	}

	[Command]
	void CmdRespawnSvr(GameObject Player)
	{
		RpcRespawnPlayer (Player);
	}

	[Command]
	void CmdTellPlayerToAddScore(string ScorePlayer)
	{
		GameObject ScorePlayerObject = GameObject.Find (ScorePlayer);
		ScorePlayerObject.GetComponent<Client_Score> ().ScoreToAdd (10);
	}

	[ClientRpc]
	void RpcRespawnPlayer(GameObject mPlayer)
	{
		mPlayer.gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		mPlayer.gameObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		mPlayer.transform.position = SpawnPoints [1].transform.position + new Vector3(0,7f,0);
		mPlayer.transform.rotation = Quaternion.identity;
		//	MyPlayer.GetComponent<Client_Vehicle_Control>().enabled = true;
	}
}
