using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class highscore : MonoBehaviour
{
    
    string endlessHS = "";
    //string timeHS = "";
    /*string testEndless = "player1    1" + System.Environment.NewLine +
            "player2    2" + System.Environment.NewLine +
            "player3    3" + System.Environment.NewLine +
            "player4    4" + System.Environment.NewLine +
            "player5    5" + System.Environment.NewLine +
            "player6    6" + System.Environment.NewLine +
            "player7    7" + System.Environment.NewLine +
            "player8    8" + System.Environment.NewLine +
            "player9    9" + System.Environment.NewLine +
            "player10    10";
*/
    //int timeHS = 0;
    [SerializeField] public TMP_Text txtEndlessGame;
    [SerializeField] public TMP_Text txtTimeBattle;
 
    // Start is called before the first frame update
    void Start()
    {
        //create keys in PlayerPrefs if it does not exists
        if(!PlayerPrefs.HasKey("EndlessGameHS")){
            Debug.Log("Creating PlayerPrefs.EndlessGameHS");
            SetPlayerPrefs("EndlessGameHS","0");
            txtEndlessGame.text = endlessHS;
        }
        else{
            Debug.Log("Loading PlayerPrefs.EndlessGameHS");
            endlessHS = GetPlayerPrefs("EndlessGameHS");
            txtEndlessGame.text = endlessHS;
        }
        if(!PlayerPrefs.HasKey("TimeBattleHS")){
            Debug.Log("Creating PlayerPrefs.TimeBattleHS");
            SetPlayerPrefs("TimeBattleHS", "10:00");
            txtTimeBattle.text = "10:00";
        }
        else{
            Debug.Log("Loading PlayerPrefs.TimeBattleHS");
            txtTimeBattle.text = GetPlayerPrefs("TimeBattleHS");
        }
    }

    //https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
    //macos save it at /Users/danielwu/Library/Preferences/unity.WuCompany.Project1.plist
    public void SetPlayerPrefs(string KeyName, string Value)
    {
        PlayerPrefs.SetString(KeyName, Value);
    }
    
    public string GetPlayerPrefs(string KeyName)
    {
        return PlayerPrefs.GetString(KeyName);
    }
}