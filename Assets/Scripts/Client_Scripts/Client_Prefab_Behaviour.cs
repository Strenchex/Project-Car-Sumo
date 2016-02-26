using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Client_Prefab_Behaviour : NetworkBehaviour {

	[SerializeField]Camera Camera;
	[SerializeField]AudioListener audiolistener;
	[SerializeField]WheelCollider WheelFL;
	[SerializeField]WheelCollider WheelFR;
	[SerializeField]WheelCollider WheelRL;
	[SerializeField]WheelCollider WheelRR;
	[SerializeField]skidMarks WheelFLS;
	[SerializeField]skidMarks WheelFRS;
	[SerializeField]skidMarks WheelRLS;
	[SerializeField]skidMarks WheelRRS;
//	[SerializeField]GameObject CenterOfMass;
	[SerializeField]Rigidbody m_rigidbody;
	[SerializeField]car carscript;
	[SerializeField]GearBox gearbox;
	[SerializeField]GameObject[] Interior;
	[SerializeField]collisionSound colsound;

	// Use this for initialization
	public override void OnStartLocalPlayer ()
	{
		//GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().enabled = true;
		//GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().enabled = true;
		//GetComponent<UnityStandardAssets.Vehicles.Car.CarAudio>().enabled = true;
		carscript.enabled = true;
		gearbox.enabled = true;
		Camera.enabled = true;
		audiolistener.enabled = true;	
		WheelFL.enabled = true;
		WheelFR.enabled = true;
		WheelRL.enabled = true;
		WheelRR.enabled = true;
		WheelFLS.enabled = true;
		WheelFRS.enabled = true;
		WheelRLS.enabled = true;
		WheelRRS.enabled = true;
		colsound.enabled = true;
	//	CenterOfMass.SetActive (true);
		//m_rigidbody.isKinematic = false;
		GameObject.Find("Scenecamera").SetActive(false);

		foreach(GameObject inter in Interior)
		{
			inter.gameObject.SetActive(true);
		}

 
	}

}
