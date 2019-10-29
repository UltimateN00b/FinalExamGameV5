using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{

    private static List<GameObject> _charactersInCombat = new List<GameObject>();

    private static GameObject _currTurnCharacter;

    private static int _currChar;

    private void Awake()
    {
        GameObject evolution = GameObject.Find("EvolutionHolder").transform.GetChild(0).gameObject;
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        enemy.GetComponent<SpriteRenderer>().sprite = evolution.GetComponent<Evolution>().enemySprite;
        enemy.GetComponent<Character>().ChangeCharacter(evolution.GetComponent<CharacterTransfer>());
        enemy.GetComponent<EnemyAI>().ChangeEnemyAI(evolution.GetComponent<EnemyAITransfer>());
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("EvolutionHolder"));

        _charactersInCombat = new List<GameObject>();

        FindAllCharacters();
        SetTurnOrder();
        SetStartingCharacter();

        _currChar = _charactersInCombat.IndexOf(_currTurnCharacter);
        Debug.Log("CHARACTERS IN COMBAT: "+_charactersInCombat.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FindAllCharacters()
    {
        _charactersInCombat = new List<GameObject>();

        GameObject characters = GameObject.Find("Characters");

        GameObject player = Utilities.SearchChild("Player_Character", characters);
        GameObject enemies = Utilities.SearchChild("Enemies", characters);
        GameObject companions = Utilities.SearchChild("Companions", characters);

        _charactersInCombat.Add(player);

        for (int i = 0; i < companions.transform.childCount; i++)
        {
            GameObject currChild = companions.transform.GetChild(i).gameObject;

            if (currChild.activeInHierarchy)
            {
                _charactersInCombat.Add(currChild);
            }
        }

        for (int i = 0; i < enemies.transform.childCount; i++)
        {
            GameObject currChild = enemies.transform.GetChild(i).gameObject;

            if (currChild.activeInHierarchy)
            {
                _charactersInCombat.Add(currChild);
            }
        }
    }

    public void SetStartingCharacter()
    {
        GameObject startingChar = _charactersInCombat[0];

        float highestInit = 0;

        foreach (GameObject g in _charactersInCombat)
        {
            if (g.GetComponent<Character>().GetInitiative() > highestInit)
            {
                highestInit = g.GetComponent<Character>().GetInitiative();
                startingChar = g;
            }
        }

        _currTurnCharacter = startingChar;


        _currTurnCharacter.GetComponent<Character>().SetMyTurn();
        _currTurnCharacter.GetComponent<Character>().ChangeDiceType();
    }

    public void SetTurnOrder()
    {
        List<GameObject> orderedList = new List<GameObject>();

        GameObject player = null;
        List<GameObject> companions = new List<GameObject>();
        List<GameObject> enemies = new List<GameObject>();

        foreach (GameObject g in _charactersInCombat)
        {

            if (g.tag.Equals("Companion"))
            {
                companions.Add(g);
            }
            else if (g.tag.Equals("Enemy"))
            {
                enemies.Add(g);
            } else if (g.tag.Equals("Player"))
            {
                player = g;
            }
        }

        foreach (GameObject c in companions)
        {
            orderedList.Add(c);
        }

        orderedList.Add(player);

        foreach (GameObject e in enemies)
        {
            orderedList.Add(e);
        }

        _charactersInCombat = orderedList;
    }

    public static void NextTurn()
    {
        GameObject.Find("AttackHolder").GetComponent<AttackHolder>().ClearAttacks();

        Utilities.SearchChild("TurnArrow", _currTurnCharacter).GetComponent<SpriteRenderer>().enabled = false;

        if (_currChar < _charactersInCombat.Count-1)
        {
            _currChar++;
        } else
        {
            _currChar = 0;
        }

        _currTurnCharacter = _charactersInCombat[_currChar];

        if (GetCurrTurnCharacter().tag.Contains("Player"))
        {
            GameObject.Find("ConfirmAttackButton").GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GameObject.Find("ConfirmAttackButton").GetComponent<SpriteRenderer>().enabled = false;
        }

        Dice.ResetNumDiceStopped();

        _currTurnCharacter.GetComponent<Character>().SetMyTurn();

        TutorialManager.SetFirstRoundFinished();
    }

    public static GameObject GetCurrTurnCharacter()
    {
        return _currTurnCharacter;
    }

    public static void FinishAttack()
    { GameObject.Find("ConfirmAttackButton").GetComponent<ConfirmAttackButton>().HideUI();

        Debug.Log("FINISHED ATTACK");
        DiceManager.GetCurrCharacter().SetTurnFinished();
        DiceManager.CurrCombatStage = DiceManager.CombatStage.ChangingTurns;
        NextTurn();
    }
}
