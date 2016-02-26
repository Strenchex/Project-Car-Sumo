using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Trigger_Vehicle_Check : MonoBehaviour 
{
	public void OnTriggerEnter(Collider col)
	{
		if(col.tag != "Player")
		{
			return;
		}
		RpcRespawnOnClient (col.gameObject);
	}

	void RpcRespawnOnClient(GameObject PlayerCollider)
	{
		PlayerCollider.transform.GetComponentInParent<Client_Vehicle_Respawn> ().RespawnPlayer ();
	}
}
