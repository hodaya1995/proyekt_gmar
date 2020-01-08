package com.universe.war.objects;

import com.universe.war.interfaces.Animateable;

public class Person {
int lives;
int decLivesAttacked;

    public Person(int lives, int decLivesAttacked) {
        this.lives = lives;
        this.decLivesAttacked = decLivesAttacked;
    }

    public Person(int decLivesAttacked) {
        this.decLivesAttacked = decLivesAttacked;
        this.lives=100;
    }

}
