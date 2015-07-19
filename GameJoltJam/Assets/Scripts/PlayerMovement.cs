using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody2D rigidBody;

    private float angularVelocity;

    public bool mouseOn = true;

    private float input;
    private float rotationSpeed = 5f;
	private NetworkIdentity netIdentity;

    void Start()
    {
        angularVelocity = gameObject.GetComponent<Rigidbody2D>().angularVelocity;
		netIdentity = GetComponent<NetworkIdentity>();
    }

	
	// Update is called once per frame
	void FixedUpdate () {

		if (!netIdentity.isLocalPlayer) {
			return;
		}


        if (mouseOn)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Quaternion rotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);


            input = Input.GetAxis("Horizontal");
            GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * speed * input);

            transform.rotation = rotation;
        }
        else
        {
            input = Input.GetAxis("Horizontal");
            transform.Rotate(new Vector3(0,0, -input * rotationSpeed));
        }

        input = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed * input);

        if(angularVelocity != 0)
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;

        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);



	
	}
}
