using UnityEngine;

namespace Tangelo.Test.Events
{
	public class EventDispatcher : MonoBehaviour{

		public delegate void ObjectNotification(int p_spawned, int p_pool);
		public event ObjectNotification ObjectNotificationEvent;

		public void OnNotification (int p_spawned, int p_pool) {
			if(ObjectNotificationEvent != null) 
				ObjectNotificationEvent.Invoke(p_spawned, p_pool);
		}
	}

}
