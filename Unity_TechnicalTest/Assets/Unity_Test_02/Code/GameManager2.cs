using Tangelo.Test.Components;
using Tangelo.Test.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tangelo.Test.Managers
{
    public class GameManager2 : MonoBehaviour
    {
        [SerializeField]
        private int m_MaxMassiveSpawningObjects; // Minimum number: 200

        [SerializeField]
        private int m_MaxRecycleObjects; // Minimum number: 200

		private EventDispatcher m_eventDispatcher;
		private SpawnerComponent2 m_pool;
        private GameStatus m_CurrentStatus = GameStatus.Loading;
        private GameStatus m_Status;
		public TMP_Text m_spawnedText;
		public TMP_Text m_poolText;
		public TMP_InputField m_spawnInput;
		public TMP_InputField m_recycleInput;
		public Slider m_sliderInstance;

        private void Awake()
        {
			m_eventDispatcher = FindObjectOfType<EventDispatcher>();
	        m_eventDispatcher.ObjectNotificationEvent += ShowNotification;
            m_CurrentStatus = GameStatus.Loading;
			m_pool = GetComponent<SpawnerComponent2>();
			m_sliderInstance.minValue = 0;
			m_sliderInstance.wholeNumbers = true;
			changeSlide(1, 0);

        }

        private void Update ()
		{
			if (Input.GetKeyDown ("space")) 
			{
				m_pool.Spawn(null);
				m_Status = GameStatus.Spawning;
			}
        	else
				m_Status = GameStatus.Playing;

            if (m_CurrentStatus == m_Status)
                return;

            m_CurrentStatus = m_Status;
        }

        // QUESTION 8: Implement here a loading process (using a UI Slider) to show how the Pool is being populated.
        private void PreparePools()
        {

        }

        // QUESTION 6: Implement here a massive spawning of objects.
        // FPS Can't drop below 50fps when MassiveSpawning is being executed.
        // If there are not enough objects in the Pool, increment the size. During the increment of the Pool size,
        // the FPS can't drop below 30fps
        // Implement a UI Button to execute this action
        public void MassiveSpawn ()
		{
			for (int i = 0; i < m_MaxMassiveSpawningObjects; i++)
			{
				m_pool.Spawn(null);
			}
        }

        // QUESTION 7: Implement here a massive recycling of the objects currently visible in the screen
        // FPS Can't drop below 50fps when MassiveRecycle is being executed
        // Implement a UI Button to execute this action
        public void MassiveRecycle()
        {
			for (int i = 0; i < m_MaxRecycleObjects; i++)
			{
				m_pool.Recycle();
			}
        }

        public void ShowNotification(int p_spawned, int p_pool)
        {
			m_spawnedText.text = "SPAWNED " + p_spawned.ToString();
			m_poolText.text = "POOLED " + p_pool.ToString();
			changeSlide(p_spawned + p_pool, p_pool);
        }

        private void changeSlide (int p_max, int p_value)
		{
			m_sliderInstance.maxValue = p_max;
			m_sliderInstance.value = p_value;
		}

		public void setMassiveSpawningValue()
		{
			if(m_spawnInput.text != null)
				m_MaxMassiveSpawningObjects = int.Parse(m_spawnInput.text);
		}

		public void setRecycleValue()
		{
			if(m_spawnInput.text != null)
				m_MaxRecycleObjects = int.Parse(m_recycleInput.text);
		}

    }
}