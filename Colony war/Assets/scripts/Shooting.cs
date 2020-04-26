using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{ 
     public GameObject weaponPrefab;
      public Animator animator;
      

    void Shoot(){
       
            
            Vector2 shootingDirection=new Vector2(animator.GetFloat("horizontal"),animator.GetFloat("vertical"));
            GameObject weapon=Instantiate(weaponPrefab,transform.position,Quaternion.identity.normalized);
            weapon.GetComponent<Rigidbody2D>().velocity=shootingDirection*20.0f;
            weapon.transform.Rotate(0.0f,0.0f,Mathf.Atan2(shootingDirection.y,shootingDirection.x)*Mathf.Rad2Deg);
            Destroy(weapon,2.0f);    
               
       
   }



   
}
