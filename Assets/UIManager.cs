using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class UIManager : MonoBehaviour
{
    public Text task, earnMoney;
    public Text amount, dollar;
    public int[] moneyToAdd = new int[12];
    public int[] increments = new int[12];
    public Button makeMoney;
    public GameObject makeMoneyBtn, stopBtn, buyHouseBtn, buyCarBtn, buyHouseBeachBtn, bankruptcy, taskLabel, earnMoneyLabel, finalSentenceLabel;
    public GameObject audioClip, redPanel;
    public GameObject houseBought, carBought, holidayHouseBought;
    public GameObject stoppedLabel, credits;

    private int currentClick, currentMoney, totalMoneyOnClick;
    private bool computing;

    private void Start()
    {
        currentClick = 1;
        currentMoney = 0;

        buyCarBtn.GetComponent<Button>().interactable = false;
        buyHouseBeachBtn.GetComponent<Button>().interactable = false;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnClickMakeMoney()
    {
        stopBtn.GetComponent<Button>().interactable = false;
        audioClip.SetActive(true);
        makeMoney.interactable = false;
            
        if (currentClick != moneyToAdd.Length)
        {
            totalMoneyOnClick = currentMoney + moneyToAdd[currentClick];
            StartCoroutine(WaitForItAdd());
        }
        else
        {
            totalMoneyOnClick = 0;
            Bankruptcy();
        }
    }

    public void Bankruptcy()
    {
        bankruptcy.SetActive(true);
        redPanel.SetActive(true);
        makeMoneyBtn.SetActive(false);
        stopBtn.SetActive(false);
        buyHouseBtn.SetActive(false);
        buyCarBtn.SetActive(false);
        buyHouseBeachBtn.SetActive(false);
        houseBought.SetActive(false);
        carBought.SetActive(false);
        holidayHouseBought.SetActive(false);
        task.text = "";
        earnMoney.text = "";
        finalSentenceLabel.SetActive(true);
        credits.SetActive(true);
        amount.text = "0";
    }

    public void OnClickStop()
    {
        makeMoneyBtn.SetActive(false);
        stopBtn.SetActive(false);
        buyHouseBtn.SetActive(false);
        buyCarBtn.SetActive(false);
        buyHouseBeachBtn.SetActive(false);
        houseBought.SetActive(false);
        carBought.SetActive(false);
        holidayHouseBought.SetActive(false);
        task.text = "";
        earnMoney.text = "";
        finalSentenceLabel.SetActive(false);
        amount.text = "";
        dollar.text = "";
        stoppedLabel.SetActive(true);
        credits.SetActive(true);
    }

    public void OnClickBuyHouse()
    {
        // 120k
        buyHouseBtn.SetActive(false);
        int cost = 120000;
        totalMoneyOnClick = currentMoney - cost;
        StartCoroutine(WaitForItSubtract(10000, "Now you must buy a car.", "Earn more money.", "house"));
    }

    public void OnClickBuyCar()
    {
        // 70k
        buyCarBtn.SetActive(false);
        int cost = 70000;
        totalMoneyOnClick = currentMoney - cost;
        StartCoroutine(WaitForItSubtract(1000, "You must buy a holiday house.", "Earn more money.", "car"));
    }

    public void OnClickBuyHouseBeach()
    {
        // 2kk
        buyHouseBeachBtn.SetActive(false);
        int cost = 2000000;
        totalMoneyOnClick = currentMoney - cost;
        StartCoroutine(WaitForItSubtract(400000, "", "", "holiday"));
    }

    public IEnumerator WaitForItAdd()
    {
        bool house = buyHouseBtn.GetComponent<Button>().IsInteractable();
        bool car = buyCarBtn.GetComponent<Button>().IsInteractable();
        bool beach = buyHouseBeachBtn.GetComponent<Button>().IsInteractable();

        buyHouseBtn.GetComponent<Button>().interactable = false;
        buyCarBtn.GetComponent<Button>().interactable = false;
        buyHouseBeachBtn.GetComponent<Button>().interactable = false;

        while (currentMoney <= totalMoneyOnClick)
        {
            amount.text = currentMoney.ToString();
            currentMoney += increments[currentClick];

            yield return null;
        }

        earnMoney.fontSize += 2;
        currentMoney = totalMoneyOnClick;
        stopBtn.GetComponent<Button>().interactable = true;
        buyHouseBtn.GetComponent<Button>().interactable = house;
        buyCarBtn.GetComponent<Button>().interactable = car;
        buyHouseBeachBtn.GetComponent<Button>().interactable = beach;
        audioClip.SetActive(false);
        makeMoney.interactable = true;
        currentClick++;

        if (currentClick == 2)
        {
            stopBtn.SetActive(true);
        }

        if (currentClick == 3)
        {
            buyHouseBtn.SetActive(true);
        }

        if (currentClick == 6)
        {
            buyCarBtn.SetActive(true);
        }

        if (currentClick == 10)
        {
            buyHouseBeachBtn.SetActive(true);
        }   
    }

    public IEnumerator WaitForItSubtract(int increment, string newTask, string newEarn, string whatAmIBuying)
    {
        task.text = "";
        earnMoney.text = "";

        while (currentMoney >= totalMoneyOnClick)
        {
            amount.text = currentMoney.ToString();
            currentMoney -= increment;

            yield return null;
        }

        currentMoney = totalMoneyOnClick;
        stopBtn.GetComponent<Button>().interactable = true;
        makeMoney.interactable = true;

        if (whatAmIBuying == "house")
        {
            buyCarBtn.GetComponent<Button>().interactable = true;
            houseBought.SetActive(true);
        }
        if (whatAmIBuying == "car")
        {
            buyHouseBeachBtn.GetComponent<Button>().interactable = true;
            carBought.SetActive(true);
        }
        if (whatAmIBuying == "holiday")
        {
            holidayHouseBought.SetActive(true);
        }

        yield return new WaitForSeconds(1f);
        task.text = newTask;
        earnMoney.text = newEarn;
    }
}
