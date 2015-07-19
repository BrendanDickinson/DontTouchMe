using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public float score = 0;
    public bool playerIsAlive = true;

    [SerializeField]
    private Text finalScoreText;

    [SerializeField]
    private Text inGameScoreText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (playerIsAlive)
        {
            score += Time.deltaTime;
            inGameScoreText.text = "Score: " + (int)score;
        }
        else
        {
            if(finalScoreText.text == "")
                finalScoreText.text = "Final Score: " + (int)score;
        }
	}
}
