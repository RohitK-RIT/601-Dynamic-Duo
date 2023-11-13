using System.Collections;
using Core.Game_Systems.Level_System;
using Core.Game_Systems.Task_System;
using UnityEngine;
using CharacterController = Core.Player.CharacterController;

namespace Interaction.Doors
{
    public class IOFinalDoorSwitch : InteractiveObject
    {
        public override bool Interactable => TaskSystem.Instance.AllTasksCompleted;

        [SerializeField] private GameObject controlDoor;

        private AudioSource _audioSource;

        protected override void Start()
        {
            base.Start();
            _audioSource = GetComponent<AudioSource>();
        }

        public override bool OnHandleInteractee(CharacterController controller)
        {
            if (!base.OnHandleInteractee(controller))
                return false;

            if (InteractingCharacters.Count >= 2)
                StartCoroutine(DoorOpened());

            return true;
        }

        private IEnumerator DoorOpened()
        {
            // Can play and wait for some animation here.
            yield return null;

            Destroy(controlDoor);
            if (_audioSource)
                _audioSource.Play();
            new LevelCompletedEvent().Raise();
        }
    }
}