using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    [Header("Stats")]
    public int CurrentHP;
    public int MaxHP;
    public int Damage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackRate;

    [Header("Components")]
    [HideInInspector]
    public CharacterControllers Controller;
    public GameObject healthBarPrefab;
    public Character target;
    public bool isDead;

    public event UnityAction onTakeDamage;
    public event UnityAction onDie;

    protected virtual void Awake()
    {
        Controller = GetComponent<CharacterControllers>();
    }

    public virtual void SetTarget(Character t){
        target = t;
    }
    public virtual void TakeDamage(int value) {
        CurrentHP -= value;
        onTakeDamage?.Invoke();

        if (CurrentHP <= 0)
            Die();
    }
    public virtual void AttackTarget()
    {
        if (target != null)
            target.TakeDamage(Damage);
    }
    public virtual void Die()
    {
        isDead = true;
        onDie?.Invoke();
        Destroy(gameObject, 3f);
    }
}
