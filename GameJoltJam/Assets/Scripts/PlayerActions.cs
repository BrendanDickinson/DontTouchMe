using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

    [SerializeField]
    private Spawn spawnScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnScript.SpawnBullet(gameObject);
        }	
	}

    void OnDestroy()
    {
        Debug.Log("Player was destroyed");
    }
}
