
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public List<GameObject> AllInBox;
    private Ennemy _linkedEnemy;

    private Vector3 _forceForRush;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _linkedEnemy = gameObject.GetComponent<Ennemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BasicAttackBegin()
    {
        if (!_linkedEnemy.isAttacking)
        {
            Invoke(nameof(BasicAttack), 0.5f);
            _linkedEnemy.isAttacking = true;
        }
    }

    void BasicAttack()
    {
        _forceForRush = _linkedEnemy.transform.forward * 200;
        _linkedEnemy.transform.GetComponent<Rigidbody>().AddForce(_forceForRush, ForceMode.Impulse);
        Invoke(nameof(BasicAttackEnds), 0.5f);
    }

    void BasicAttackEnds()
    {
        _linkedEnemy.isAttacking = false;
        _linkedEnemy.transform.GetComponent<Rigidbody>().AddForce(-_forceForRush, ForceMode.Impulse);
        _forceForRush = Vector3.zero;
    }

    public void SpiderStrikeBegins()
    {
        if (!_linkedEnemy.isAttacking)
        {
            Invoke(nameof(SpiderStrike), 0.5f);
            _linkedEnemy.isAttacking = true;
        }
    }

    void SpiderStrike()
    {
        _linkedEnemy.transform.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 1f);
        _linkedEnemy.transform.GetComponent<CapsuleCollider>().height = 3.0f;
        Invoke(nameof(SpiderStrikeEnds), 1f);
    }

    void SpiderStrikeEnds()
    {
        _linkedEnemy.isAttacking = false;
        _linkedEnemy.transform.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0f);
        _linkedEnemy.transform.GetComponent<CapsuleCollider>().height = 1.0f;
    }

    public void BossRushBegin()
    {
        if (!_linkedEnemy.isAttacking)
        {
            Invoke(nameof(BossRush), 0.5f);
            _linkedEnemy.isAttacking = true;
        }
    }

    void BossRush()
    {
        _forceForRush = _linkedEnemy.transform.forward * 300;
        _linkedEnemy.transform.GetComponent<Rigidbody>().AddForce(_forceForRush, ForceMode.Impulse);
        Invoke(nameof(BossRushEnds), 1.25f);
    }

    void BossRushEnds()
    {
        _linkedEnemy.isAttacking = false;
        _linkedEnemy.transform.GetComponent<Rigidbody>().AddForce(-_forceForRush, ForceMode.Impulse);
        _linkedEnemy.InvokeRepeating(nameof(_linkedEnemy.ChanceForRush), 0f, 3f);
        _forceForRush = Vector3.zero;
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
}
