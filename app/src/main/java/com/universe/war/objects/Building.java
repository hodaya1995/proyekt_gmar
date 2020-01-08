package com.universe.war.objects;

public class Building {
    int lives;
    int decLivesAttacked;
    int status;
    int timeToConstruction;

    public Building(int lives, int decLivesAttacked,int timeToConstruction) {
        this.timeToConstruction=timeToConstruction;
        this.lives = lives;
        this.decLivesAttacked = decLivesAttacked;
        this.status=1;
    }

    public Building(int lives, int decLivesAttacked) {
        this.status=1;
        this.timeToConstruction=0;
        this.lives = lives;
        this.decLivesAttacked = decLivesAttacked;
    }
}
