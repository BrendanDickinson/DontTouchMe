using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerActions : NetworkBehaviour
{

    [SerializeField]
    private Spawn spawnScript;

    private float firingLatency = 0;
    private NetworkIdentity netIdentity;

    private Object prefabEye, prefabFist, prefabMustache;


    // Use this for initialization
    void Start()
    {
        if (spawnScript == null)
        {
            spawnScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Spawn>();
        }

        spawnScript.setPlayer(gameObject);
        netIdentity = GetComponent<NetworkIdentity>();

        prefabEye = Resources.Load("Prefabs/eye");
        prefabFist = Resources.Load("Prefabs/fist");
        prefabMustache = Resources.Load("Prefabs/mustache");

    }

    // Update is called once per frame
    void Update()
    {

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

        GameObject bullet;

        if (name.Contains("PewDiePie"))
        {
            bullet = Instantiate(prefabFist) as GameObject;
        }
        else if (name.Contains("Markiplier"))
        {
            bullet = Instantiate(prefabMustache) as GameObject;
        }
        else
        {
            bullet = Instantiate(prefabEye) as GameObject;
        }

        bullet.transform.position = transform.position;
        NetworkServer.Spawn(bullet);
    }


}
