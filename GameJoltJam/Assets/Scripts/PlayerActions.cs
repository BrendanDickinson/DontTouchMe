using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

    [SerializeField]
    private Spawn spawnScript;

    private float firingLatency = 0;

	// Use this for initialization
	void Start () {
        if (spawnScript == null)
        {
            spawnScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Spawn>();
        }

        spawnScript.setPlayer(gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space) && firingLatency > 0.3f)
        {
            spawnScript.SpawnBullet(gameObject);
            firingLatency = 0;
        }
        firingLatency += Time.deltaTime;
	}

    void OnDestroy()
    {
        Debug.Log("Player was destroyed");
    }
}
