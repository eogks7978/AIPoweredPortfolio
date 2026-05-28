using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;

    public class ExamplePlayer : MonoBehaviour
    {
        public ExampleCharacterController Character;


        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
