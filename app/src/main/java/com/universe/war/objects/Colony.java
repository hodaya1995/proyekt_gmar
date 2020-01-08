package com.universe.war.objects;

import com.universe.war.interfaces.Upgradeable;

import java.util.ArrayList;
import java.util.List;

public class Colony implements Upgradeable {
 Army army;
 MainBuilding mainBuilding;
 List<House> houses;
 Resources resources;
 List<Worker> workers;
 String colonyName;
 public static int status; //will activate in main building methods

    //TODO ADD MORE CONSTRUCTORS
    public Colony(String colonyName) {
        //TODO ADD INITIAL AMOUNT OF RESOURCES FOR THE BEGINNING- LIKE 3 HOUSES FOR EXAMPLE
        this.colonyName = colonyName;
        this.army=new Army();
        this.mainBuilding=new MainBuilding();
        this.houses=new ArrayList<>();
        this.resources=new Resources();
        this.workers=new ArrayList<>();
        this.status=1;//level 1
    }


    @Override
    public void upgrade() {

    }
}
