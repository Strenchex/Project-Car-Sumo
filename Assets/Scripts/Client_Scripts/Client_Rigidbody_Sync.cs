using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Client_Rigidbody_Sync : NetworkBehaviour 
{
	[SyncVar]public Vector3 syncVelocity;
	[SyncVar]public Vector3 syncAngularVelocity;
	[SerializeField]Rigidbody CarRigidbody;

	void Update()
	{
		AssignVelocity ();
		AssignAngularVelocity ();
	}

	void FixedUpdate()
	{
		TransmitVelocity();
	}

	void AssignVelocity()
	{
		if(!isLocalPlayer)
		{
			CarRigidbody.velocity = syncVelocity;
		}
	}

	void AssignAngularVelocity()
	{
		if(!isLocalPlayer)
		{
			CarRigidbody.angularVelocity = syncAngularVelocity;
		}
	}

	[Command]
	void CmdProvideVelocityToServer(Vector3 vel, Vector3 avel)
	{
		syncVelocity = vel;
		syncAngularVelocity = avel;
	}

	[ClientCallback]
	void TransmitVelocity()
	{
		if (isLocalPlayer)
		{
			CmdProvideVelocityToServer(CarRigidbody.velocity, CarRigidbody.angularVelocity);
		}
	}
}
