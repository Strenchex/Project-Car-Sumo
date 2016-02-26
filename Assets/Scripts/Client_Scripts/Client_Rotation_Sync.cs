using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Client_Rotation_Sync : NetworkBehaviour 
{
	[SyncVar]private Quaternion syncRot;
	[SyncVar]private Quaternion syncRot1;
	[SyncVar]private Quaternion syncRot2;
	[SyncVar]private Quaternion syncRot3;
	[SyncVar]private Quaternion syncRot4;
	[SerializeField]
	Transform CarRot;
	[SerializeField]
	Transform FlwRot;
	[SerializeField]
	Transform FrwRot;
	[SerializeField]
	Transform RrwRot;
	[SerializeField]
	Transform RlwRot;
	[SerializeField]
	private float LerpRate = 15;
	Quaternion lastCarRot;
	Quaternion lastFlwRot;
	Quaternion lastFrwRot;
	Quaternion lastRrwRot;
	Quaternion lastRlwRot;
	float treshold = 0.1f;

	void Update()
	{
		LerpRotation();
	}

	void FixedUpdate()
	{
		TransmitRotation();
	}

	void LerpRotation()
	{
		if (!isLocalPlayer)
		{
			CarRot.rotation = Quaternion.Lerp(CarRot.rotation, syncRot, Time.deltaTime * LerpRate);
			FlwRot.rotation = Quaternion.Lerp(FlwRot.rotation, syncRot1, Time.deltaTime * LerpRate);
			FrwRot.rotation = Quaternion.Lerp(FrwRot.rotation, syncRot2, Time.deltaTime * LerpRate);
			RrwRot.rotation = Quaternion.Lerp(RrwRot.rotation, syncRot3, Time.deltaTime * LerpRate);
			RlwRot.rotation = Quaternion.Lerp(RlwRot.rotation, syncRot4, Time.deltaTime * LerpRate);
		}
	}

	[Command]
	void CmdProvideRotationToServer(Quaternion Rot, Quaternion Rot1, Quaternion Rot2, Quaternion Rot3, Quaternion Rot4)
	{
		syncRot = Rot;
		syncRot1 = Rot1;
		syncRot2 = Rot2;
		syncRot3 = Rot3;
		syncRot4 = Rot4;
	}

	[ClientCallback]
	void TransmitRotation()
	{
		if (isLocalPlayer)
		{
			if( Quaternion.Angle(CarRot.transform.rotation,lastCarRot) > treshold || Quaternion.Angle(FlwRot.transform.rotation,lastFlwRot) > treshold || Quaternion.Angle(FrwRot.transform.rotation,lastFrwRot) > treshold || Quaternion.Angle(RrwRot.transform.rotation, lastRrwRot) > treshold || Quaternion.Angle(RlwRot.transform.rotation,lastRlwRot)> treshold)
			{
				CmdProvideRotationToServer(CarRot.rotation, FlwRot.rotation, FrwRot.rotation, RrwRot.rotation, RlwRot.rotation);
				lastCarRot = CarRot.transform.rotation;
				lastFlwRot = FlwRot.transform.rotation;
				lastFrwRot = FrwRot.transform.rotation;
				lastRrwRot = RrwRot.transform.rotation;
				lastRlwRot = RlwRot.transform.rotation;
			}
		}
	}


}
