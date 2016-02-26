using UnityEngine;
using System.Collections;

public class Client_Vehicle_Control : MonoBehaviour
{
    public float maxTorque = 50f;

    public Transform centerOfMass;

    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] tireMeshes = new Transform[4];

    private Rigidbody m_rigidBody;
	private bool FirstpersonCameraOn = false; 
	[SerializeField]GameObject CameraPlayer;
	[SerializeField]Transform CameraFirstPersonTransform;
	[SerializeField]Transform CameraThirdPersonTransform;
	[SerializeField]Transform CameraFirstPersonLookBackTransform;
	private float steer;
	private float accelerate;


    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.centerOfMass = centerOfMass.localPosition;
    }

    void Update()
    {
        UpdateMeshesPositions();
		//CameraAnimation ();
    }

    void FixedUpdate()
    {
        steer = Input.GetAxis("Horizontal");
        accelerate = Input.GetAxis("Vertical");

        float finalAngle = steer * 30f;
        wheelColliders[0].steerAngle = finalAngle;
        wheelColliders[1].steerAngle = finalAngle;

        for(int i = 0; i < 4; i++)
        {
            wheelColliders[i].motorTorque = accelerate * maxTorque;
        }
    }

    void UpdateMeshesPositions()
    {
        for(int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);

            tireMeshes[i].position = pos;
            tireMeshes[i].rotation = quat;
        }
    }
	/*
	void CameraToggle()
	{
		if(Input.GetButtonDown("Camera Toggle") && FirstpersonCameraOn == false)
		{
			CameraPlayer.transform.position = CameraFirstPersonTransform.position;
			FirstpersonCameraOn = true;
		}
		else if(Input.GetButtonDown("Camera Toggle") && FirstpersonCameraOn == true)
		{
			CameraPlayer.transform.position = CameraThirdPersonTransform.position;
			FirstpersonCameraOn = false;
		}
	}

	void CameraAnimation()
	{
		if(FirstpersonCameraOn == true  && accelerate <= 0)
		{
			CameraPlayer.transform.position = CameraFirstPersonLookBackTransform.transform.position;
			CameraPlayer.transform.rotation = CameraFirstPersonLookBackTransform.transform.rotation;
		}
		if(FirstpersonCameraOn == true && accelerate >= 0)
		{
			CameraPlayer.transform.position = CameraFirstPersonTransform.transform.position;
			CameraPlayer.transform.rotation = CameraFirstPersonTransform.transform.rotation;
		}
	}
	*/
}

















