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
        if (Player.Current == null) { return;  }

        nav.SetActive(Player.Current.Controller.isMoving && !Player.Current.isDead);
        if (Player.Current.isDead) return;
        if (Mouse.current.leftButton.wasPressedThisFrame)
            Click();
    }

    private void Click()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            int hitLayer = hit.collider.gameObject.layer;
            if (hitLayer == LayerMask.NameToLayer("Ground"))
            {
                Player.Current.SetTarget(null);
                Player.Current.Controller.StopMovement();
                Player.Current.Controller.MoveToPosition(hit.point);
                Debug.Log(hit.point);
            }
            else if (hitLayer == LayerMask.NameToLayer("Enemy"))
            {
                Character enemy = hit.collider.GetComponent<Character>();
                Player.Current.SetTarget(enemy);
            }
            nav.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
        }
        Debug.Log("Jalan");
    }


}
