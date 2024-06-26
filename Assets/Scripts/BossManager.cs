using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(UnitPool))]
public class BossManager : MonoBehaviour
{
    public MobManager mm;
    public Rigidbody2D Ball;

    [SerializeField] public float BossHP = 100;
    [SerializeField] private float curHP = 0; //Should decrement till zero
    [SerializeField] RectTransform healthbarFill;
    [SerializeField] GameObject BossGO;
     public SpriteRenderer spriteRenderer;
    public Sprite b2;
    public Sprite b3;
    [SerializeField] private Animator fearAnimator;
    
    public UnitPool pool;
    [SerializeField] GameObject enemyPrefab;
    EnemyScoreController1 EnScCtr1;

    public int limitCounter = 0;
    public int limitBreak = 5;

    [SerializeField] AudioSource BossKillSound;
    [SerializeField] AudioSource debuffSound;
    [SerializeField] AudioSource extraB;    
    [SerializeField] AudioSource BossHit;    

    // Start is called before the first frame update
    void Start()
    {
        pool = GetComponent<UnitPool>();
        EnScCtr1 = FindObjectOfType<EnemyScoreController1>();
        curHP = BossHP;
    }

    // Update is called once per frame
    void Update()
    {
        float healthRatio = curHP/BossHP;
        healthbarFill.localScale = new Vector3(healthRatio, 1f, 1f);

        if (!Ball){
            // Try to find another jumpingBody in the scene
            GameObject[] jumpingBodies = GameObject.FindGameObjectsWithTag("ball");
            if (jumpingBodies.Length > 0)
            {
                Ball = jumpingBodies[0].GetComponent<Rigidbody2D>();
            }
        }
    }

    public void HandleEnemy(GameObject other)//Deletes enemy and applies damage
    {
        curHP--;
        BossHit.Play();
        float healthRatio = curHP/BossHP;
        healthbarFill.localScale = new Vector3(healthRatio, 1f, 1f);

        if (curHP > 0){
            Debug.Log(curHP + " / " + BossHP);
            limitCounter++;
            if (limitCounter % limitBreak == 0 && limitCounter > 0){
                extraB.Play();
                Debug.Log("Add another ball: " + limitCounter + "hits");
                GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
                GameObject newBall = Instantiate(balls[0], transform);
                newBall.transform.position = balls[0].transform.position;
                newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(balls[0].GetComponent<Rigidbody2D>().velocity.x * Random.Range(-0.5f, 0.5f), balls[0].GetComponent<Rigidbody2D>().velocity.y * Random.Range(-1f, 1f));
            }
            if(curHP <= BossHP * 1/3){
                Debug.Log("Boss still has less than 1/3!");
                spriteRenderer.sprite = b3;
                fearEffect();
                debuffSound.Play();
                StartCoroutine(MobEffect(2));
            }
            else if(curHP <= BossHP * 2/3){
                Debug.Log("Boss still has less than 2/3!");
                spriteRenderer.sprite = b2;
                fearEffect();
                debuffSound.Play();
                //Debug.Log("Y: " + Ball.transform.position.y);
                StartCoroutine(MobEffect(1));
            }
        }
        else{
            Debug.Log("Boss is dead!");
            BossKillSound.Play();
            Destroy(other);
            EnScCtr1.ScoreGame1();
        }
        
    }
    IEnumerator MobEffect(int k){
        //wait to balls move away b4 plaing barrier
        yield return new WaitForSeconds(1);
        //Debug.Log("Looking for enemies...");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<string> enemiesPos = new List<string>();
        foreach (GameObject enemy in enemies){
            enemiesPos.Add(enemy.transform.position.x.ToString() + "," + enemy.transform.position.y.ToString());
        } 
        Debug.Log("Building barrier!");
        for (int j = 1; j <= 4; j++){
            if (!enemiesPos.Contains("-2," + j)){
                mm.SpawnMobs(-2, j);
            }
            if (!enemiesPos.Contains("2," + j)){
                mm.SpawnMobs(2, j);
            }
        }
        for (int j = -2; j <= 2; j++){
            if (!enemiesPos.Contains(j + ",0")){
                mm.SpawnMobs(j, 0);
            }
        }
        if (k == 2){
            for (int j = 0; j <= 4; j++){
                if (!enemiesPos.Contains("-3," + j)){
                    mm.SpawnMobs(-3, j);
                }
                if (!enemiesPos.Contains("3," + j)){
                    mm.SpawnMobs(3, j);
                }
            }
            for (int j = -3; j <= 3; j++){
                if (!enemiesPos.Contains(j + ",-1")){
                    mm.SpawnMobs(j, -1);
                }
            }
        }
    }

    private void fearEffect(){
        fearAnimator.Play("fearCircle",0,0.0f);
        Debug.Log("Fear Effect");
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
        foreach (GameObject ball in balls)
        {
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (ball.transform.position - transform.position).normalized;
                rb.AddForce(direction * 6f, ForceMode2D.Impulse);
            }
        }
    }
}
