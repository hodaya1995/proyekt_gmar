package com.universe.war.objects;

import com.universe.war.interfaces.Animateable;
import com.universe.war.interfaces.Upgradeable;

public class Soldier extends Person implements Upgradeable, Animateable {

int decLivesInAttack;
int status;
int decLivesAttacked;

    public Soldier(int decLivesInAttack,int decLivesAttacked,int lives) {
        super(lives,decLivesAttacked);
        this.decLivesAttacked=decLivesAttacked;
        this.decLivesInAttack = decLivesInAttack;
        this.status=1;
    }

    @Override
    public void animate() {

    }

    @Override
    public void upgrade() {

    }
}
