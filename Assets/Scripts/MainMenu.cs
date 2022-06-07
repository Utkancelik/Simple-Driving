using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AndroidNotificationHandler AndroidNotificationHandler;

    [SerializeField] private TMP_Text HighScoreText;
    [SerializeField] private TMP_Text PlayButtonText;

    [SerializeField] private int energyRechargeDuration = 1; // 1 minutes to recharge the energy amount
    [SerializeField] private int maxEnergyAmount = 5; // max amount of energy we can have

    private int energyAmount; // how much do we have now

    private const string energyAmountKey = "EnergyAmount";
    private const string energyReadyKey = "EnergyReady";

    private void Start()
    {
        // displays the highscore you get until now
        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey);
        HighScoreText.text = $"Highscore : {highScore}";
        // how much energy we have
        energyAmount = PlayerPrefs.GetInt(energyAmountKey, maxEnergyAmount);
        // if we are out of energy
        if (energyAmount == 0)
        {
            // then check if we passed enough time to recharge
            // if we passed enough time then recharge the energy

            // check when energy should be regenerated
            string energyReadyTimeInStringForm = PlayerPrefs.GetString(energyReadyKey, string.Empty);

            if(energyReadyTimeInStringForm == string.Empty) { return; }

            DateTime energyReadyDateTime = DateTime.Parse(energyReadyTimeInStringForm);

            // eðer þu an enerjinin yenilenmesi için gereken zamaný geçtiysek
            if (DateTime.Now > energyReadyDateTime)
            {
                // enerji max olsun
                energyAmount = maxEnergyAmount;
                PlayerPrefs.SetInt(energyAmountKey, energyAmount);
            }
        }

        PlayButtonText.text = $"Play ({energyAmount})";
    }
    public void LoadScene_Game()
    {
        // if he has not enough energy
        if (energyAmount < 1)
        {
            //then do nothing
            return;
        }
        else
        {
            // decrease energy amount
            energyAmount--;
            // set playerprefs
            PlayerPrefs.SetInt(energyAmountKey, energyAmount);



            // energy'nin tekrar dolacaðý zamaný belirleyelim
            // 1 dakika sonra dolmuþ olsun 
            // bunun kontrolü start fonksiyonunda yapýlýyor
            // eðer enerjimiz bitti ise (enerji = 0)
            // o haLde þu an belirleyeceðimiz zamana (which is 1 minutes after from now) ulaþýp ulaþmadýðýmýza bakacak
            // eðer ulaþtýysak enerjimiz dolacak


            // if we are out of energy (energy = 0)
            if (energyAmount == 0)
            {
                // then determine when we charge up
                // determine the time that energyRechargeDuration after from now 
                DateTime whenEnergyBeRecharged = DateTime.Now.AddMinutes(energyRechargeDuration);
                // assign it to the playerPrefs that we use for checking if we passed that time
                PlayerPrefs.SetString(energyReadyKey, whenEnergyBeRecharged.ToString());
                //send notification when energy is up and do it only in android targets
#if UNITY_ANDROID
                AndroidNotificationHandler.ScheduleNotification(whenEnergyBeRecharged);
#endif
            }
            
            
            // Loading the game screen in index 1
            SceneManager.LoadScene(1);
        }
        
    }
}
