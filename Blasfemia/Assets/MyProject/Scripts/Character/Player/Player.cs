using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    protected override void Update()
    {
        base.Update();

        PlayerInputs();
        if (direction.x < 0 && !facingLeft || direction.x > 0 && facingLeft) Flip();
    }

    private void PlayerInputs()
    {
        float input_x = Input.GetAxisRaw("Horizontal");
        direction.x = input_x;

        if (Input.GetButtonDown("Jump") && OnGround()) canJump = true;
    }
}
