using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{ 
     public GameObject weaponPrefab;
      public Animator animator;
      

    void Shoot(){
            //Vector2 shootingDirection=new Vector2(animator.GetFloat("horizontal"),animator.GetFloat("vertical"));
            // Debug.Log("horizontal: "+animator.GetFloat("horizontal"));
            // Debug.Log("vertical: "+animator.GetFloat("vertical"));
            // Debug.Log("speed: "+animator.GetFloat("speed"));
            Vector2 shootingDirection=new Vector2();
            if(Mathf.Abs(Attackable.velocityX)>Mathf.Abs(Attackable.velocityY) ){
              shootingDirection=new Vector2(Attackable.velocityX,0);
            }else{
              shootingDirection=new Vector2(0,Attackable.velocityY);
            }
            GameObject weapon=Instantiate(weaponPrefab,transform.position,Quaternion.identity.normalized);
            weapon.GetComponent<Rigidbody2D>().velocity=shootingDirection*20.0f;
            weapon.transform.Rotate(0.0f,0.0f,Mathf.Atan2(shootingDirection.y,shootingDirection.x)*Mathf.Rad2Deg);

            Destroy(weapon,0.03f);    
               
       
   }



   
}
