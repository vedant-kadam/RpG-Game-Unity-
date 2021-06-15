﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {

        if (instance != null)
        {
            
            Destroy(gameObject);
            return;

        }
        //else
        //{
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += LoadData;
        //}
        
       // SceneManager.sceneLoaded += LoadData;
        
    }

    //resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;


    //refrence to various game objects like player npsc etc
    public PlayerMovement player;
    public Weapon _weapon;


    //weapon script reftrence to text ;
    public FloatingTextManager floatingTextManager;


    //tracking variables
    public int money;
    public int experience;
    



    ///Common place to show message
    public void ShowText(string msg,int fontSize,Color color ,Vector3 position, Vector3 motion,float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public bool TryUpgradeWeapon()
    {
        //is weapon level max
        if (weaponPrices.Count <= _weapon.weaponLevel) return false;

        if(money>=weaponPrices[_weapon.weaponLevel])
        {
            money -= weaponPrices[_weapon.weaponLevel];
            _weapon.UpGradeWeapon();
            return true;
        }
        return false;

    }


    //function to save the game data
    public void SaveState()
    {

        string save_data = "";

        save_data += "0" + "|";
        save_data += money.ToString() + "|";
        save_data += experience.ToString() + "|";
        save_data += _weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("saveData", save_data);
    }
    
    //function to Load the data from the game when you open
    public void LoadData( Scene s ,LoadSceneMode mode)
    {
        Debug.Log("load data");
        //SceneManager.sceneLoaded -= LoadData;

        if (!PlayerPrefs.HasKey("saveData"))//if we dont have a key in the game
        {
            return;
        }

        string[] data = PlayerPrefs.GetString("saveData").Split('|');
        money = int.Parse(data[1]);
        experience = int.Parse(data[2]);
       
        _weapon.SetWeaponLevel(int.Parse(data[3]));

    }



}
