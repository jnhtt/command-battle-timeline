using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Chapter3
{

    public class TimelineManager : MonoBehaviour
    {
        [SerializeField] private BaseActionExecute actionExecute;

        public void Play(ActionData actionData, System.Action onFinish)
        {
            actionExecute?.Play(actionData, onFinish);
        }
    }
}
