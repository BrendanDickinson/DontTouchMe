using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Death : MonoBehaviour {
    [SerializeField]
    private float score = 0;

    [SerializeField]
    private GameObject restartMenu;

    [SerializeField]
    private Text scoreText;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        score += Time.deltaTime;
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Triggered");
        if (gameObject.tag != "Player" && collider.tag == "bullet")
        {
            Debug.Log("BULLET HIT");
            collider.GetComponent<BulletBehavior>().Deactivate();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (gameObject.tag == "Player")
        {
            if (collider.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
                int tableID = 83537; // Set it to 0 for main highscore table.
                string extraData = ""; // This will not be shown on the website. You can store any information.

                restartMenu.SetActive(true);
                scoreText.text = "Score: " + (int)score;

                GameJolt.API.Scores.Add((int)score, score.ToString(), tableID, extraData, (bool success) => {
                     Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
                });
                //UnityEditor.EditorApplication.isPlaying = false;
                //Application.Quit();

            }
        }
    }
}
