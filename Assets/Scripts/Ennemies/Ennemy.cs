using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Ennemy : MonoBehaviour
{
    public Enum_EnnemyTypes.EnnemyTypes ennemyType;

    public Transform[] meshes;

    // Stats
    private int _health = 5;
    private int _maxHealth = 5;
    private int _attack = 1;
    private bool _isAttacking = false;
    
    public bool isStunned = false;
    public float _stunDuration = 3.0f;

    // AI
    public NavMeshAgent navMesh;
    public GameObject player;

    public Vector3 walkPoint;
    public float walkPointRange;
    private bool _walkPointSet;

    public float cooldownAttack;

    public float sightRange = 20, attackRange = 5;
    

    private void Awake()
    {
        player = GameObject.Find("Player");
        navMesh = GetComponent<NavMeshAgent>();
        
        switch (ennemyType)
        {
            case Enum_EnnemyTypes.EnnemyTypes.Mushroom :
                ActivateMesh(0);
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Rabbit :
                ActivateMesh(1);
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Slime :
                ActivateMesh(2);
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Spider :
                ActivateMesh(3);
                break;
        }
    }

    private void Start()
    {
        _health = _maxHealth;
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= sightRange)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                AttackPlayer();
            }
            else ChasePlayer();
        }
        else Patrol();
    }

    private void Patrol()
    {
        if (!_walkPointSet)
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,
                transform.position.z + randomZ);
            _walkPointSet = true;
        }

        else navMesh.SetDestination(walkPoint);

        if (Vector3.Distance(transform.position, walkPoint) <= 2)
            _walkPointSet = false;
    }

    // If the ennemy see the player
    private void ChasePlayer()
    {
        if (ennemyType != Enum_EnnemyTypes.EnnemyTypes.Mushroom)
            navMesh.SetDestination(player.transform.position);
        // Mushrooms only chase if they are attacked
        else if (_health < _maxHealth)
            navMesh.SetDestination(player.transform.position);
    }

    
    // Attack Events
    private void AttackPlayer()
    {
        navMesh.SetDestination(transform.position);
        transform.LookAt(player.transform);
        
        if (!_isAttacking)
        {
            _isAttacking = true;
            player.GetComponent<PlayerHealth>().TakeDamage(_attack);
            Invoke(nameof(ResetAttack), cooldownAttack);
        }
    }

    private void ResetAttack()
    {
        _isAttacking = false;
    }

    // Set Mesh depending on an index
    private void ActivateMesh(int index)
    {
        for (int i = 0; i < meshes.Length; i++)
        {
            if (i == index)
                meshes[i].gameObject.SetActive(true);
            else
                meshes[i].gameObject.SetActive(false);
        }
    }

    // Stun Events
    public void IsStun()
    {
        if (isStunned)
        {
            print("Ouch");
            Invoke(nameof(ResetStun), _stunDuration);
        }
    }

    private void ResetStun()
    {
        isStunned = false;
    }

    // Damage Event
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(this);
        }
    }
}
