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
        if (playerConn == null) 
        {
            GameObject player = GameObject.Instantiate(Resources.Load("Prefabs/SepticEye"), Vector3.zero, Quaternion.identity) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            playerConn = conn;
        }
        else
        { // Enemy
            GameObject player = GameObject.Instantiate(Resources.Load("Prefabs/EnemyPlayer"), Vector3.zero, Quaternion.identity) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

    }

    public void Spawn(GameObject obj)
    {
        NetworkServer.Spawn(obj);
    }

}
