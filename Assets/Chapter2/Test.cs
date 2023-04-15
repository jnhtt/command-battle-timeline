using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chapter2
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private TimelineManager timelineManager;
        [SerializeField] private List<AnimationClip> animationClipList;

        [SerializeField] private GameObject player;
        [SerializeField] private GameObject enemy1;
        [SerializeField] private GameObject enemy2;

        private bool playing = false;

        public void AttackbyPlayer()
        {
            if (playing)
            {
                return;
            }
            playing = true;

            var data = new ActionData();
            data.actor = player;
            data.receiverList = new List<GameObject>() { UnityEngine.Random.Range(0, 2) == 0 ? enemy1 : enemy2 };
            data.actionValue = UnityEngine.Random.Range(0, 100);
            data.clip = animationClipList.ElementAt(UnityEngine.Random.Range(0, animationClipList.Count));
            timelineManager.Play(data, () => { playing = false; });
        }
        public void AttackbyEnemy1()
        {
            if (playing)
            {
                return;
            }
            playing = true;

            var data = new ActionData();
            data.actor = enemy1;
            data.receiverList = new List<GameObject>() { player };
            data.actionValue = UnityEngine.Random.Range(0, 100);
            data.clip = animationClipList.ElementAt(UnityEngine.Random.Range(0, animationClipList.Count));
            timelineManager.Play(data, () => { playing = false; });
        }
        public void AttackbyEnemy2()
        {
            if (playing)
            {
                return;
            }
            playing = true;

            var data = new ActionData();
            data.actor = enemy2;
            data.receiverList = new List<GameObject>() { player };
            data.actionValue = UnityEngine.Random.Range(0, 100);
            data.clip = animationClipList[UnityEngine.Random.Range(0, animationClipList.Count)];
            timelineManager.Play(data, () => { playing = false; });
        }
    }
}
