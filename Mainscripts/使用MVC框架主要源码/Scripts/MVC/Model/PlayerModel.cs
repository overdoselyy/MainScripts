using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 作为一个唯一的数据类型 一般情况 要不自己是单例模式对象
/// 要不自己存在 一个单例模式对象中
/// </summary>
public class PlayerModel
{
    //数据内容
    private string playerName;
    private int lev;
    private int money;
    private int gem;
    private int power;

    private int def;
    private int atk;
    private int hp;
    private int luck;
    private int miss;
    private int crit;

    public string PlayerName{
        get {
            return playerName;
        }
    }
    public int Lev
    {
        get
        {
            return lev;
        }
    }
    public int Money
    {
        get
        {
            return money;
        }
    }
    public int Gem
    {
        get
        {
            return gem;
        }
    }
    public int Power
    {
        get
        {
            return power;
        }
    }
    public int Atk
    {
        get
        {
            return atk;
        }
    }
    public int Def
    {
        get
        {
            return def;
        }
    }
    public int Hp
    {
        get
        {
            return hp;
        }
    }
    public int Crit
    {
        get
        {
            return crit;
        }
    }
    public int Miss
    {
        get
        {
            return miss;
        }
    }
    public int Luck
    {
        get
        {
            return luck;
        }
    }
    //通知外部更新的事件
    //通过他和外部建立联系 而不是获取外部面板
    private event UnityAction<PlayerModel> updateEvent;

    //在外部第一次获取这个数据 如何获取
    //通过单例模式 来达到数据的唯一性 和数据的获取
    private static PlayerModel data = null;

    public static PlayerModel Data
    {
        get
        {
            if (data == null)
            {
                data = new PlayerModel();
                data.Init();
            }
            return data;
        }
    }

    //数据相关的操作
    //初始化
    public void Init() 
    {
        playerName = PlayerPrefs.GetString("PlayerName", "旺旺");
        lev = PlayerPrefs.GetInt("PlayerLev", 1);
        money = PlayerPrefs.GetInt("PlayerMoney", 666);
        gem = PlayerPrefs.GetInt("PlayerGem", 555);
        power = PlayerPrefs.GetInt("PlayerPower", 888);

        hp = PlayerPrefs.GetInt("PlayerHp", 100);
        atk = PlayerPrefs.GetInt("PlayerAtk", 555);
        def = PlayerPrefs.GetInt("PlayerDef", 666);
        miss = PlayerPrefs.GetInt("PlayerMiss", 666);
        luck = PlayerPrefs.GetInt("PlayerLuck", 555);
        crit = PlayerPrefs.GetInt("PlayerCrit", 888);
    }
    //更新 升级
    public void LevUp() 
    {
        //升级 改变内容
        lev += 1;
        hp += lev;
        atk += lev;
        def += lev;
        crit += lev;
        miss += lev;
        luck += lev;

        //改变过后保存
        SaveData();
    }
    //保存
    public void SaveData() {
        //把这些数据内容存储在本地
        PlayerPrefs.SetInt("PlayerLev", lev);
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("PlayerMoney", money);
        PlayerPrefs.SetInt("PlayerGem", gem);
        PlayerPrefs.SetInt("PlayerPower", power);

        PlayerPrefs.SetInt("PlayerHp", hp);
        PlayerPrefs.SetInt("PlayerDef", def);
        PlayerPrefs.SetInt("PlayerAtk", atk);
        PlayerPrefs.SetInt("PlayerMiss", miss);
        PlayerPrefs.SetInt("PlayerLuck", luck);
        PlayerPrefs.SetInt("PlayerCrit", crit);

        UpdateInfo();
    }

    //通知外部更新数据的方法
    private void UpdateInfo() {
        //找到对应的使用数据的脚步 去更新数据
        if (updateEvent != null) {
            updateEvent(this);
        }
    }

    public void AddEventListener(UnityAction<PlayerModel> function)
    {
        updateEvent += function;
    }

    public void RemoveEventListener(UnityAction<PlayerModel> function)
    {
        updateEvent -= function;

    }
}
