package com.universe.war.objects;

public class Base extends Building {
    int lives;
    int decLivesAttacked;
    int timeToCreateSoldiers;

    public Base(int lives, int decLivesAttacked,int timeToCreateSoldiers) {
        super(lives, decLivesAttacked);
        this.lives=lives;
        this.decLivesAttacked=decLivesAttacked;
        this.timeToCreateSoldiers=timeToCreateSoldiers;
    }
}
