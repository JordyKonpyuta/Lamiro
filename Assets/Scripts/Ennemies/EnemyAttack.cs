
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyAttack : MonoBehaviour
{
    public List<GameObject> AllInBox;
    private Ennemy _linkedEnemy;
    
    // Audio
    public AudioResource[] attackSounds;
    private AudioSource _audioSource;

    private Vector3 _forceForRush;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _linkedEnemy = gameObject.GetComponent<Ennemy>();
    }

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void BasicAttackBegin()
    {
        if (!_linkedEnemy.isAttacking)
        {
            _linkedEnemy.navMesh.isStopped = true;
            Invoke(nameof(BasicAttack), 0.5f);
            _linkedEnemy.isAttacking = true;
        }
    }

    void BasicAttack()
    {
        PlaySound(attackSounds[0]);
        _forceForRush = _linkedEnemy.transform.forward * 75;
        _forceForRush.y += 20f;
        _linkedEnemy.transform.GetComponent<Rigidbody>().AddForce(_forceForRush, ForceMode.Impulse);
        Invoke(nameof(BasicAttackEnds), 0.75f);
    }

    void BasicAttackEnds()
    {
        _linkedEnemy.transform.GetComponent<Rigidbody>().AddForce(-_forceForRush, ForceMode.Impulse);
        _forceForRush = Vector3.zero;
        Invoke(nameof(StopAttacking), 0.5f);
    }

    public void SpiderStrikeBegins()
    {
        if (!_linkedEnemy.isAttacking)
        {
            _linkedEnemy.navMesh.isStopped = true;
            Invoke(nameof(SpiderStrike), 0.5f);
            _linkedEnemy.isAttacking = true;
        }
    }

    void SpiderStrike()
    {
        PlaySound(attackSounds[UnityEngine.Random.Range(2,4)]);
        _linkedEnemy.transform.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 1f);
        _linkedEnemy.transform.GetComponent<CapsuleCollider>().height = 3.0f;
        Invoke(nameof(SpiderStrikeEnds), 1f);
    }

    void SpiderStrikeEnds()
    {
        _linkedEnemy.transform.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
        _linkedEnemy.transform.GetComponent<CapsuleCollider>().height = 1.0f;
        Invoke(nameof(StopAttacking), 0.35f);
    }

    public void BossRushBegin()
    {
        if (!_linkedEnemy.isAttacking)
        {
            _linkedEnemy.navMesh.isStopped = true;
            _linkedEnemy.isRushAttack = true;
            Invoke(nameof(BossRush), 0.85f);
            _linkedEnemy.isAttacking = true;
        }
    }

    void BossRush()
    {
        PlaySound(attackSounds[1]);
        _forceForRush = _linkedEnemy.transform.forward * 300;
        _linkedEnemy.transform.GetComponent<Rigidbody>().AddForce(_forceForRush, ForceMode.Impulse);
        Invoke(nameof(BossRushEnds), 1.25f);
    }

    void BossRushEnds()
    {
        _linkedEnemy.transform.GetComponent<Rigidbody>().AddForce(-_forceForRush, ForceMode.Impulse);
        _linkedEnemy.InvokeRepeating(nameof(_linkedEnemy.ChanceForRush), 0f, 3f);
        _linkedEnemy.isRushAttack = false;
        _forceForRush = Vector3.zero;
        Invoke(nameof(StopAttacking), 0.75f);
    }

    void StopAttacking()
    {
        _linkedEnemy.isAttacking = false;
        _linkedEnemy.navMesh.isStopped = false;
    }
    
    // Got Stunned?
    public void StunAttackLoss()
    {
        CancelInvoke(nameof(_linkedEnemy.ChanceForRush));
        CancelInvoke(nameof(BossRush));
        CancelInvoke(nameof(BossRushEnds));
        CancelInvoke(nameof(BasicAttack));
        CancelInvoke(nameof(BasicAttackEnds));
        CancelInvoke(nameof(SpiderStrike));
        CancelInvoke(nameof(SpiderStrikeEnds));
        
        _linkedEnemy.transform.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
        _linkedEnemy.transform.GetComponent<CapsuleCollider>().height = 1.0f;
        _linkedEnemy.transform.GetComponent<Rigidbody>().AddForce(-_forceForRush, ForceMode.Impulse);
        _forceForRush = Vector3.zero;
        if (_linkedEnemy.ennemyType == Enum_EnnemyTypes.EnnemyTypes.Rabbit)
            _linkedEnemy.InvokeRepeating(nameof(_linkedEnemy.ChanceForRush), 5f, 3f);
    }

    
    // Box Collision Attack
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _linkedEnemy.isAttacking && !AllInBox.Contains(other.gameObject))
        {
            AllInBox.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AllInBox.Clear();
    }

    private void PlaySound(AudioResource audio)
    {
        _audioSource.resource = audio;
        _audioSource.Play();
    }
}
