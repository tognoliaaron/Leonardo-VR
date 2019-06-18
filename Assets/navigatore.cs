using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class navigatore : MonoBehaviour
    {
        public GameObject freccia;
        public GameObject destinazione;
        public List<GameObject> destinazioni;
        public TextMesh testo;
        public SteamVR_ActionSet actionSetEnable;
        public SteamVR_Action_Boolean cambiaDestinazione = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("cambiadestinazione");
        private int indice=-1;
        private bool off = true;
        private Player player;

        // Start is called before the first frame update
        void Start()
        {
            actionSetEnable.Activate();
            player = InteractionSystem.Player.instance;
            if (player == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> Teleport: No Player instance found in map.");
                Destroy(this.gameObject);
                return;
            }
            freccia.SetActive(false);
            testo.text = "";
            
            foreach (GameObject dest in destinazioni)
            {
                dest.SetActive(false);
            }

        }

        // Update is called once per frame
        void Update()
        {
           // foreach (Hand hand in player.hands)
           // {
                Hand hand = player.hands[1];
                player = InteractionSystem.Player.instance;
                if (cambiaDestinazione.GetStateUp(hand.handType))
                {
                    //Debug.LogWarning();
                    if(indice!=-1)
                    {
                        destinazioni[indice].SetActive(false);
                    }
                    indice++;
                    if (indice == destinazioni.Count)
                    {
                        indice = -1;
                        freccia.SetActive(false);
                        testo.text = "";
                    }
                    else
                    {
                        freccia.SetActive(true);
                        destinazioni[indice].SetActive(true);
                        destinazione = destinazioni[indice];

                        testo.text = destinazione.GetComponent<Animazione>().nome;
                    }

                }
           // }
            if (freccia.activeSelf)
            {
                freccia.transform.LookAt(destinazione.transform);
                freccia.transform.Rotate(new Vector3(0, 180, 0));
            }
        }
    }
}
