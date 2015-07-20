using UnityEngine;
using System.Collections;

public class RestartMenuScript : MonoBehaviour {
    private float disableCounter = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.activeSelf)
        {
            disableCounter += Time.deltaTime;
            if (disableCounter > 5)
            {
                gameObject.SetActive(false);
                disableCounter = 0;
            }
        }

	
	}
}
