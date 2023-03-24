using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
    [SerializeField] private GameObject nav;
    [SerializeField] private LayerMask layerMask;

    private void Update()
    {
        if (Player.current == null) return;
        nav.SetActive(Player.current.Controller.isMoving && !Player.current.isDead);
        if (Player.current.isDead) return;
        if (Mouse.current.rightButton.wasPressedThisFrame)

        {
            Debug.Log("Left mouse button pressed!");
            Click();
        }
    }

    private void Click()
    {
        Debug.Log("Click Called!!");
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 1f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            int hitLayer = hit.collider.gameObject.layer;
            if (hitLayer == LayerMask.NameToLayer("Ground"))
            {
                Player.current.SetTarget(null);
                Player.current.Controller.StopMovement();
                Player.current.Controller.MoveToPosition(hit.point);
                Debug.Log(hit.point);
            }
            else if (hitLayer == LayerMask.NameToLayer("Enemy"))
            {
                Character enemy = hit.collider.GetComponent<Character>();
                Player.current.SetTarget(enemy);
            }
            nav.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
        }
        Debug.Log("Jalan");
    }
}
