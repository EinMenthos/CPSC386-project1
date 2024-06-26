using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//used as a reference
////https://www.youtube.com/watch?v=CE9VOZivb3I

public class SceneLoader : MonoBehaviour
{
    public string Scenebase = "";
    public string SceneName = "";
    public bool GoNextScene;
    //https://docs.unity3d.com/ScriptReference/EditorGUI.BeginDisabledGroup.html
    public Animator transition;
    public float transitionTime = 1f;


    public void LoadScene()
    {
        //globalVariables.HSUpdated flag is set during game1 and game1.
        //Should be disabled before starting another game
        if(SceneName.Contains("game")){
            GlobalVariables.HSUpdated = false;
        }
        //if game is paused, just skip animation since it will not work
        if (Time.timeScale == 1 && transition != null){
            StartCoroutine(TransitionLevel());
        }
        else{
            LoadLevel();
        }
    }

    IEnumerator TransitionLevel(){
        //Play animation
        transition.SetTrigger("Start");
        //Wait
        yield return new WaitForSeconds(transitionTime);
        //Load Scene
        if(SceneName == "quit"){
            QuitApp();
        }  
        else{
            LoadLevel();
        }
    }
    public void LoadLevel(){
        if(GoNextScene)
            {
                //SceneManager.LoadScene(SceneIndex);
                Debug.Log("Loading Next Level");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1, LoadSceneMode.Single);
                SceneManager.LoadScene("baseLv", LoadSceneMode.Additive);
            }
        else{
            //the game might be paused at start. Should start it.
            Time.timeScale = 1;
            if(Scenebase == "") {
                SceneManager.LoadScene(SceneName);
            }
            else{
                //2 scenes used to create each level.
                SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
                SceneManager.LoadScene(Scenebase, LoadSceneMode.Additive);
                Time.timeScale = 1;

            }
        }
    }
    public void QuitApp(){
        Application.Quit();
        Debug.Log("Quitting...");
    }
}
