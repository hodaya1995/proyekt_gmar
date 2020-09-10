using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StayInRectangle")]
public class StayInRectangle : FilteredFlockBehavior
{

    public Vector2 center;
    public float radius1 = 3f;
    public float radius2 = 1.5f;
    int soldiersCount = 10;
    const float AgentDensity = 0.02f;


    public void Start()
    {
        Vector2 randomSquare = new Vector2(Random.Range(0, 0.5f), Random.Range(0, 1f));

      
    }
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
       
        Vector2 randomSquare = new Vector2(Random.Range(0, 0.5f), Random.Range(0, 1f));

         return randomSquare * soldiersCount * AgentDensity;


    }
}
