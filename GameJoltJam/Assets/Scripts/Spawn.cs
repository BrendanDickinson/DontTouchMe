using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : MonoBehaviour {
    [SerializeField]
    private List<GameObject> bulletPool = new List<GameObject>();

    private List<Vector2> spawnRegions = new List<Vector2>();

    [SerializeField]
    private float enemySpawnTimer = 0;

    [SerializeField]
    private int noOfBullets;

    private GameObject bullet;
    private GameObject enemy;

    [SerializeField]
    private GameObject player;


	// Use this for initialization
	void Start () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if(player != null)
            InstantiateBullets();

        spawnRegions.Add(new Vector3(-20, 10));
        spawnRegions.Add(new Vector3(-20, -10));
        spawnRegions.Add(new Vector3(20, 10));
        spawnRegions.Add(new Vector3(20, -10));
	
	}
	
	// Update is called once per frame
	void Update () {
        if (enemySpawnTimer > 2)
        {
            enemySpawnTimer = 0;
            //SpawnEnemy();
        }
        enemySpawnTimer += Time.deltaTime;
	
	}

    void InstantiateBullets()
    {

        for (int i = 0; i < noOfBullets; i++)
        {
            if (player.name.Contains("PewDiePie"))
            {
                bullet = Instantiate(Resources.Load("Prefabs/fist")) as GameObject;
            } 
            else if (player.name.Contains("Markiplier"))
            {
                bullet = Instantiate(Resources.Load("Prefabs/mustache")) as GameObject;
            }
            else if (player.name.Contains("SepticEye"))
            {
                bullet = Instantiate(Resources.Load("Prefabs/eye")) as GameObject;
            }
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public void SpawnBullet(GameObject shooter)
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if(!bulletPool[i].activeSelf)
            {
                bulletPool[i].transform.position = shooter.transform.position;
                bulletPool[i].GetComponent<BulletBehavior>().bulletLife = 10;
                bulletPool[i].SetActive(true);
                GetComponent<NetManager>().Spawn(bulletPool[i]);
                return;
            }
        }
        noOfBullets = 5;
        InstantiateBullets();
        SpawnBullet(shooter);
    }

    private void SpawnEnemy()
    {
        int region = Random.Range(0, 4);
        enemy = Instantiate(Resources.Load("Prefabs/Enemy")) as GameObject;
        enemy.transform.position = spawnRegions[region] + Random.insideUnitCircle * 0.03f;
    }

    public void setPlayer(GameObject obj)
    {
        player = obj;
        InstantiateBullets();
    }
}
