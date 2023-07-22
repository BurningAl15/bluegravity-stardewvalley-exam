using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationIndexGroupComparer : IComparer<AnimationIndexGroup>
{
    public int Compare(AnimationIndexGroup x, AnimationIndexGroup y)
    {
        // Compare the bodyPartIndex values of AnimationIndexGroup objects.
        return x.bodyPartIndex.CompareTo(y.bodyPartIndex);
    }
}

[Serializable]
public class AnimationParts
{
    public List<Sprite> hairSprites = new List<Sprite>();
    public List<Sprite> headSprites = new List<Sprite>();
    public List<Sprite> bodySprites = new List<Sprite>();
    public List<Sprite> armsSprites = new List<Sprite>();
    public List<Sprite> legsSprites = new List<Sprite>();
    public List<Sprite> shoesSprites = new List<Sprite>();

    public AnimationParts(List<Sprite> _hairSprites,
    List<Sprite> _headSprites,
    List<Sprite> _bodySprites,
    List<Sprite> _armsSprites,
    List<Sprite> _legsSprites,
    List<Sprite> _shoesSprites)
    {
        hairSprites = _hairSprites;
        headSprites = _headSprites;
        bodySprites = _bodySprites;
        armsSprites = _armsSprites;
        legsSprites = _legsSprites;
        shoesSprites = _shoesSprites;
    }

    public List<Sprite> GetAnimationPart(int _index)
    {
        List<Sprite> tempIndex = new List<Sprite>();
        switch (_index)
        {
            default:
            case 0:
                tempIndex = hairSprites;
                break;
            case 1:
                tempIndex = headSprites;
                break;
            case 2:
                tempIndex = bodySprites;
                break;
            case 3:
                tempIndex = armsSprites;
                break;
            case 4:
                tempIndex = legsSprites;
                break;
            case 5:
                tempIndex = shoesSprites;
                break;
        }

        return tempIndex;
    }
}

[Serializable]
public class AnimationGroup
{
    public AnimationParts upSprites;
    public AnimationParts downSprites;
    public AnimationParts horizontalSprites;
}

[Serializable]
public class AnimationIndexGroup
{
    public int groupIndex;
    public int partIndex;
    public int bodyPartIndex;

    public AnimationIndexGroup(int _groupIndex, int _partIndex, int _bodyPartIndex)
    {
        groupIndex = _groupIndex;
        partIndex = _partIndex;
        bodyPartIndex = _bodyPartIndex;
    }
}

[CreateAssetMenu(fileName = "New Assets List", menuName = "Asset Data")]
public class AssetsList : ScriptableObject
{
    [SerializeField] List<AnimationGroup> animGroup;
    public List<AnimationGroup> AnimGroup
    {
        get
        {
            return animGroup;
        }
    }

    public AnimationGroup GetAnimGroup(int _groupIndex)
    {
        return animGroup[_groupIndex];
    }
}
