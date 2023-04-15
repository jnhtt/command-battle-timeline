using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace Chapter3
{
    public static class TimelineUtils
    {
        public static void ChangeAnimationClip(PlayableDirector director, string trackName, string clipDisplayName, AnimationClip clip)
        {
            var timelineAsset = director.playableAsset as TimelineAsset;
            if (timelineAsset != null)
            {
                IEnumerable<TrackAsset> tracks = timelineAsset.GetOutputTracks();
                TrackAsset track = tracks.FirstOrDefault(x => x.name == trackName);
                if (track != null)
                {
                    foreach (var timelineClip in track.GetClips())
                    {
                        if (timelineClip.displayName == clipDisplayName)
                        {
                            ((AnimationPlayableAsset)timelineClip.asset).clip = clip;
                        }
                    }
                }
            }
        }
    }
}
