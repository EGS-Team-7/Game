







using System.Collections;
using System.Collections.Generic;
using UnityEngine;








/// <summary>
/// A Data Class, containing:
///  - String name
///  - Int[] thresholds
/// This is also a PERSISTENT structure, it must be carried from scene-to-scene
/// </summary>
public class Difficulty : MonoBehaviour
{



    [Header("Difficulty Class")]

    [Space(20)]

    [Tooltip("The Name of this difficulty mode")]
    [SerializeField] private string _difficultyName = "Normal";

    [Space(20)]

    [Header("Difficulty Thresholds")]

    [Tooltip("Score to unlock default shot-type")]
    [SerializeField] private int _defaultThreshold = 0;

    [Tooltip("Score to unlock spread shot-type")]
    [SerializeField] private int _spreadThreshold = 100;

    [Tooltip("Score to unlock guided shot-type")]
    [SerializeField] private int _guidedThreshold = 200;



    /* REMOVED: Is adding duplicates!
    public void Awake()
    {
        // Do not destroy this object, ever!
        DontDestroyOnLoad(gameObject);
    }
    */



    public string Name()
    {
        return _difficultyName;
    }




    public List<int> Thresholds()
    {
        return new List<int>()
        {
            _defaultThreshold,
            _spreadThreshold,
            _guidedThreshold
        };
    }
}
