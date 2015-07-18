using UnityEngine;
using System.Collections;

public class BotBasic : MonoBehaviour {

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform player;

    private float z;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


	// Update is called once per frame
	void FixedUpdate () {
        if(player != null)
            z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, z);
        GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed);
	}
}
