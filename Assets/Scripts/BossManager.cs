using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(UnitPool))]
public class BossManager : MonoBehaviour
{
    //[SerializeField] public int maxEnemiesSpawned = 10;
    EnemyManager em;
    public Rigidbody2D Ball;

    [SerializeField] public int BossHP = 10;
    //public float spawnsPerSecond = 0.5f;
    //[SerializeField] public bool usePooling = false;
    [SerializeField] GameObject BossGO;
    public UnitPool pool;
    [SerializeField] GameObject enemyPrefab;

    //float timer = 0;
    int curHP = 0;//Should increment up to maxEnemiesSpawned
    //int curSpawned = 1;//Should increment up to maxEnemiesSpawned

    EnemyScoreController1 EnScCtr1;
    //EnemyScoreController2 EnScCtr2;

    public SpriteRenderer spriteRenderer;
    public Sprite b2;
    public Sprite b3;
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
        em = FindObjectOfType<EnemyManager>();
        pool = GetComponent<UnitPool>();
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
        if(curHP > BossHP * 2/3){
            Debug.Log("Boss lose 2/3!");
            spriteRenderer.sprite = b3;
            StartCoroutine(MobWait(-1));

        }
        else if(curHP > BossHP * 1/3){
            Debug.Log("Boss lose 1/3!");
            spriteRenderer.sprite = b2;
            Debug.Log("Y: " + Ball.transform.position.y);
            StartCoroutine(MobWait(0));
            
        }
    }
    IEnumerator MobWait(int j){
        //need to create an ending condition. It still runs when the game ends.
        while(Ball.transform.position.y > j-1){
            Debug.Log("waiting to create barrier");
            yield return null;
        }
        for (int i = -8; i <= 8; i++){
            em.SpawnMobs(i, j);
        }
    }


}
