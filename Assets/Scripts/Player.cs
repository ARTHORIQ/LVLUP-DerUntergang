using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player Current;

    private Animator anim;
    private float lastAttackTime;

    protected override void Awake()
    {
        base.Awake();
        Current = this;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (target != null && !target.isDead)
        {
            float targetDistance = Vector3.Distance(transform.position, target.transform.position);
            if (targetDistance < attackRange)
            {
                Controller.StopMovement();
                Controller.LookTowards(target.transform.position - transform.position);
                if (Time.time - lastAttackTime > attackRate)
                {
                    lastAttackTime = Time.time;
                    anim.SetTrigger("isAttacking");
                }
            }
            else Controller.MoveToTarget(target.transform);
        }
        anim.SetBool("isMoving", Controller.isMoving);
    }
    public override void Die()
    {
        base.Die();
        anim.SetBool("isDead", true);
    }

}
