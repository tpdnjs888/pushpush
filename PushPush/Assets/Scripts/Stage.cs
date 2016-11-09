using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Stage : MonoBehaviour {

    public GameObject Wall;
    public GameObject Home;
    public GameObject Ball;

    public Transform PlyerPosition;

    [HideInInspector]
    public List<Transform> Walls;
    [HideInInspector]
    public List<Home> Homes;
    [HideInInspector]
    public List<Ball> Balls;

    private void Start()
    {
        Walls = Wall.GetComponentsInChildren<Transform>().ToList();
        Homes = Home.GetComponentsInChildren<Home>().ToList();
        Balls = Ball.GetComponentsInChildren<Ball>().ToList();
    }
}
