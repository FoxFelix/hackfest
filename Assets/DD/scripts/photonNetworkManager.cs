using UnityEngine;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class photonNetworkManager : MonoBehaviour
{
    public string version;

    private int level;
    private string roomToConnect;
    public GameObject masterSpawnPrefab;
    Transform spawnSpot;
    public GameObject CameraPrefab;
    public bool offlineMode = false;
    public bool checkPing;
    string roomName;
    public bool connecting = false;
    public float respawnTimer = 0;
    Room currentRoom;

    void Start()
    {
        //AudioListener.pause = false;
        version = "Hackfest Demo";
        roomToConnect = "demo";
        PhotonNetwork.playerName = PlayerPrefs.GetString("Username", "New Player Name");

        if (PhotonNetwork.connected)
        {
            OnJoinedLobby();
        }
        Connect();
    }

    public void playerPing()
    {
        Hashtable PlayerCustomProps = new Hashtable();
        PlayerCustomProps["Ping"] = PhotonNetwork.GetPing();
    }

    public void playOffline()
    {
        if (PhotonNetwork.offlineMode == true)
            return;

        if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();

        PhotonNetwork.offlineMode = true;
        offlineMode = true;
        connecting = true;
        OnJoinedLobby();
    }

    public void Connect()
    {
        if (offlineMode == false)
        {
            PhotonNetwork.offlineMode = false;
            connecting = true;
            PhotonNetwork.ConnectUsingSettings(version);
            PhotonNetwork.sendRate = 30;
            PhotonNetwork.sendRateOnSerialize = 30;
        }
    }

    public void ConnectToRegion(CloudRegionCode code)
    {
        if (PhotonNetwork.connected)
            PhotonNetwork.Disconnect();

        PhotonNetwork.offlineMode = false;
        connecting = true;
        PhotonNetwork.ConnectToRegion(code, version);
        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 30;
    }

    void OnDestroy()
    {
        PlayerPrefs.SetString("Username", PhotonNetwork.playerName);
    }


    void OnJoinedLobby()
    {
        print("joined lobby");
        //PhotonNetwork.JoinRandomRoom();
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, MaxPlayers = 8 };
        PhotonNetwork.JoinOrCreateRoom(roomToConnect, roomOptions, TypedLobby.Default);

    }



    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        initializePlayer();
    }

    void initializePlayer()
    {
        roomName = PhotonNetwork.room.Name;
        Debug.Log("Joined Room " + roomName);
        connecting = false;
        SpawnMyPlayer();
        if (checkPing == true)
        {
            CancelInvoke("playerPing");
            InvokeRepeating("playerPing", 0, 3);
        }
    }

    public void rejoin()
    {
        PhotonNetwork.ReJoinRoom(roomName);
    }

    public void reloadLevel0()
    {
        PhotonNetwork.LeaveRoom();
        level = 0;
    }
    
    public void loadLevel(int levelToLoad)
    {

        if (checkPing == true)
        {
            CancelInvoke("playerPing");
        }
        level = levelToLoad;
        PhotonNetwork.LeaveRoom();
    }

    void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(level);
    }


    public void SpawnMyPlayer()
    {

       PhotonNetwork.Instantiate(masterSpawnPrefab.name, Vector3.zero, Quaternion.identity, 0);
    }

}