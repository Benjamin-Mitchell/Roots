using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Unity.AI.Navigation;

public class Boss : Character
{
    public enum State { INTRO, VULNERABLE, FLYING };
    public State state = State.VULNERABLE;

    public enum FlyingState { TOWARDS_AIR, RAINING_PARADE };
    public FlyingState flyingState = FlyingState.TOWARDS_AIR;

    public enum VulnerableState { TOWARDS_GROUND, FIGHT_TIME };
    public VulnerableState vulnerableState = VulnerableState.TOWARDS_GROUND;

    public GameObject airPos, groundPos;

    public float flightSpeed;
    public float attackFrequency = 2.5f;
    public float attackTimer = 0.0f;
    //public bool flightMoving

    public NavMeshSurface navMeshSurface;

    public GameObject hitMarker;
    public GameObject barrel;
    public GameObject mapCentre;

    public GameObject[] enemiesToSpawn;

    private GameObject player;
    private PlayerController playerController;

    private Slider bossHealthSlider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        bossHealthSlider = GameObject.Find("GameManager").GetComponent<GameManager>().bossHealthSlider;
        bossHealthSlider.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.INTRO:
                break;
            case State.VULNERABLE:

                switch (vulnerableState)
                {
                    case VulnerableState.TOWARDS_GROUND:
                        if (Vector3.Distance(transform.position, groundPos.transform.position) > 0.3f)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, groundPos.transform.position, flightSpeed * Time.deltaTime);
                        }
                        else
                        {
                            //throw shit time
                            vulnerableState = VulnerableState.FIGHT_TIME;
                        }
                        break;
                    case VulnerableState.FIGHT_TIME:

                        attackTimer += Time.deltaTime;

                        if (attackTimer > attackFrequency)
                        {
                            int numSpawns = Random.Range(1, 3);

                            for (int i = 0; i < numSpawns; i++)
                            {
                                int rand = Random.Range(0, enemiesToSpawn.Length);
                                GameObject enemy = GameObject.Instantiate(enemiesToSpawn[rand], transform.position + transform.forward + (Vector3.up * 2.0f), Quaternion.identity);
                                EnemyLaunch temp = enemy.GetComponent<EnemyLaunch>();
                                //GameObject bar = GameObject.Instantiate(barrel, transform.position + transform.forward + Vector3.up, Quaternion.identity);
                                //BarrelLaunch temp = bar.GetComponent<BarrelLaunch>();
                                float radius = 5.0f;
                                temp.LaunchIt(RandomNavmeshLocation(radius), 50.0f);
                            }
                            attackTimer = 0.0f;
                        }

                        break;
                }
                //stand there and spawn a bunch of enemies for X seconds
                break;
            case State.FLYING:
                //Fly up and throw shit down
                switch (flyingState)
                {
                    case FlyingState.TOWARDS_AIR:
                        if (Vector3.Distance(transform.position, airPos.transform.position) > 0.3f)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, airPos.transform.position, flightSpeed * Time.deltaTime);
                        }
                        else
                        {
                            //throw shit time
                            flyingState = FlyingState.RAINING_PARADE;
                        }
                        break;
                    case FlyingState.RAINING_PARADE:

                        //fly side to side here

                        attackTimer += Time.deltaTime;
                        if (attackTimer > attackFrequency)
                        {
                            int numAttacks = Random.Range(2, 4);

                            //get some positions
                            List<GameObject> hitMarkers = new List<GameObject>();

                            float radius = 5.0f;

                            //spawn one on the player every time
                            hitMarkers.Add(GameObject.Instantiate(hitMarker, player.transform.position, Quaternion.identity));

                            for (int i = 0; i < numAttacks; i++)
                            {
                                Vector3 attackPos = RandomNavmeshLocation(radius);
                                hitMarkers.Add(GameObject.Instantiate(hitMarker, new Vector3(attackPos.x, 0.0f, attackPos.z), Quaternion.identity));
                            }

                            StartCoroutine(WaitAndChuck(0.66f, hitMarkers));

                            attackTimer = 0.0f;
                        }
                        break;
                }
                break;
            default:
                Debug.Log("How tf did we get here");
                break;
        }
    }

    public void StartTheFight()
    {
        state = State.FLYING;
        StartCoroutine(SwitchState(10.0f));
        StartCoroutine(WaitAndBake());
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += mapCentre.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }

    private void ResetValues()
    {
        flyingState = FlyingState.TOWARDS_AIR;
        vulnerableState = VulnerableState.TOWARDS_GROUND;
    }

    private IEnumerator SwitchState(float secs)
    {
        yield return new WaitForSeconds(secs);

        ResetValues();
        state = (state == State.FLYING) ? State.VULNERABLE : State.FLYING;
        StartCoroutine(SwitchState(10.0f));
    }

    private IEnumerator WaitAndBake()
    {
        yield return new WaitForSeconds(0.5f);
        navMeshSurface.BuildNavMesh();
    }

    private IEnumerator WaitAndChuck(float secs, List<GameObject> hitMarkers)
    {
        yield return new WaitForSeconds(secs);

        for (int i = 0; i < hitMarkers.Count; i++)
        {
            GameObject bar = GameObject.Instantiate(barrel, transform.position + transform.forward + Vector3.up, Quaternion.identity);
            BarrelLaunch temp = bar.GetComponent<BarrelLaunch>();
            temp.LaunchIt(hitMarkers[i].transform.position, 45.0f, playerController, Mathf.Sqrt(hitMarker.transform.localScale.x));
        }

    }



    //Character Overrides

    public override void TakeDamage(int amount)
    {
        currentHealth -= amount;
        bossHealthSlider.value = (float)currentHealth / (float)maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public override void PerformAttack()
	{
        //not used for Boss.
	}

    public override void Die()
	{
        //TriggerWin();
        Debug.Log("Holy Shit you beat the game!");
	}

    public override void Knockback(Vector3 vector3)
	{
        //not used for Boss.
	}
}
