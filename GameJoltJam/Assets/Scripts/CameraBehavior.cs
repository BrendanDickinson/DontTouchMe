using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {
    [SerializeField]
    private GameObject player;
	// Use this for initialization
	void Start () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
	
	}
	
	// Update is called once per frame
	void Update () {
        if(player != null)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        if(Camera.main.orthographicSize > 3)
            Camera.main.orthographicSize -= 0.003f;
	
	}
}
