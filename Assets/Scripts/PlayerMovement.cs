using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rgb;
    Vector2 direction;

    [SerializeField] PlayerAnimationController animController;

    void Start()
    {

    }

    void Update()
    {
        ProcessMovementInput();
        ProcessInventoryInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        direction = new Vector2(moveX, moveY).normalized;
        animController.HandleFlip(direction);
        animController.SetSprite(direction);
    }

    private void ProcessInventoryInput(){
        if(Input.GetKeyDown("i")){
            InventoryManager._instance.ShowInventory();
        }
    }

    private void Move()
    {
        rgb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }


}
