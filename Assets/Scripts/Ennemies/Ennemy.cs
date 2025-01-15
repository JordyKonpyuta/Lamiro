using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class Ennemy : MonoBehaviour
{
    public Enum_EnnemyTypes.EnnemyTypes ennemyType;

    public Transform[] meshes;
    
    // Audio
    public AudioResource deathSound;
    private AudioSource _audioSource;

    // Stats
    private int _health = 5;
    private int _maxHealth = 5;
    private int _attack = 1;
    public bool isAttacking = false;
    public bool isRushAttack = false;
    private bool _isDead = false;
    
    public bool isStunned = false;
    private float _stunDuration = 5.0f;
    private Vector3 _forceForRush;
    private float _Rotated;
    private bool negRot = false; 
    public bool isGiant = false;

    // AI
    public NavMeshAgent navMesh;
    public GameObject player;
    private NavMeshPath _navMeshPath;
    private Rigidbody _rigidbody;

    public Vector3 walkPoint;
    private Vector3 _originalPos;
    public float walkPointRange;
    private bool _walkPointSet;
    private bool _detectedPlayer;

        // Attack AI
    public float cooldownAttack;
    private EnemyAttack _possibleAttackPatterns;
    private float _bossRNGAttack = 0f;
    
    public float sightRange = 20, attackRange = 5;
    

    private void Awake()
    {
        player = GameObject.Find("Player"); 
        navMesh = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();

        _audioSource = gameObject.GetComponent<AudioSource>();
        int enemyTypeCount = 0;
        switch (ennemyType)
        {
            case Enum_EnnemyTypes.EnnemyTypes.Mushroom :
                ActivateMesh(0);
                navMesh.speed = 2.5f;
                _maxHealth = 5;
                enemyTypeCount = 0;
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Rabbit :
                ActivateMesh(1);
                navMesh.speed = 4.5f;
                attackRange = 8;
                _maxHealth = 50;
                transform.GetComponent<CapsuleCollider>().radius = 1.2f;
                transform.GetComponent<CapsuleCollider>().height = 5f;
                enemyTypeCount = 1;
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Slime :
                ActivateMesh(2);
                _maxHealth = 8;
                transform.GetComponent<CapsuleCollider>().radius = 0.35f;
                transform.GetComponent<CapsuleCollider>().height = 0.7f;
                enemyTypeCount = 2;
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Spider :
                ActivateMesh(3);
                attackRange = 2.5f;
                navMesh.speed = 4.5f;
                _maxHealth = 7;
                enemyTypeCount = 3;
                break;
        }
        _health = _maxHealth;

        if (!isGiant) return;
        
        _maxHealth = _maxHealth * 3 + 1;
        _health = _maxHealth - 1;
        transform.GetComponent<CapsuleCollider>().radius = transform.GetComponent<CapsuleCollider>().radius * 3;
        transform.GetComponent<CapsuleCollider>().height = transform.GetComponent<CapsuleCollider>().height * 3;
        navMesh.speed /= 2;
        transform.GetChild(enemyTypeCount).transform.localScale *= 3;
    }

    private void Start()
    {
        _health = _maxHealth;
        _navMeshPath = new NavMeshPath();
        _originalPos = transform.position;
        _possibleAttackPatterns = transform.GetComponent<EnemyAttack>();
        
        Invoke(nameof(CheckForPlayer), 1f);
        navMesh.destination = _originalPos;
    }

    private void Update()
    {
        if (isStunned)
        {
            float curRotAdd = 90 * Time.deltaTime * (negRot ? -1 : 1);
            _Rotated += curRotAdd;
            if (_Rotated >= 45 || _Rotated <= -45) negRot = !negRot;
            transform.Rotate(0f, curRotAdd, 0f);
        }
        if (_detectedPlayer && !_isDead)
        {
            if ((   _bossRNGAttack > 85f && ennemyType == Enum_EnnemyTypes.EnnemyTypes.Rabbit && !isAttacking) 
                || (Vector3.Distance(player.transform.position, transform.position) <= attackRange && !isAttacking)
                && !(ennemyType == Enum_EnnemyTypes.EnnemyTypes.Mushroom && _health == _maxHealth))
                AttackPlayer();
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
        else
        {
            navMesh.SetDestination(walkPoint);
        }

        if (Vector3.Distance(transform.position, walkPoint) <= 2)
            _walkPointSet = false;
        
    }

    // If the ennemy see the player
    private void ChasePlayer()
    {
        // Mushrooms only chase if they are attacked
        if ((ennemyType != Enum_EnnemyTypes.EnnemyTypes.Mushroom || _health < _maxHealth || isGiant) && !isAttacking)
        {
            navMesh.SetDestination(player.transform.position);
        }
        else if (ennemyType == Enum_EnnemyTypes.EnnemyTypes.Mushroom && _health == _maxHealth && !isAttacking)
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
        if (navMesh.CalculatePath(player.transform.position, _navMeshPath))
        {
            if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                navMesh.SetDestination(player.transform.position);
            }
        }

        Invoke(nameof(CheckForPlayerTrue), 0.25f);
    }

    private void CheckForPlayerTrue()
    {
        if (navMesh.pathStatus == NavMeshPathStatus.PathComplete && (Math.Abs(navMesh.destination.x - _originalPos.x) > 0.2 || Math.Abs(navMesh.destination.z - _originalPos.z) > 0.2))
        {
            if (!_detectedPlayer)
            {
                if (ennemyType == Enum_EnnemyTypes.EnnemyTypes.Rabbit)
                    InvokeRepeating(nameof(ChanceForRush), 3f, 3f);
                _detectedPlayer = true;
            }
        }
        else
        {
            if (_detectedPlayer)
            {
                _detectedPlayer = false;
                CancelInvoke(nameof(ChanceForRush));
            }
            navMesh.SetDestination(_originalPos);
        }
        
        Invoke(nameof(CheckForPlayer), 0.25f);
    }

    // Attack Events
    private void AttackPlayer()
    {
        if (!isAttacking)
        {
            navMesh.SetDestination(transform.position);
            transform.LookAt(player.transform);
            switch (ennemyType)
            {
                default:
                    _possibleAttackPatterns.BasicAttackBegin();
                    break;
                case Enum_EnnemyTypes.EnnemyTypes.Rabbit:
                    if (_bossRNGAttack > 85f)
                    {
                        StopRNGForNow();
                        _possibleAttackPatterns.BossRushBegin();
                    }
                    else
                        _possibleAttackPatterns.BasicAttackBegin();
                    break;
                case Enum_EnnemyTypes.EnnemyTypes.Spider:
                    _possibleAttackPatterns.SpiderStrikeBegins();
                    break;
            }
        }
    }

    public void ChanceForRush()
    {
        _bossRNGAttack = Random.Range(0f, 100f);
    }

    public void StopRNGForNow()
    {
        CancelInvoke(nameof(ChanceForRush));
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

    public void GetStunned()
    {
        isStunned = true;
        _Rotated = 0;
        _possibleAttackPatterns.StunAttackLoss();
        Invoke(nameof(ResetStun), _stunDuration);
        _forceForRush = transform.forward * 75;
        _forceForRush.y += 1000f;
        transform.GetComponent<Rigidbody>().AddForce(-_forceForRush, ForceMode.Impulse);
        Invoke(nameof(StopGoingBack), 1.0f);
        navMesh.isStopped = true;
        print("Get Stunned Bugs Bnnuy");
    }

    private void StopGoingBack()
    {
        transform.GetComponent<Rigidbody>().AddForce(_forceForRush, ForceMode.Impulse);
    }

    private void ResetStun()
    {
        isStunned = false;
        navMesh.isStopped = false;
        print("stop stun");
    }

    // Damage Event
    public void TakeDamage(int damage)
    {
        print("Inflicted " + damage + " damage ; HPs left : " + _health + " / " + _maxHealth);
        switch (ennemyType)
        {
            default:
                _health -= damage;
                if (_health <= 0)
                    Death();
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Rabbit:
                if (isStunned)
                {
                    _health -= damage;
                    if (_health <= 0)
                        Destroy(gameObject);
                }
                else
                {
                    GetStunned();
                }
                break;
        }
    }

    private void Death()
    {
        PlaySound();
        _attack = 0;
        _isDead = true;
        player.GetComponent<AllPlayerReferences>().allEnemies.Add(this);
        navMesh.Warp(new Vector3(0, -50, 0));
        _rigidbody.useGravity = false;
        navMesh.SetDestination(new Vector3(0, -50, 0));
    }

    // Left the Room
    public void ResetStatus()
    {
        _health = _maxHealth;
        _attack = 1;
        if (_isDead) navMesh.Warp(_originalPos);
        _isDead = false;
        _rigidbody.useGravity = true;
        StopRNGForNow(); 
    }
    
    // Sound Player
    private void PlaySound()
    {
        _audioSource.resource = deathSound;
        _audioSource.Play();
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

    private void OnTriggerEnter(Collider other)
    {
        if (isRushAttack && other.gameObject.layer == 7)
        {
            if (!isStunned)
                GetStunned();
        }
    }
}
