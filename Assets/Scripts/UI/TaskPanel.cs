using Core.Game_Systems.Task_System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TaskPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private Toggle checkBox;

        public void UpdateData(Task task)
        {
            gameObject.SetActive(true);
            
            title.SetText(task.Title);
            if (task.IsCompleted)
            {
                title.fontStyle = FontStyles.Strikethrough;
                checkBox.isOn = true;
            }
            else
            {
                title.fontStyle = FontStyles.Normal;
                checkBox.isOn = false;
            }
        }
        
    }
}