using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

namespace Chapter3
{
    public class ActionExecute : BaseActionExecute
    {
        [SerializeField] private ReactionExecute reactionExecutePrefab;
        [SerializeField] private PlayableDirector director;
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        [SerializeField] private Cinemachine.CinemachineTargetGroup targetGroup;

        private ActionData actionData;
        private Coroutine coroutine;
        private List<ReactionExecute> reactionList;

        public override void Play(ActionData data, System.Action onFinish)
        {
            actionData = data;

            var animatorBinding = director.playableAsset.outputs.First(c => c.streamName == "AnimationTrack");
            director.SetGenericBinding(animatorBinding.sourceObject, data.actor.GetComponent<Animator>());

            var moveBinding = director.playableAsset.outputs.First(c => c.streamName == "MoveTrack");
            director.SetGenericBinding(moveBinding.sourceObject, data.actor.transform);

            var dir = actionData.actor.transform.position - actionData.receiverList[0].transform.position;
            start.position = actionData.actor.transform.position;
            end.position = actionData.receiverList[0].transform.position + 0.7f * dir.normalized;

            TimelineUtils.ChangeAnimationClip(director, "AnimationTrack", "Attack", data.clip);

            coroutine = StartCoroutine(PlayCoroutine(onFinish));
        }

        private IEnumerator PlayCoroutine(System.Action onFinish)
        {
            if (targetGroup != null)
            {
                foreach (var r in actionData.receiverList)
                {
                    targetGroup.AddMember(r.transform, 1f, 0f);
                }
            }
            director.Play();
            yield return new WaitForSeconds((float)director.duration);
            if (targetGroup != null)
            {
                foreach (var r in actionData.receiverList)
                {
                    targetGroup.RemoveMember(r.transform);
                }
            }
            onFinish?.Invoke();
        }

        public void ExecuteDamage()
        {
            reactionList = new List<ReactionExecute>();
            foreach (var r in actionData.receiverList)
            {
                var go = GameObject.Instantiate(reactionExecutePrefab);
                var reaction = go.GetComponent<ReactionExecute>();
                if (reaction != null)
                {
                    var data = new ActionData();
                    data.actor = r;
                    data.actionValue = CaluculateDamage();
                    reaction.Play(data, () =>
                    {
                        GameObject.Destroy(reaction.gameObject);
                    });
                }
                
            }
        }


        private int CaluculateDamage()
        {
            return UnityEngine.Random.Range(0, 100);
        }
    }
}
