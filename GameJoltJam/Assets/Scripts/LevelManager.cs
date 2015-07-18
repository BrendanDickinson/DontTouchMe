using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(Application.loadedLevel == 0)
            GameJolt.UI.Manager.Instance.ShowSignIn();

	}

    public void LoadMainScene()
    {
        Application.LoadLevel("Field1");
    }

    public void LoadLogin()
    {
        var isSignedIn = GameJolt.API.Manager.Instance.CurrentUser;
        if (isSignedIn == null)
        {
            GameJolt.API.Manager.Instance.CurrentUser.SignOut();
        }
        Application.LoadLevel(0);
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
