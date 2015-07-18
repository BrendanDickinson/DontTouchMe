using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody2D rigidBody;

    private float angularVelocity;

    void Start()
    {
        angularVelocity = gameObject.GetComponent<Rigidbody2D>().angularVelocity;
    }

	
	// Update is called once per frame
	void FixedUpdate () {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = rotation;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        if(angularVelocity != 0)
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;

        float input = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed * input);

        input = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * speed * input);


	
	}
}
