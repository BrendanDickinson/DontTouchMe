using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

    [SerializeField]
    private Spawn spawnScript;

	// Use this for initialization
	void Start () {
        if (spawnScript == null)
        {
            spawnScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Spawn>();
        }
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            spawnScript.SpawnBullet(gameObject);
        }	
	}

    void OnDestroy()
    {
        Debug.Log("Player was destroyed");
    }
}
