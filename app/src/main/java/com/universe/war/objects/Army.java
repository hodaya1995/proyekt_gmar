package com.universe.war.objects;

import java.util.ArrayList;
import java.util.List;

public class Army {
    List<Soldier> soldiers;
    Base base;

    public Army() {
        //TODO ADD SOME SOLDIERS
        this.soldiers=new ArrayList<>();
        this.base=new Base();
    }
}
