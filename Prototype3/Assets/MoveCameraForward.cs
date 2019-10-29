using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCameraForward : MonoBehaviour
{

    public float speed;

    public float timeBeforeChange;

    private float _timer;

    public GameObject fadeCanvas;

    public string nextScene = "CombatInstructions";

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        _timer += Time.deltaTime;

        if (_timer >= timeBeforeChange)
        {
            fadeCanvas.SetActive(true);
            fadeCanvas.GetComponent<FadeCanvasLegacy>().ChangeScene(nextScene);
        }
    }
}
