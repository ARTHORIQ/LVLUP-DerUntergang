using UnityEngine;

public class Player : Character
{
    public static Player current;

    private Animator anim;
    private float lastAttackTime;

    protected override void Awake()
    {
        base.Awake();
        current = this;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (target != null && !target.isDead)
        {
            float targetDistance = Vector3.Distance(transform.position, target.transform.position);
            if (targetDistance < attackRange)
            {
                Controller.StopMovement();
                Controller.LookTowards(target.transform.position - transform.position);

                // Check attack speed
                if (Time.time - lastAttackTime > attackRate)
                {
                    lastAttackTime = Time.time;
                    anim.SetTrigger("isAttacking");
                }
            }
            else
            {
                Controller.MoveToTarget(target.transform);
            }
        }

        anim.SetBool("isMoving", Controller.isMoving);
    }

    public override void Die()
    {
        Controller.StopMovement();
        base.Die();
        Controller.isMoving = false;
        anim.SetBool("isDead", true);
        anim.SetBool("isMoving", false);
    }
}