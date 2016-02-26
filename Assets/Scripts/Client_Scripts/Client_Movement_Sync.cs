using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Client_Movement_Sync : NetworkBehaviour 
{
	[SyncVar]private Vector3 syncPos;
	//[SyncVar]private Vector3 syncPos1;
	//[SyncVar]private Vector3 syncPos2;
	//[SyncVar]private Vector3 syncPos3;
	//[SyncVar]private Vector3 syncPos4;
	[SerializeField]Transform CarTransform;
	//[SerializeField]Transform WheelFRTransform;
	//[SerializeField]Transform WheelFLTransform;
	//[SerializeField]Transform WheelRRTransform;
	//[SerializeField]Transform WheelRLTransform;
	[SerializeField]private float LerpRate = 15;
	[SerializeField]private Vector3 lastpos;
	private float treshold = 0.001f;

	void Update()
	{
		LerpPosition();
	
	}

	void FixedUpdate()
	{
		TransmitPosition();
	}

	void LerpPosition()
	{
		if(!isLocalPlayer)
		{
			CarTransform.position = Vector3.Lerp(CarTransform.position, syncPos, Time.deltaTime * LerpRate);
		}
	}

	[Command]
	void CmdProvidePositionToServer(Vector3 pos)
	{
		syncPos = pos;
	}

	[ClientCallback]
	void TransmitPosition()
	{
		if (isLocalPlayer)
		{
			if(Vector3.Distance(CarTransform.position, lastpos)>treshold)
			{
			CmdProvidePositionToServer(CarTransform.position);
			lastpos = CarTransform.position;
			}
		}
	}
}
