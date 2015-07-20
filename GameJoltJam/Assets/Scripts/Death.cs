using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Death : NetworkBehaviour {
    [SerializeField]
    private float score = 0;

    [SerializeField]
    private GameObject restartMenu;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private GameObject manager;

	private GameObject enemy;


	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("Manager");

        if (tag == "Player" && restartMenu == null)
        {
            restartMenu = manager.GetComponent<LevelManager>().restartMenu;
        }	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Triggered");
        if (gameObject.tag != "Player" && collider.tag == "bullet")
        {
            Debug.Log("BULLET HIT");
            collider.GetComponent<BulletBehavior>().Deactivate();
			CmdRespawnEnemy();
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (gameObject.tag == "Player")
        {
            if (collider.gameObject.tag == "Enemy")
            {

				enemy = collider.gameObject;
				CmdRespawn();
                int tableID = 83537; // Set it to 0 for main highscore table.
                string extraData = ""; // This will not be shown on the website. You can store any information.

                restartMenu.SetActive(true);

                manager.GetComponent<ScoreManager>().playerIsAlive = false;
                manager.GetComponent<NetManager>().playerConn = null;

                GameJolt.API.Scores.Add((int)score, score.ToString(), tableID, extraData, (bool success) => {
                     Debug.Log(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
                });
                //UnityEditor.EditorApplication.isPlaying = false;
                //Application.Quit();

            }
        }
    }


	[Command]
	private void CmdRespawn() {
		NetworkIdentity enemyIdent = enemy.GetComponent<NetworkIdentity> ();
		manager.GetComponent<NetManager>().Respawn(GetComponent<NetworkIdentity>(), enemyIdent, enemy.transform);
		Destroy(enemy);
        Destroy(gameObject);
	}

	[Command]
	private void CmdRespawnEnemy() {
		NetworkIdentity enemyIdent = gameObject.GetComponent<NetworkIdentity> ();
		manager.GetComponent<NetManager>().RespawnEnemy(enemyIdent);
		Destroy(gameObject);
	}
	
	
}
