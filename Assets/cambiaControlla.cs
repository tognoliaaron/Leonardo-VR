using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Valve.VR.InteractionSystem.Sample
{
    public class cambiaControlla : MonoBehaviour
    {
        public HoverButton hoverButton;

        public string nomeScena;

        private void Start()
        {
            hoverButton.onButtonDown.AddListener(OnButtonDown);
        }

        private void OnButtonDown(Hand hand)
        {
            SceneManager.LoadScene(nomeScena, LoadSceneMode.Single);
        }

       
    }
}