using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager_Custom : NetworkManager 
{
	public Canvas MainMenu;
	public Canvas JoinMenu;
	public Canvas SettingsMenu;
	[SerializeField]Camera PlayerCamera;
	[SerializeField]private Canvas PauseMenu;
	[SerializeField]private bool PauseMenuOpened = false;
	[SerializeField]private bool CanPauseMenu = false;
	[SerializeField]private bool JoinMenuOpened = false;
	[SerializeField]private bool SettingsMenuOpened = false;
	public string PlayerName = "";

	void Start()
	{
		MainMenu = MainMenu.GetComponent<Canvas> ();
		JoinMenu = JoinMenu.GetComponent<Canvas> ();
		SettingsMenu = SettingsMenu.GetComponent<Canvas> ();
		GetPlayerName ();
	}

	void Update()
	{
		EscapeButtonSettings ();
	}

	void EscapeButtonSettings()
	{
		if(Input.GetButtonDown("PauseMenu") && PauseMenuOpened == false && CanPauseMenu == true)
		{
			PauseMenuOpened = true;
			Cursor.lockState = CursorLockMode.None;
			PauseMenuOpen();
		}
		else if(Input.GetButtonDown("PauseMenu") && PauseMenuOpened == true  && CanPauseMenu == true)
		{
			PauseMenuOpened = false;
			Cursor.lockState = CursorLockMode.Locked;
			PauseMenuClose();
		}
		else if(Input.GetButtonDown("PauseMenu") && CanPauseMenu == false && JoinMenuOpened == true)
		{
			JoinMenuClose();
		}
		else if(Input.GetButtonDown("PauseMenu") && CanPauseMenu == false && SettingsMenuOpened == true)
		{
			SettingsMenuClose();
		}
	}

	void PauseMenuOpen()
	{
		PauseMenu.enabled = true;
	}

	void PauseMenuClose()
	{
		PauseMenu.enabled = false;
	}

	public void JoinMenuOpen()
	{
		MainMenu.enabled = false;
		JoinMenu.enabled = true;
		JoinMenuOpened = true;
	}

	void JoinMenuClose()
	{
		MainMenu.enabled = true;
		JoinMenu.enabled = false;
		JoinMenuOpened = false;
	}

	public void SettingsMenuOpen()
	{
		MainMenu.enabled = false;
		SettingsMenu.enabled = true;
		SettingsMenuOpened = true;
	}
	
	void SettingsMenuClose()
	{
		MainMenu.enabled = true;
		SettingsMenu.enabled = false;
		SettingsMenuOpened = false;
	}

	public void StartupServer()
	{
		SetPort();
		NetworkManager.singleton.StartServer();
	}

	public void JoinGame()
	{
		SetIPAddress();
		SetPort();
		NetworkManager.singleton.StartClient();
	}

	void SetIPAddress()
	{
		string ipAddress = GameObject.Find("IpAdressInputField").transform.FindChild("Text").GetComponent<Text>().text;
		NetworkManager.singleton.networkAddress = ipAddress;
	}

	void GetPlayerName()
	{
		PlayerName = PlayerPrefs.GetString ("Player Name");

		if(PlayerName == "")
		{
			PlayerName = "Player";
		}
	}

	public void SetPlayerName()
	{
		PlayerName = GameObject.Find("PlayerInputField").transform.FindChild("Text").GetComponent<Text>().text;

		PlayerPrefs.SetString ("Player Name", PlayerName);
	}

	void SetPort()
	{
		NetworkManager.singleton.networkPort = 7777;
	}

	void OnLevelWasLoaded (int level)
	{
		if(level == 0)
		{
            //SetupMenuSceneButtons();
            StartCoroutine(SetupMenuSceneButtons());
			CanPauseMenu = false;
			
			MainMenu = GameObject.Find("MainCanvas").GetComponent<Canvas>();
			JoinMenu = GameObject.Find("JoinCanvas").GetComponent<Canvas>();
		}

		else
		{
			PauseMenu = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
			//GameObject.Find("Scenecamera").GetComponent<>();
			StartCoroutine(SetupOtherSceneButtons());
			PauseMenu.enabled = false;
			CanPauseMenu =true;
		}
	}

	IEnumerator SetupMenuSceneButtons()
	{
        yield return new WaitForSeconds(0.3f);
		GameObject.Find("StartServerButton").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find("StartServerButton").GetComponent<Button>().onClick.AddListener(StartupServer);

		GameObject.Find("JoinServerButton").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find("JoinServerButton").GetComponent<Button>().onClick.AddListener(JoinMenuOpen);

		GameObject.Find("QuitGameButton").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find("QuitGameButton").GetComponent<Button>().onClick.AddListener(QuitGame);

		GameObject.Find("JoinButton").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find("JoinButton").GetComponent<Button>().onClick.AddListener(JoinGame);

		GameObject.Find("PlayerSettingsButton").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find("PlayerSettingsButton").GetComponent<Button>().onClick.AddListener(SettingsMenuOpen);

		GameObject.Find("Back").GetComponent<Button>().onClick.RemoveAllListeners();
		GameObject.Find ("Back").GetComponent<Button> ().onClick.AddListener (JoinMenuClose);
	}

	IEnumerator SetupOtherSceneButtons()
	{
		yield return new WaitForSeconds(0.3f);
		GameObject.Find ("ResumeButton").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ResumeButton").GetComponent<Button> ().onClick.AddListener (PauseMenuClose);

		GameObject.Find ("DisconnectButton").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("DisconnectButton").GetComponent<Button> ().onClick.AddListener (NetworkManager.singleton.StopHost);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}
