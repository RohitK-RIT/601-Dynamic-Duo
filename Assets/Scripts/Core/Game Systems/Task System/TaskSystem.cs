using System;
using System.Collections.Generic;
using System.Linq;
using Interaction;
using UnityEngine;

namespace Core.Game_Systems.Task_System
{
    public class TaskSystem : MonoBehaviour
    {
        public static TaskSystem Instance { get; private set; }
        public bool AllTasksCompleted => taskList.All(task => task.IsCompleted);
        
        [SerializeField] private List<Task> taskList;

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }

    [Serializable]
    public class Task
    {
        public bool IsCompleted => taskTrigger.IsCompleted;
        
        [SerializeField] private IOTaskTrigger taskTrigger;
    }
}