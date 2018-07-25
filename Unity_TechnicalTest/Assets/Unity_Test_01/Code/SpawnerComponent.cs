using System.Collections.Generic;
using UnityEngine;
using Tangelo.Test.Events;

namespace Tangelo.Test.Components
{
    public interface ISpawnerComponent
    {
        void Spawn(Transform position);
        void Recycle();
    }

    public class SpawnerComponent : MonoBehaviour, ISpawnerComponent {
	 
		private List<GameObject> m_spawnedCubes = new List<GameObject>();
		private List<GameObject> m_poolCubes = new List<GameObject>();
		private EventDispatcher m_eventDispatcher;

		void Start () {
			m_eventDispatcher = FindObjectOfType<EventDispatcher>();

		}

        // QUESTION 3: We want to implement a Spawn system using a Pool. Objects should be recycled
        // Re-implement this method to use a Pool instead of creation on-demand.
        public void Spawn (Transform p_position)
		{
			if (m_poolCubes.Count == 0)
				InstantiateCube();
			else
				GetCubeFromPool();

            NotifyPoolStatus();
        }

        // QUESTION 5: Implement here the recycling of the object. Once you called this method, the object should be added to Pool.
        // If the object is in the pool, it should be invisible
		public void Recycle ()
		{
			if (m_spawnedCubes.Count > 0)
			{
				CubeToPool ();
				NotifyPoolStatus ();
			}
        }

        // QUESTION 9: We want to notify how many objects are spawned and the number of items in the pool.
        // SendMessage or Static Methods to notify the status to the GameManager, is not allowed.
		private void NotifyPoolStatus()
        {
	        m_eventDispatcher.OnNotification(m_spawnedCubes.Count, m_poolCubes.Count);
        }

        private void InstantiateCube ()
		{
			GameObject spawnedObject = GameObject.CreatePrimitive (PrimitiveType.Cube);
			spawnedObject.AddComponent<SpinnerComponent> ();
			m_spawnedCubes.Add (spawnedObject);
		}

		private void GetCubeFromPool ()
		{
			var t_firstAvailablePoolCube = m_poolCubes[0];
			m_spawnedCubes.Add(t_firstAvailablePoolCube);
			m_poolCubes.Remove(t_firstAvailablePoolCube);
			t_firstAvailablePoolCube.SetActive(true);
		}

		private void CubeToPool ()
		{
			var t_firstAvailableCube = m_spawnedCubes[0];
			m_poolCubes.Add(t_firstAvailableCube);
			m_spawnedCubes.Remove(t_firstAvailableCube);
			t_firstAvailableCube.SetActive(false);
		}

    }
}