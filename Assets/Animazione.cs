using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animazione : MonoBehaviour
{
    public GameObject freccia;
    public int maxsu=10;
    public string nome;

    private bool su = true;
    private float mingiu;

    // Start is called before the first frame update
    void Start()
    {
        mingiu = freccia.transform.position.y;
        InvokeRepeating("anima", 0.1f, 0.1f);
    }

    // Update is called once per frame
    private void Update()
    {
        /*foreach (Hand hand in player.hands)
        {
            if (menuAction.GetStateDown(hand.handType))
            {
                player = InteractionSystem.Player.instance;
                Destroy(this);
                SceneManager.LoadScene("scelta", LoadSceneMode.Single);
            }
        }*/
    }

    private void anima()
    {
        float distanzaSpostamento = 0.5f;   
        Quaternion tempRot = freccia.transform.rotation;
        Vector3 tempEuler = tempRot.eulerAngles;
        Vector3 tempPos = freccia.transform.position;
        tempEuler.y += 10;
        tempRot.eulerAngles = tempEuler;
        if(su)
        {
            tempPos.y += distanzaSpostamento;
            if (tempPos.y > mingiu+maxsu)
                su = false;
        }
        else
        {
            tempPos.y -= distanzaSpostamento;
            if (tempPos.y < mingiu)
                su = true;
        }
        freccia.transform.rotation = tempRot;
        freccia.transform.position = tempPos;

    }
}
