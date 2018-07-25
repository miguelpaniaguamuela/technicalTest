using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tangelo.Test.Interface {

	public class FrameRateShower : MonoBehaviour {
		
		#region Variables
		public TextMeshProUGUI FPSvalue;

		private int m_frameCounter = 0;
		private float m_timeCounter = 0.0f;
		private float m_lastFramerate = 0.0f;
		private const float m_refreshTime = 0.5f;
		#endregion
		
		#region Handle FPS
		private void Update() {
			if(m_timeCounter < m_refreshTime) {
				m_timeCounter += Time.deltaTime;
				++m_frameCounter;
			}
			else {
				m_lastFramerate = m_frameCounter / m_timeCounter;
				m_frameCounter = 0;
				m_timeCounter = 0.0f;
			}

			FPSvalue.text = m_lastFramerate.ToString(CultureInfo.InvariantCulture);
		}
		#endregion
	}
}