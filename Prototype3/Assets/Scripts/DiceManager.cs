using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DiceManager : MonoBehaviour
{

    public float resetDiceTime;

    public enum CombatStage { DiceAndTargets, ExecutingAttack, ChangingTurns }
    public static CombatStage CurrCombatStage = CombatStage.ChangingTurns;

    private static Character _currCharacter;
    private static List<Character> _currTargets = new List<Character>();

    private static bool _aPFound;
    private static bool _pPFound;

    private float _timer;

    private Vector3 startPos;

    private static bool _resetManually;

    private static bool _hasEnabledButtons;


    private static bool _canReset;

    private static int _numPlayerRolls;


    // Start is called before the first frame update
    void Start()
    {
        EnableAllButtons();
        _currTargets = new List<Character>();

    _timer = resetDiceTime;

        startPos = this.GetComponent<RectTransform>().position;

        _resetManually = false;

        _canReset = false;
    }

    private void OnLevelWasLoaded(int level)
    {
        _numPlayerRolls = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (_hasEnabledButtons == false)
        {

            if (CurrCombatStage == CombatStage.DiceAndTargets)
            {
                if (!GetCurrCharacter().tag.Equals("Enemy"))
                {
                    EnableAllButtons();
                    _hasEnabledButtons = true;
                }
            }
        }

        if (_canReset)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                GameObject.Find("ConfirmAttackButton").GetComponent<ConfirmAttackButton>().ShowUI();

                for (int i = 0; i < GameObject.Find("DiceCanvas").transform.childCount; i++)
                {
                    if (GameObject.Find("DiceCanvas").transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        GameObject.Find("DiceCanvas").transform.GetChild(i).GetComponent<Dice>().StartDiceReset();
                    }
                }

                _timer = resetDiceTime;
                _resetManually = false;
                _canReset = false;
            }
        }
    }

    public static Character GetCurrCharacter()
    {
        return _currCharacter;
    }

    public static List<Character> GetCurrTargets()
    {
        return _currTargets;
    }

    public static void SetCurrCharacter(Character charac)
    {
        _currCharacter = charac;
    }

    public static void AutoSetTargets()
    {
        //*change to go for lowest health for players companions and enemies (depending on whose turn it is of course).

        if (_currTargets.Count <= 0)
        {

            if (DiceManager.GetCurrCharacter().tag.Contains("Player") || DiceManager.GetCurrCharacter().tag.Contains("Companion"))
            {
                if (GameObject.FindGameObjectsWithTag("Enemy") != null)
                {
                    _currTargets.Add(GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<Character>());
                }
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag("Player") != null)
                {
                    _currTargets.Add(GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Character>());
                }
            }
        }

        foreach (Character c in DiceManager.GetCurrTargets())
        {
            Debug.Log(c.name);
            //c.GetComponent<SpriteRenderer>().color = Color.red;
        }

    }

    public static void AddTarget(Character target)
    {
       // target.GetComponent<SpriteRenderer>().color = Color.red;
        _currTargets.Add(target);
    }

    public static void ClearTargets()
    {
        foreach (Character c in _currTargets)
        {
            //c.GetComponent<SpriteRenderer>().color = Color.white;
        }

        _currTargets = new List<Character>();
    }

    public static void ExecuteAttack(int damage) //OLD EXECUTE ATTACK. REVERT TO THIS IF WE TAKE OUT COMBO ANIMATIONS.
    {
        foreach (Character c in _currTargets)
        {
            if (DiceManager.FindTypeTotalGameObject("AP") != null)
            {
                //int damage = int.Parse(DiceManager.FindTypeTotalGameObject("AP").transform.GetChild(0).GetComponent<Text>().text);

                GameObject healthCanvas = Utilities.SearchChild("HealthCanvas", c.gameObject);
                GameObject healthBar = Utilities.SearchChild("HealthBar", healthCanvas);

                float excess = c.GetComponent<Character>().GetCurrHP() - damage;

                if (excess < 0)
                {
                    GameObject currCharacter = TurnManager.GetCurrTurnCharacter().GetComponent<Character>().gameObject;
                    GameObject healthCanvasChar = Utilities.SearchChild("HealthCanvas", currCharacter.gameObject);
                    GameObject healthBarChar = Utilities.SearchChild("HealthBar", healthCanvasChar);

                    healthBarChar.GetComponent<HealthBar>().ChangeHealth(Mathf.Abs(excess));
                }

                healthBar.GetComponent<HealthBar>().ChangeHealth(-damage);

                if (c.GetComponent<Character>().GetCurrHP() <= 0)
                {
                    if (c.tag.Contains("Player"))
                    {
                        GameObject.Find("Ayanda").GetComponent<Animator>().SetBool("dying", true);
                    } else
                    {
                        if (!TutorialManager.IsTutorial())
                        {
                            GameObject.Find("AyandaMonster").GetComponent<Animator>().SetBool("dying", true);
                        }
                    }
                }
            }
        }
    }

    public static DiceType SearchDiceType(string diceTypeName)
    {

        DiceType returnDiceType = null;

        GameObject diceTypeManager = GameObject.Find("DiceTypeManager");

        foreach (DiceType dT in diceTypeManager.GetComponents<DiceType>())
        {
            if (dT.GetName().Contains(diceTypeName))
            {
                returnDiceType = dT;
            }
        }

        return returnDiceType;
    }

    public static bool APFound()
    {
        return _aPFound;
    }

    public static bool PPFound()
    {
        return _pPFound;
    }

    public static void SetAPFound(bool apFound)
    {
        _aPFound = apFound;
    }

    public static void SetPPFound(bool ppFound)
    {
        _pPFound = ppFound;
    }

    public static void ClearAllDiceTotals()
    {
        if (!_currCharacter.tag.Contains("Enemy"))
        {
            GameObject.Find("ClickTheDice").GetComponent<Text>().text = "CLICK THE DICE";
        }
        else
        {
            GameObject.Find("ClickTheDice").GetComponent<Text>().text = "";
        }

        _aPFound = false;
        _pPFound = false;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("TypeTotal"))
        {
            g.GetComponent<Text>().text = "";
            g.transform.GetChild(0).GetComponent<Text>().text = "0";
            g.transform.GetChild(0).GetComponent<Text>().enabled = false;
        }

        DiceManager.ResetRollValues();
    }

    public static GameObject FindEmptyTypeTotal()
    {
        GameObject firstEmptyType = null;

        List<GameObject> allTypeTotals = GameObject.FindGameObjectsWithTag("TypeTotal").ToList<GameObject>();

        var sortedList = allTypeTotals.OrderBy(go => go.name).ToList();

        foreach (GameObject g in sortedList)
        {
            if (firstEmptyType == null)
            {
                if (g.GetComponent<Text>().text.Equals(""))
                {
                    firstEmptyType = g;
                }
            }
        }

        return firstEmptyType;
    }

    public static GameObject FindTypeTotalGameObject(string typeTotal)
    {
        GameObject typeTotalObject = null;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("TypeTotal"))
        {
            if (g.GetComponent<Text>().text.ToUpper().Contains(typeTotal.ToUpper()))
            {
                typeTotalObject = g;
            }
        }

        return typeTotalObject;
    }

    public float GetResetDiceTime()
    {
        return resetDiceTime;
    }

    public static void DisableAllButtons()
    {
        for (int i = 0; i < GameObject.Find("DiceCanvas").transform.childCount; i++)
        {
            if (GameObject.Find("DiceCanvas").transform.GetChild(i).gameObject.activeInHierarchy)
            {
                GameObject.Find("DiceCanvas").transform.GetChild(i).GetComponent<Button>().enabled = false;
            }
        }
    }

    public static void EnableAllButtons()
    {
        for (int i = 0; i < GameObject.Find("DiceCanvas").transform.childCount; i++)
        {

            GameObject currChild = GameObject.Find("DiceCanvas").transform.GetChild(i).gameObject;

            if (currChild.activeInHierarchy)
            {
                if (!currChild.GetComponent<Dice>().IsParalysed())
                {
                    currChild.GetComponent<Button>().enabled = true;
                    //currChild.GetComponent<Image>().color = Color.white;
                    //currChild.GetComponent<ShakeObject>().Shake();
                    string questionMarkName = "QuestionMark" + currChild.name.ToCharArray()[currChild.name.Length - 1];
                    GameObject.Find(questionMarkName).GetComponent<Image>().enabled = true;
                }
            }
        }
    }

    public static void ResetRollValues()
    {
        for (int j = 0; j < GameObject.Find("DiceStatsCanvas").transform.childCount; j++)
        {
            if (GameObject.Find("DiceStatsCanvas").transform.GetChild(j).name.Contains("RollValue"))
            {
                GameObject.Find("DiceStatsCanvas").transform.GetChild(j).GetComponent<Text>().text = "";
            }
        }
    }

    public static void SetCanReset(bool reset)
    {
        _canReset = reset;
    }

    public static bool CanReset()
    {
        return _canReset;
    }

    public static void SetAllDiceUnclicked()
    {
        for (int i = 0; i < GameObject.Find("DiceCanvas").transform.childCount; i++)
        {
            if (GameObject.Find("DiceCanvas").transform.GetChild(i).gameObject.activeInHierarchy)
            {
                GameObject.Find("DiceCanvas").transform.GetChild(i).GetComponent<Dice>().SetClicked(false);
            }
        }
    }

    public static void IncreaseNumPlayerRolls()
    {
        _numPlayerRolls++;
    }

    public static int GetNumPlayerRolls()
    {
        return _numPlayerRolls;
    }
}
