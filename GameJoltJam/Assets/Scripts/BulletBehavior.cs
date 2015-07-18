using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject player;

    private Vector3 direction;

    public float bulletLife = 10;

	// Use this for initialization
	void OnEnable() {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = player.transform.up;
        transform.rotation = player.transform.rotation;
        GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed);       	
	}

    public void Deactivate()
    {
        //gameObject.SetActive(false);
    }

    void Update()
    {
        if (bulletLife > 0)
            bulletLife -= Time.deltaTime;
        else
            Deactivate();
        
    }
}
