using UnityEngine;

namespace Interaction
{
    public class IOTaskTrigger : InteractiveObject
    {
        public bool IsCompleted { get; private set; }
        public string TaskTitle => taskTitle;

        [SerializeField] private string taskTitle;

        protected override void Start()
        {
            base.Start();
            IsCompleted = false;
        }

        protected override void OnInteractionEnd(bool successful)
        {
            IsCompleted = successful;
            base.OnInteractionEnd(successful);
        }
    }
}