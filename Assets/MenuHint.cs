using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Valve.VR.InteractionSystem
{
    public class MenuHint : MonoBehaviour
    {
        // Start is called before the first frame update
        public SteamVR_Action_Boolean menuAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("torna_al_menu");
        private Coroutine hintCoroutine = null;
        private Player player;
        void Start()
        {
            player = InteractionSystem.Player.instance;

            if (player == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> Teleport: No Player instance found in map.");
                Destroy(this.gameObject);
                return;
            }

            Invoke("ShowMenuHint", 5.0f);
        }

        public void ShowMenuHint()
        {
            CancelMenuHint();

            hintCoroutine = StartCoroutine(TeleportHintCoroutine());
        }

        public void CancelMenuHint()
        {
            if (hintCoroutine != null)
            {
                ControllerButtonHints.HideTextHint(player.leftHand, menuAction);
                ControllerButtonHints.HideTextHint(player.rightHand, menuAction);

                StopCoroutine(hintCoroutine);
                hintCoroutine = null;
            }

            CancelInvoke("ShowMenuHint");
        }
        private IEnumerator TeleportHintCoroutine()
        {
            float prevBreakTime = Time.time;
            float prevHapticPulseTime = Time.time;

            while (true)
            {
                bool pulsed = false;

                //Show the hint on each eligible hand
                foreach (Hand hand in player.hands)
                {

                    bool isShowingHint = !string.IsNullOrEmpty(ControllerButtonHints.GetActiveHintText(hand, menuAction));
                   
                    if (!isShowingHint)
                    {
                        ControllerButtonHints.ShowTextHint(hand, menuAction, "Torna al menu");
                        prevBreakTime = Time.time;
                        prevHapticPulseTime = Time.time;
                    }

                    if (Time.time > prevHapticPulseTime + 0.05f)
                    {
                        //Haptic pulse for a few seconds
                        pulsed = true;

                        hand.TriggerHapticPulse(500);
                    }
                    
                    /*else if (isShowingHint)
                    {
                        ControllerButtonHints.HideTextHint(hand, menuAction);
                    }*/
                }

                if (Time.time > prevBreakTime + 3.0f)
                {
                    //Take a break for a few seconds
                    yield return new WaitForSeconds(3.0f);

                    prevBreakTime = Time.time;
                }

                if (pulsed)
                {
                    prevHapticPulseTime = Time.time;
                }

                yield return null;
            }
        }
        // Update is called once per frame
        void Update()
        {
            foreach (Hand hand in player.hands)
            {
                if(menuAction.GetStateDown(hand.handType))
                {
                    player = InteractionSystem.Player.instance;
                    Destroy(this);
                    SceneManager.LoadScene("scelta",LoadSceneMode.Single);
                }
            }
        }
    }

}
