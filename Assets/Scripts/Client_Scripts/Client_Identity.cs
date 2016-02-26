using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Client_Identity : NetworkBehaviour {

	[SyncVar]public string playerUniqueName;
	private NetworkInstanceId playerNetID;
	private Transform MyTransform;
	[SerializeField]private Transform MyCollider;

	public override void OnStartLocalPlayer()
	{
		GetNetIdentity();
		SetIdentity();
	}
	// Use this for initialization
	void Awake () 
	{
		MyTransform = transform;
	}

	// Update is called once per frame
	void Update () 
	{
		if (MyTransform.name == "" || MyTransform.name == "Pickup_Unet(Clone)") 
		{
			SetIdentity();
		}
	}

	[Client]
	void GetNetIdentity()
	{
		playerNetID = GetComponent<NetworkIdentity> ().netId;
		CmdTellServerMyName (MakeUniqueName());
	}
	void SetIdentity()
	{
		if (!isLocalPlayer) {
			MyTransform.name = playerUniqueName;
		} else {
			MyTransform.name = MakeUniqueName();
			MyCollider.name = MakeUniqueName();
		}
	}

	string MakeUniqueName()
	{
		string UniqueName = "Player" + playerNetID.ToString ();
		return UniqueName;
	}

	[Command]
	void CmdTellServerMyName(string name)
	{
		playerUniqueName = name;
	}

}
