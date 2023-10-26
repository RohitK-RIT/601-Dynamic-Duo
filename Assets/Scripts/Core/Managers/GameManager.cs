using UnityEngine;

namespace Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = new GameObject("Game Manager").AddComponent<GameManager>();
                }

                return _instance;
            }
            private set => _instance = value;
        }

        private static GameManager _instance;
        public SceneManager SceneManager { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager = new SceneManager();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}