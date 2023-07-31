using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MCSaveData
{
    public int sceneIndex, noticeInt;

    public float HP, MP,
        cycleCurrentTime, cycleMaxTime, money;

    public bool isOnJenny, isOnDungeon, ilkGorev, ikinciGorev, ucuncuGorev, dorduncuGorev, besinciGorev, altincigorev,
        yedincigorev, sekizincigorev, dokuzuncugorev, onuncugorev, onbirincigorev, onikincigorev, onUcuncuGorev, 
        onDorduncuGorev, isOnSaffron, isOnDealer, isOnPepper,
        goSaffron, goWP, goPepper, birdn, ikidn, ucdn, talkJenny2ndisOver, mcAttackJenny, goBackkDungeon, jennyActive;

    public float[] position;

    public string missionTxT;


    public DayCycles dayCycle;
    //public GameObject inventory;


    public MCSaveData(mcScript mc)
    {
        HP = mc.currentHealth;
        MP = mc.currentMP;

        money = mc.money;

        noticeInt = mc.noticeInt;
        missionTxT = mc.missionTxT;


        position = new float[2];
        position[0] = mc.transform.position.x;
        position[1] = mc.transform.position.y;

        sceneIndex = mc.sceneIndex;

        dayCycle = mc.dayCycle;
        cycleCurrentTime = mc.cycleCurrentTime;
        cycleMaxTime = mc.cycleMaxTime;

        isOnSaffron = mc.isOnSaffron;
        isOnDealer = mc.isOnDealer;
        isOnPepper = mc.isOnPepper;
        isOnJenny = mc.isOnJenny;
        isOnDungeon = mc.isOnDungeon;

        goSaffron = mc.goSaffron;
        goWP = mc.goWP;
        goPepper = mc.goPepper;

        ilkGorev = mc.ilkGorev;
        ikinciGorev = mc.ikinciGorev;
        ucuncuGorev = mc.ucuncuGorev;
        dorduncuGorev = mc.dorduncuGorev;
        besinciGorev = mc.besinciGorev;
        altincigorev = mc.altincigorev;
        yedincigorev = mc.yedincigorev;
        sekizincigorev = mc.sekizincigorev;
        dokuzuncugorev = mc.dokuzuncugorev;
        onuncugorev = mc.onuncugorev;
        onbirincigorev = mc.onbirincigorev;
        onikincigorev = mc.onikincigorev;
        onUcuncuGorev = mc.onUcuncuGorev;
        onDorduncuGorev = mc.onDorduncuGorev;

        birdn = mc.birdn;
        ikidn = mc.ikidn;
        ucdn = mc.ucdn;
        talkJenny2ndisOver = mc.talkJenny2ndisOver;
        mcAttackJenny = mc.mcAttackJenny;
        goBackkDungeon = mc.goBackkDungeon;
        jennyActive = mc.jennyActive;
    }
}
