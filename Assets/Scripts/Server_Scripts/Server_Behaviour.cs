using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Server_Behaviour : NetworkBehaviour 
{
	[SerializeField]private Canvas Hud;
	// Use this for initialization
	void Start () 
	{
		Hud = GameObject.Find ("HudCanvas").GetComponent<Canvas> ();
		Hud.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
