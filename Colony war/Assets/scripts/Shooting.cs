﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Animator animator;


    void Shoot()
    {
        float x = animator.GetFloat("horizontal");
        float y = animator.GetFloat("vertical");
        Vector2 shootingDirection = new Vector2(x, y);
        //if(Mathf.Abs(Attack.velocityX)>Mathf.Abs(Attack.velocityY) ){
        //  shootingDirection=new Vector2(Attack.velocityX,0);
        //}else{
        //  shootingDirection=new Vector2(0, Attack.velocityY);
        //}

        GameObject weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity.normalized);
        weapon.GetComponent<Rigidbody2D>().velocity = shootingDirection * 20.0f;
        weapon.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);

        Destroy(weapon, 0.03f);


    }




}
