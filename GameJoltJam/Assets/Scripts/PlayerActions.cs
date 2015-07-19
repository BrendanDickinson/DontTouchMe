using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerActions : NetworkBehaviour {

    [SerializeField]
    private Spawn spawnScript;

    private float firingLatency = 0;
	private NetworkIdentity netIdentity;
	

	// Use this for initialization
	void Start () {
        if (spawnScript == null)
        {
            spawnScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Spawn>();
        }

        spawnScript.setPlayer(gameObject);
		netIdentity = GetComponent<NetworkIdentity>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!GetComponent<NetworkIdentity>().isLocalPlayer)
        {
			return;
		}

        if (Input.GetKey(KeyCode.Space) && firingLatency > 0.3f)
        {
            //spawnScript.SpawnBullet(gameObject);
            CmdFire();
            firingLatency = 0;
        }
        firingLatency += Time.deltaTime;
	}

    void OnDestroy()
    {
        Debug.Log("Player was destroyed");
    }


    [Command]
    public void CmdFire()
    {
        GameObject bullet = Instantiate(Resources.Load("Prefabs/eye")) as GameObject;
        NetworkServer.Spawn(bullet);
       // GetComponent<NetManager>().Spawn(bullet);
    }


}
