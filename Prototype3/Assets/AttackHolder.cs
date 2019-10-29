using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHolder : MonoBehaviour
{
    private List<IndividualAttack> _attacks = new List<IndividualAttack>();

    private void OnLevelWasLoaded(int level)
    {
        ClearAttacks();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAttack(int roll, string type, int aP)
    {
        IndividualAttack newAttack = new IndividualAttack();

        newAttack.SetRoll(roll);
        newAttack.SetType(type);
        newAttack.SetAP(aP);

        _attacks.Add(newAttack);
    }

    public void AddAttack(int roll, string type, int aP, int pP)
    {
        IndividualAttack newAttack = new IndividualAttack();

        newAttack.SetRoll(roll);
        newAttack.SetType(type);
        newAttack.SetAP(aP);
        newAttack.SetPP(pP);

        _attacks.Add(newAttack);
    }

    public void ClearAttacks()
    {
        _attacks = new List<IndividualAttack>();
    }

    public List<IndividualAttack> GetAttacks()
    {
        return _attacks;
    }
}
