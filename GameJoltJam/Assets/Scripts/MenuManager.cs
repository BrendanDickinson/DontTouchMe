using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
    [SerializeField]
    private GameObject pauseMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (Time.timeScale != 0)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
	
	}

    public void PauseGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = 1;
    }

    public void ToggleMouseControl()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().mouseOn = !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().mouseOn;
    }
}
