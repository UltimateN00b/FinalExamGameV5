using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMissedMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAttackMissed()
    {
        GameObject.Find("ConfirmAttackButton").GetComponent<ConfirmAttackButton>().HideUI();

        if (TurnManager.GetCurrTurnCharacter().tag.Contains("Enemy"))
        {
            TurnManager.GetCurrTurnCharacter().GetComponent<EnemyAI>().CeaseEnemyAI();
        }

        TurnManager.NextTurn();
    }
}
