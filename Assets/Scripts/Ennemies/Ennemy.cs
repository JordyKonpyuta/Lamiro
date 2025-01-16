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
    public AudioResource mushroomSound;
    private AudioSource _audioSource;

    // VFX
    public GameObject vfx;

    // Stats
    private int _health = 5;
    private int _maxHealth = 5;
    private int _attack = 1;
    public bool isAttacking = false;
    public bool isRushAttack = false;
    
    public bool isStunned = false;
    private float _stunDuration = 8.0f;
    private Vector3 _forceForRush;
    private float _Rotated;
    private bool negRot = false; 
    public bool isGiant = false;
    
    // Death
    private bool _isDead = false;
    public GameObject screwDrop;
    public GameObject jetpackDrop;

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
                _maxHealth = 3;
                enemyTypeCount = 0;
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Rabbit :
                ActivateMesh(1);
                navMesh.speed = 4.5f;
                attackRange = 8;
                _maxHealth = 30;
                transform.GetComponent<CapsuleCollider>().radius = 1.2f;
                transform.GetComponent<CapsuleCollider>().height = 5f;
                enemyTypeCount = 1;
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Slime :
                ActivateMesh(2);
                _maxHealth = 5;
                transform.GetComponent<CapsuleCollider>().radius = 0.35f;
                transform.GetComponent<CapsuleCollider>().height = 0.7f;
                enemyTypeCount = 2;
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Spider :
                ActivateMesh(3);
                attackRange = 2.5f;
                navMesh.speed = 4.5f;
                _maxHealth = 2;
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
        if (_isDead)
        {
            float curRotAdd = Random.Range(80, 100) * Time.deltaTime;
            transform.Rotate(0f, curRotAdd, 0f);
        }

        if (isStunned)
        {
            float curRotAdd = 90 * Time.deltaTime * (negRot ? -1 : 1);
            _Rotated += curRotAdd;
            if (_Rotated >= 45 || _Rotated <= -45) negRot = !negRot;
            transform.Rotate(0f, curRotAdd, 0f);
            return;
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
        CancelInvoke(nameof(ResetStun));
        isStunned = true;
        _Rotated = 0;
        _possibleAttackPatterns.StunAttackLoss();
        Invoke(nameof(ResetStun), _stunDuration);
        transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        //Invoke(nameof(GoBackwards), 0.2f);
        transform.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * -35, ForceMode.Impulse);
        Invoke(nameof(StopGoingBack), 1.0f);
        navMesh.isStopped = true;
        print("Get Stunned Bugs Bnnuy");
    }

    private void GoBackwards()
    {
        transform.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * -35, ForceMode.Impulse);
    }

    private void StopGoingBack()
    {
        transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }

    private void ResetStun()
    {
        isStunned = false;
        navMesh.isStopped = false;
        isRushAttack = false;
        transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }

    // Damage Event
    public void TakeDamage(int damage)
    {
        print("Inflicted " + damage + " damage ; HPs left : " + _health + " / " + _maxHealth);
        switch (ennemyType)
        {
            default:
                _health -= damage;
                vfx.SetActive(true);
                vfx.GetComponent<ParticleSystem>().Play();
                if (_health <= 0)
                    Death();
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Rabbit:
                if (isStunned)
                {
                    _health -= damage;
                    vfx.SetActive(true);
                    vfx.GetComponent<ParticleSystem>().Play();
                    if (_health <= 0)
                        Destroy(gameObject);
                }
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Mushroom:
                if (_health == _maxHealth)
                {
                    PlaySound(mushroomSound);
                }
                _health -= damage;
                if (_health <= 0)
                    Death();
                vfx.SetActive(true);
                vfx.GetComponent<ParticleSystem>().Play();
                break;
                
        }
        Invoke(nameof(ResetVFX), 0.15f);
    }

    void ResetVFX()
    {
        vfx.GetComponent<ParticleSystem>().Stop();
        vfx.SetActive(false);
    }

    private void Death()
    {
        PlaySound(deathSound);
        _attack = 0;
        _isDead = true;
        switch (ennemyType)
        {
            default:
                player.GetComponent<AllPlayerReferences>().allEnemies.Add(this);
                navMesh.Warp(new Vector3(0, -50, 0));
                _rigidbody.useGravity = false;
                navMesh.SetDestination(new Vector3(0, -50, 0));
                navMesh.isStopped = true;
                Vector3 screwThrowVector = Random.onUnitSphere * 20;
                screwThrowVector = new Vector3(screwThrowVector.x, 50, screwThrowVector.z);
                
                int randNum = UnityEngine.Random.Range(0, 100);
                if (randNum > 90)
                {
                    GameObject thisScrewDrop = thisScrewDrop = Instantiate(screwDrop, transform.position + Vector3.up * 5, transform.rotation);
                    thisScrewDrop.GetComponent<Rigidbody>().AddForce(screwThrowVector, ForceMode.Impulse);
                    screwThrowVector = Random.onUnitSphere * 20;
                }
                if (randNum > 70)
                {
                    GameObject thisScrewDrop = thisScrewDrop = Instantiate(screwDrop, transform.position + Vector3.up * 5, transform.rotation);
                    thisScrewDrop.GetComponent<Rigidbody>().AddForce(screwThrowVector, ForceMode.Impulse);
                }
                break;
            case Enum_EnnemyTypes.EnnemyTypes.Rabbit:
                float timeWaitScrew = 0.0f;
                for (int i = 0, num = Random.Range(20, 25); i < num; i++)
                {
                    Invoke(nameof(SummonScrew), timeWaitScrew);
                    timeWaitScrew += 0.1f;
                    navMesh.isStopped = true;
                }
                GameObject thisJetpackDrop = Instantiate(jetpackDrop, transform.position + Vector3.up, transform.rotation);
                thisJetpackDrop.GetComponent<Collectibles>().type = Enum_Collectibles.CollectibleType.Jetpack;
                Destroy(gameObject, 3f);
                _isDead = true;
                break;
        }
    }

    private void SummonScrew()
    {
        Vector3 screwThrowVector = Random.onUnitSphere * 10;
        Vector3 randomPosModifier = Random.onUnitSphere * 1;
        screwThrowVector = new Vector3(screwThrowVector.x, 50, screwThrowVector.z);
        GameObject thisRabbitScrewDrop =
            Instantiate(screwDrop, transform.position + randomPosModifier + Vector3.up * 5, transform.rotation);
        thisRabbitScrewDrop.GetComponent<Rigidbody>().AddForce(screwThrowVector, ForceMode.Impulse);
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
        navMesh.isStopped = false;
    }
    
    // Sound Player
    private void PlaySound(AudioResource audio)
    {
        _audioSource.resource = audio;
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
        if (other.gameObject.layer == 7 && (isRushAttack || other.GetComponent<LinkedObject>().getIsFattening()))
        {
            if (!isStunned)
                GetStunned();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 7 || !isRushAttack) return;
        if (!isStunned)
            GetStunned();
    }
}
