using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Client_Score_Collision : NetworkBehaviour
{
	[SyncVar(hook = "OnAttackerChanged")]public string MyAttacker = "";

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			MyAttacker = collision.gameObject.name;
			StartCoroutine(RemoveMyAttackerAfterSomeTime());
		}
	}


	IEnumerator RemoveMyAttackerAfterSomeTime()
	{
		yield return new WaitForSeconds (10);
		MyAttacker = "";
	}

	void OnAttackerChanged(string att)
	{
		MyAttacker = att;
	}
}
