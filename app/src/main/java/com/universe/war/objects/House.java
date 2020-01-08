package com.universe.war.objects;

public class House extends Building{
    int lives;
    int decLivesAttacked;
    int timeToConstruction;
    int workersCapacity;

    public House(int lives, int decLivesAttacked, int timeToConstruction,int workersCapacity) {
        super(lives, decLivesAttacked, timeToConstruction);
        this.decLivesAttacked=decLivesAttacked;
        this.lives=lives;
        this.timeToConstruction=timeToConstruction;
        this.workersCapacity=workersCapacity;
    }
}
