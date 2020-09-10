using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/WalkToTarget")]
public class WalkToTarget : FilteredFlockBehavior
{

    //float prevVelocityX = Mathf.Infinity;
    //float prevVelocityY = Mathf.Infinity;
    //bool facingRight;
    //Animator anim;

    //private void Flip()//flip the animation
    //{
    //    facingRight = !facingRight;
    //    Vector3 theScale = transform.localScale;
    //    theScale.x *= -1;
    //    transform.localScale = theScale;
    //}

    //private void FlipAnimation()
    //{
    //    float h = anim.GetFloat("horizontal");
    //    if (h > 0 && !facingRight)
    //        Flip();
    //    else if (h < 0 && facingRight)
    //        Flip();
    //}

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 velocity=agent.GetComponent<Walk>().GetCurrentMoveToPoint();
        //anim = this.gameObject.GetComponentInChildren<Animator>();
        //float horizontalVelocity = Vector2.Dot(velocity, Vector2.right);
        //float verticalVelocity = Vector2.Dot(velocity, Vector2.up);
        //if (prevVelocityX != Mathf.Infinity && prevVelocityY != Mathf.Infinity && (prevVelocityX < 0 && horizontalVelocity < 0) || (prevVelocityX > 0 && horizontalVelocity > 0) || (prevVelocityX == 0 && horizontalVelocity == 0)
        //    && (prevVelocityY < 0 && verticalVelocity < 0) || (prevVelocityY > 0 && verticalVelocity > 0) || (prevVelocityY == 0 && verticalVelocity == 0))
        //{
        //    anim.SetFloat("horizontal", horizontalVelocity);
        //    anim.SetFloat("vertical", verticalVelocity);

        //}
        //else if (prevVelocityX == Mathf.Infinity && prevVelocityY == Mathf.Infinity)
        //{
        //    anim.SetFloat("horizontal", horizontalVelocity);
        //    anim.SetFloat("vertical", verticalVelocity);

        //}
        //prevVelocityX = horizontalVelocity;
        //prevVelocityY = verticalVelocity;

        //float h = velocity.x;
        //if (h > 0 && !facingRight)
        //    Flip();
        //else if (h < 0 && facingRight)
        //    Flip();

        //FlipAnimation();
        return velocity;
       
    }
}
