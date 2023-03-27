using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUi : MonoBehaviour
{
    [SerializeField] private Image healthFill;
    [SerializeField] private Color playerHealthColor;
    [SerializeField] private Color enemyHealthColor;
    private Character character;

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(
        transform.position - Camera.main.transform.position
        );
    }
    void UpdateHealthBar()
    {
        healthFill.fillAmount =
        (float)character.CurrentHP / (float)character.MaxHP;
    }
    void HideHealthBar()
    {
        gameObject.SetActive(false);
    }


    void OnEnable()
    {
        character.onTakeDamage += UpdateHealthBar;
        character.onDie += HideHealthBar;
    }
    void OnDisable()
    {
        character.onTakeDamage -= UpdateHealthBar;
        character.onDie -= HideHealthBar;
    }
    void Awake()
    {
        character = GetComponentInParent<Character>();
        healthFill.color =
        transform.root.tag == "Player" ?
        playerHealthColor : enemyHealthColor;
    }
    void Start()
    {
        UpdateHealthBar();
    }

}
