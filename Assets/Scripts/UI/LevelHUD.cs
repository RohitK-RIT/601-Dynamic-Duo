using System.Collections.Generic;
using Core.Game_Systems.Level_System;
using Core.Game_Systems.Task_System;
using Core.Game_Systems.UI_System;
using UnityEngine;

namespace UI
{
    public class LevelHUD : UIPage
    {
        public override PageID PageID => PageID.LevelHUD;
        public List<Task> Tasks => _taskSystem.TaskList;

        [SerializeField] private List<TaskPanel> panels;

        private TaskSystem _taskSystem;

        private void OnEnable()
        {
            _taskSystem = LevelSystem.Instance.TaskSystem;
        }

        private void OnDisable()
        {
            _taskSystem = null;
        }

        private void Update()
        {
            if (panels.Count < Tasks.Count)
            {
                var panelsToAdd = Tasks.Count - panels.Count;
                var taskPanel = panels[^1];
                for (int i = 0; i < panelsToAdd; i++)
                {
                    panels.Add(Instantiate(taskPanel, taskPanel.transform.parent));
                }
            }
            else if (Tasks.Count < panels.Count)
            {
                for (var i = Tasks.Count; i < panels.Count; i++)
                {
                    panels[i].gameObject.SetActive(false);
                }
            }

            for (var i = 0; i < Tasks.Count; i++)
            {
                panels[i].UpdateData(Tasks[i]);
            }
        }
    }
}