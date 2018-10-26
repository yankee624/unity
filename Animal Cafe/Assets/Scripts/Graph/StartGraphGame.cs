using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGraphGame : MonoBehaviour {

    public DataController dataController;

    public Button startButton;
    public Button stopButton;

    public GameObject notEnoughMoneyPanel;

    public Material mat;
    public int initGridSize=100;

    public Camera cam;
    public Transform camPosition;
    public Vector3 initCamPosition;
    public float initOrthographicSize=0.8f;

    public LineRenderer lineRenderer;
    public int numPoints = 400;

    public Text multiplierText;
    public Text chosenMultiplierText;
    public Text winMoneyText;

    public Text historyText;
    public List<float> history;

    public static float multiplier = 1f;
    private float targetMultiplier;

    public Color green;
    public Color red;

    public static bool running = false;

    // Use this for initialization
    void Start()
    {
        dataController = FindObjectOfType<DataController>();

        initGridSize = 100;
        lineRenderer.positionCount = 0;

        multiplierText.text = "x0.00";
        multiplierText.color = red;
        chosenMultiplierText.text = "";
        winMoneyText.text = "";

        history = new List<float>();
    }


    void Update()
    {

    }

    private Vector3 v;

    public IEnumerator DrawGraph()
    {
        for (int i = 0; i < numPoints; i++)
        {
            if (i == 0)
            {
                v = Vector3.zero;
            }
            else
            {
                v.x += 1f;
                v.y += Random.Range(0.3f, 1f);
            }
            lineRenderer.positionCount = i + 1;
            lineRenderer.SetPosition(i, v);

            yield return new WaitForSeconds(0.1f);

        }
    }

    public IEnumerator IncrementMultiplier()
    {
        while (true)
        {
            multiplier *= 1.03f;
            multiplierText.text = string.Format("x{0:F2}", multiplier);
            if(multiplier >= 2f)
            {
                multiplierText.color = green;
            }

            if (running)
            {
                if (multiplier >= targetMultiplier)
                {
                    EndGame();
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public float GetRandomValue()
    {
        float rand = Random.value;
        if (rand <= 0.5)
        {
            return Random.Range(1.01f, 2f);
        }
        if (rand <= 0.88)
        {
            return Random.Range(2f, 3f);
        }
        return Random.Range(3f, 100f);
    }


    IEnumerator Zoom()
    {
        while (true)
        {
            cam.orthographicSize += 0.01f;
            camPosition.Translate(new Vector3(0.02f, 0.015f, 0));
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ChangeGrid()
    {
        while (true)
        {
            yield return new WaitForSeconds(7f);
            int gridSize = mat.GetInt("_GridSize");
            mat.SetInt("_GridSize", gridSize / 2);
        }
    }

    public void StartGame()
    {
        if(dataController.money >= BetButton.betMoney)
        {
            running = true;

            dataController.money -= BetButton.betMoney;

            mat.SetInt("_GridSize", initGridSize);
            camPosition.position = initCamPosition;
            cam.orthographicSize = initOrthographicSize;
            lineRenderer.positionCount = 0;
            multiplier = 1;
            multiplierText.text = "x0.00";
            multiplierText.color = red;
            StopButton.chosenMultiplier = 0;
            chosenMultiplierText.text = "";
            winMoneyText.text = "";

            targetMultiplier = GetRandomValue();
            Debug.Log(targetMultiplier);

            StartCoroutine(DrawGraph());
            StartCoroutine(IncrementMultiplier());
            StartCoroutine(Zoom());
            StartCoroutine(ChangeGrid());

            startButton.interactable = false;
        }
        else
        {
            notEnoughMoneyPanel.SetActive(true);
        }
        
    }

    public void EndGame()
    {
        running = false;
        StopAllCoroutines();

        if(StopButton.chosenMultiplier >= 2f)
        {
            dataController.money += (int)(BetButton.betMoney * StopButton.chosenMultiplier);
        }

        WriteHistory();


        startButton.interactable = true;
        stopButton.interactable = true;
    }


    void WriteHistory()
    {
        historyText.text = "";
        if (history.Count >= 8)
        {
            history.RemoveAt(0);
        }
        history.Add(multiplier);
        foreach (float f in history)
        {
            if (f >= 2) 
                historyText.text += string.Format("<color=#00E522>x{0:F2}</color>  ", f);
            else
                historyText.text += string.Format("<color=#ff0000>x{0:F2}</color>  ", f);
        }
    }



}
