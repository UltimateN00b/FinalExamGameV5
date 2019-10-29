using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualAttack : MonoBehaviour
{
    private int _roll;
    private int _aP;
    private int _pP;
    private string _type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRoll(int roll)
    {
        _roll = roll;
    }

    public void SetAP(int damage)
    {
        _aP = damage;
    }

    public void SetPP(int damage)
    {
        _pP = damage;
    }

    public void SetType(string type)
    {
        _type = type;
    }

    public int GetMyRoll()
    {
        return _roll;
    }

    public int GetMyAP()
    {
        return _aP;
    }

    public int GetMyPP()
    {
        return _pP;
    }

    public string GetMyType()
    {
        return _type;
    }
}
