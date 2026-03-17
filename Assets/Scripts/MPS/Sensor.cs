using UnityEngine;

namespace MPS
{
    /// <summary>
    /// 근접, 금속 센서에 따라 물체가 감지되면 메쉬랜더러의 색상을 바꿔준다.
    /// ※ 가상의 센서를 PLC의 Input(X디바이스)으로 사용
    /// 속성 : enum 센서타입 열거형, PLC로 보내는 신호, 메쉬랜더러
    /// </summary>
    public class Sensor : MonoBehaviour
    {
        public enum SensorType
        {
            근접센서,
            금속감지센서,
            유동형센서
        }
        public SensorType sensorType = SensorType.근접센서;
        public bool sensorSignal;
        MeshRenderer mr;

        private void Start()
        {
            mr = GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(sensorType == SensorType.금속감지센서)
            {
                if(other.tag == "금속")
                {
                    sensorSignal = true;
                    mr.material.color = new Color(1, 0, 0, 0.7f);
                }
            }

            else
            {
                sensorSignal = true;
                mr.material.color = new Color(1, 0, 0, 0.7f);

            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            sensorSignal = false;
            mr.material.color = new Color(0, 1, 0, 0.7f);
        }
    }

}

