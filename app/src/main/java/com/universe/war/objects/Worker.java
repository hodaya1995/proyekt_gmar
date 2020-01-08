package com.universe.war.objects;

public class Worker extends Person{
    int income;
    int decLivesAttacked;
    int lives;
    public Worker(int income,int decLivesAttacked,int lives) {
        super(lives,decLivesAttacked);
        this.lives=lives;
        this.decLivesAttacked=decLivesAttacked;
        this.income = income;
    }
}
