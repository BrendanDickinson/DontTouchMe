using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using UnityStandardAssets._2D;

public class NetManager : NetworkManager
{

    protected bool server = false;
    public NetworkConnection playerConn = null;
    private UnityEngine.Object[] playerPrefabs;
    private UnityEngine.Object prefabEnemy;

    private Dictionary<int, GameObject> playerDictionary = new Dictionary<int, GameObject>();

    // Use this for initialization
    void Start()
    {

        if (Application.platform == RuntimePlatform.WindowsPlayer
            || Application.platform == RuntimePlatform.LinuxPlayer)
        {

            string[] arguments = Environment.GetCommandLineArgs();

            foreach (string arg in arguments)
            {
                if (arg == "-server")
                {
                    server = true;
                    print("Server mode");
                }
            }
        }

        if (server)
        {
            NetworkManager.singleton.StartServer();
        }
        else
        {
            NetworkManager.singleton.StartClient();
        }

        playerPrefabs = new UnityEngine.Object[3];
        playerPrefabs[0] = Resources.Load("Prefabs/SepticEye");
        playerPrefabs[1] = Resources.Load("Prefabs/Markiplier");
        playerPrefabs[2] = Resources.Load("Prefabs/PewDiePie");

        prefabEnemy = Resources.Load("Prefabs/EnemyPlayer");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (playerConn == conn)
        {
            playerConn = null;
        }

        NetworkServer.DestroyPlayersForConnection(conn);
        Destroy(playerDictionary[conn.connectionId]);
        playerDictionary.Remove(conn.connectionId);
        
    }

    public override void OnStopServer()
    {
        playerConn = null;
    }


    /*public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
		
        Debug.Log("SERVER:: OnServerConnect: " + conn);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
		
        Debug.Log("CLIENT:: OnClientConnect: " + conn);
        //BattleController.Instance.AddPlayer(conn);
    }*/


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player;
        if (playerConn == null) 
        {
            int num = UnityEngine.Random.Range(0, 3);

            player = GameObject.Instantiate(playerPrefabs[num], Vector3.zero, Quaternion.identity) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            playerConn = conn;
        }
        else
        { // Enemy
            player = GameObject.Instantiate(prefabEnemy, Vector3.zero, Quaternion.identity) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        playerDictionary.Add(playerConn.connectionId, player);
    }

    public void Spawn(GameObject obj)
    {
        NetworkServer.Spawn(obj);
       // NetworkServer.SpawnObjects();
    }
    
	public void Respawn(NetworkIdentity ident, NetworkIdentity enemyIdent, Transform transform)
    {
		
        int num = UnityEngine.Random.Range(0, 3);

        GameObject player = GameObject.Instantiate(playerPrefabs[num], transform.position, Quaternion.identity) as GameObject;
		NetworkServer.ReplacePlayerForConnection(enemyIdent.connectionToClient, player, enemyIdent.playerControllerId);
		playerConn = enemyIdent.connectionToClient;

		player = GameObject.Instantiate(prefabEnemy, Vector3.zero, Quaternion.identity) as GameObject;
		NetworkServer.AddPlayerForConnection(ident.connectionToClient, player, ident.playerControllerId);

	}


	public void RespawnEnemy(NetworkIdentity ident) {
		GameObject player = GameObject.Instantiate(prefabEnemy, Vector3.zero, Quaternion.identity) as GameObject;
		NetworkServer.ReplacePlayerForConnection(ident.connectionToClient, player, ident.playerControllerId);
	}
	
	
}
