using UnityEngine;
using Pathfinding;

public class PlayerDestinationSetter : MonoBehaviour
{
    protected Transform Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindFirstObjectByType<SuckAbility>().transform;
        GetComponent<Pathfinding.AIDestinationSetter>().target = Player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
