using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinBehaviour : MonoBehaviour
{
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    
    
    private void Start()
    {
        _startPosition = gameObject.transform.position;
        _startRotation = gameObject.transform.rotation;
    }
    
    public void Win()
    {
        Debug.Log("Win");
        gameObject.transform.position = _startPosition;
        gameObject.transform.rotation = _startRotation;
    }
}
