using System;
using System.Text;
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
    public bool isAttacking = false;
    private bool _isDead = false;
    
    public bool isStunned = false;
    public float _stunDuration = 3.0f;

    // AI
    public NavMeshAgent navMesh;
    public GameObject player;
    private NavMeshPath _navMeshPath;

    public Vector3 walkPoint;
    private Vector3 _originalPos;
    public float walkPointRange;
    private bool _walkPointSet;
    private bool _detectedPlayer;

        // Attack AI
    public float cooldownAttack;
    private EnemyAttack _possibleAttackPatterns;
    
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
        _navMeshPath = new NavMeshPath();
        _originalPos = transform.position;
        _possibleAttackPatterns = transform.GetComponent<EnemyAttack>();
        
        Invoke(nameof(CheckForPlayer), 0.25f);
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= sightRange)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                AttackPlayer();
            }
            else if (navMesh.pathStatus == NavMeshPathStatus.PathComplete && _detectedPlayer && !isAttacking) 
                ChasePlayer();
            else if (!isAttacking) WalkBackToSpawn();
        }
        else
        {
            if (_detectedPlayer)
            {
                _detectedPlayer = false;
                navMesh.SetDestination(_originalPos);
                CheckForPlayer();
            }
        }
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
        // Mushrooms only chase if they are attacked
        if (ennemyType != Enum_EnnemyTypes.EnnemyTypes.Mushroom || _health < _maxHealth)
        {
            navMesh.SetDestination(player.transform.position);
            if (navMesh.pathStatus != NavMeshPathStatus.PathComplete)
            {
                _detectedPlayer = false;
            }
            else
                _detectedPlayer = true;
        }
        else if (ennemyType != Enum_EnnemyTypes.EnnemyTypes.Mushroom && _health == _maxHealth)
            Patrol();
    }
    
    // If the enemy doesn't see the player
    private void WalkBackToSpawn()
    {
        if (!_detectedPlayer) return;
        
        navMesh.SetDestination(_originalPos);
        _detectedPlayer = false;
        CheckForPlayer();
        ResetStatus();
    }
    
    // If No Path : check if there's a player nearby and available
    private void CheckForPlayer()
    {
        navMesh.SetDestination(player.transform.position);
        if (navMesh.pathStatus != NavMeshPathStatus.PathComplete)
        {
            Invoke(nameof(CheckForPlayer), 0.25f);
            navMesh.SetDestination(_originalPos);
        }
        else
        {
            _detectedPlayer = true;
        }
    }

    // Attack Events
    private void AttackPlayer()
    {
        if (!isAttacking)
        {
            navMesh.SetDestination(transform.position);
            transform.LookAt(player.transform);
            _possibleAttackPatterns.BasicAttackBegin();
        }
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
            Death();
        }
    }

    private void Death()
    {
        _attack = 0;
        _isDead = true;
        player.GetComponent<AllPlayerReferences>().allEnemies.Add(this);
        navMesh.Warp(new Vector3(100, 0, 100));
    }

    public void ResetStatus()
    {
        _health = _maxHealth;
        _attack = 1;
        if (_isDead) navMesh.Warp(_originalPos);
        _isDead = false;
    }
    
    
    // GETTER 

    public int GetAttack()
    {
        return _attack;
    }

    public void SetAttack(int attack)
    {
        _attack = attack;
    }
}
