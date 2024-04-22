using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(UnitPool))]
public class BossManager : MonoBehaviour
{
    [SerializeField] public int BossHP = 10;
    //public float spawnsPerSecond = 0.5f;
    //[SerializeField] public bool usePooling = false;
    //[SerializeField] GameObject enemyPrefab;
    //public UnitPool pool {get; protected set;}
    //float timer = 0;
    int curHP = 0;//Should increment up to maxEnemiesSpawned
    EnemyScoreController1 EnScCtr1;
    //EnemyScoreController2 EnScCtr2;
/*
    //fade in effect while destroying/disabling it
    public float fadeDelay = 0.3f;
    public float alphaValue = 0;
    public bool destroyGameObject = false;
    SpriteRenderer spriteRenderer;
*/

    // Start is called before the first frame update
    void Start()
    {
        //pool = GetComponent<UnitPool>();
        EnScCtr1 = FindObjectOfType<EnemyScoreController1>();
        //EnScCtr2 = FindObjectOfType<EnemyScoreController2>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void HandleEnemy(GameObject other)//Deletes enemy and applies damage
    {
        curHP++;
        if (curHP < BossHP){
            Debug.Log(curHP + " / " + BossHP);
        }
        else{
            Debug.Log("Boss is dead!");
            Destroy(other);
            EnScCtr1.ScoreGame1();
        }
    }

}