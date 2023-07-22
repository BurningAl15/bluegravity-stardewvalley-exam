using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public SpriteRenderer hairRenderer;
    public SpriteRenderer headRenderer;
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer armsRenderer;
    public SpriteRenderer legsRenderer;
    public SpriteRenderer shoesRenderer;


    [SerializeField] AssetsList assetsList;

    [SerializeField] AnimationParts upSprites;
    [SerializeField] AnimationParts downSprites;
    [SerializeField] AnimationParts horizontalSprites;
    [SerializeField] AnimationGroup currentGroup;

    [SerializeField] float frameRate;
    [SerializeField] float idleTime;

    [SerializeField] AnimationGroup tempAnimGroup;

    [SerializeField] AnimationParts tempUpSprites;
    [SerializeField] AnimationParts tempDownSprites;
    [SerializeField] AnimationParts tempHorizontalSprites;

    List<AnimationIndexGroup> up = new List<AnimationIndexGroup>();
    List<AnimationIndexGroup> down = new List<AnimationIndexGroup>();
    List<AnimationIndexGroup> horizontal = new List<AnimationIndexGroup>();


    private void Start()
    {
        currentGroup = assetsList.AnimGroup[0];
        upSprites = currentGroup.upSprites;
        downSprites = currentGroup.downSprites;
        horizontalSprites = currentGroup.horizontalSprites;

        SetClothesFromAssets();
    }

    public void SetClothesFromAssets(int _index = 0)
    {
        up = SaveManager._instance.GetData().CurrentlyUsed_Filter(0);
        down = SaveManager._instance.GetData().CurrentlyUsed_Filter(1);
        horizontal = SaveManager._instance.GetData().CurrentlyUsed_Filter(2);

        currentGroup = GetCustomAnimationGroup(up, down, horizontal);

        upSprites = currentGroup.upSprites;
        downSprites = currentGroup.downSprites;
        horizontalSprites = currentGroup.horizontalSprites;

        hairRenderer.sprite = downSprites.hairSprites[0];
        headRenderer.sprite = downSprites.headSprites[0];
        bodyRenderer.sprite = downSprites.bodySprites[0];
        armsRenderer.sprite = downSprites.armsSprites[0];
        legsRenderer.sprite = downSprites.legsSprites[0];
        shoesRenderer.sprite = downSprites.shoesSprites[0];
    }

    public void HandleFlip(Vector2 _direction)
    {
        if (!hairRenderer.flipX && _direction.x < 0)
        {
            hairRenderer.flipX = true;
            headRenderer.flipX = true;
            bodyRenderer.flipX = true;
            armsRenderer.flipX = true;
            legsRenderer.flipX = true;
            shoesRenderer.flipX = true;
        }
        else if (hairRenderer.flipX && _direction.x > 0)
        {
            hairRenderer.flipX = false;
            headRenderer.flipX = false;
            bodyRenderer.flipX = false;
            armsRenderer.flipX = false;
            legsRenderer.flipX = false;
            shoesRenderer.flipX = false;
        }
    }

    public void SetSprite(Vector2 _direction)
    {
        AnimationParts directionSprites = GetSpriteDirection(_direction);

        if (directionSprites != null)
        {
            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * frameRate);
            int frame = totalFrames % directionSprites.hairSprites.Count;
            SetSprites(directionSprites, frame);
        }
        else
        {
            idleTime = Time.time;
        }
    }

    AnimationParts GetSpriteDirection(Vector2 _direction)
    {
        AnimationParts selectedSprites = null;

        if (_direction.y > 0)
        {
            selectedSprites = upSprites;
        }
        else if (_direction.y < 0)
        {
            selectedSprites = downSprites;
        }
        else
        {
            if (Mathf.Abs(_direction.x) > 0)
            {
                selectedSprites = horizontalSprites;
            }
        }

        return selectedSprites;
    }

    public void SetSprites(AnimationParts _directionSprites, int _frame)
    {
        hairRenderer.sprite = _directionSprites.hairSprites[_frame];
        headRenderer.sprite = _directionSprites.headSprites[_frame];
        bodyRenderer.sprite = _directionSprites.bodySprites[_frame];
        armsRenderer.sprite = _directionSprites.armsSprites[_frame];
        legsRenderer.sprite = _directionSprites.legsSprites[_frame];
        shoesRenderer.sprite = _directionSprites.shoesSprites[_frame];
    }


    public AnimationGroup GetCustomAnimationGroup(List<AnimationIndexGroup> indexGroupUp, List<AnimationIndexGroup> indexGroupDown, List<AnimationIndexGroup> indexGroupHorizontal)
    {
        tempUpSprites = GetAnimationParts(indexGroupUp);
        tempDownSprites = GetAnimationParts(indexGroupDown);
        tempHorizontalSprites = GetAnimationParts(indexGroupHorizontal);

        tempAnimGroup.upSprites = tempUpSprites;
        tempAnimGroup.downSprites = tempDownSprites;
        tempAnimGroup.horizontalSprites = tempHorizontalSprites;

        return tempAnimGroup;
    }


    AnimationParts GetAnimationParts(List<AnimationIndexGroup> indexGroup)
    {
        List<Sprite> hairSprites = new List<Sprite>();
        List<Sprite> headSprites = new List<Sprite>();
        List<Sprite> bodySprites = new List<Sprite>();
        List<Sprite> armsSprites = new List<Sprite>();
        List<Sprite> legsSprites = new List<Sprite>();
        List<Sprite> shoesSprites = new List<Sprite>();

        for (int i = 0; i < indexGroup.Count; i++)
        {
            AnimationIndexGroup tempIndex = indexGroup[i];
            switch (i)
            {
                default:
                case 0:
                    hairSprites = GetSprites(tempIndex.groupIndex, tempIndex.partIndex, tempIndex.bodyPartIndex);
                    break;
                case 1:
                    headSprites = GetSprites(tempIndex.groupIndex, tempIndex.partIndex, tempIndex.bodyPartIndex);
                    break;
                case 2:
                    bodySprites = GetSprites(tempIndex.groupIndex, tempIndex.partIndex, tempIndex.bodyPartIndex);
                    break;
                case 3:
                    armsSprites = GetSprites(tempIndex.groupIndex, tempIndex.partIndex, tempIndex.bodyPartIndex);
                    break;
                case 4:
                    legsSprites = GetSprites(tempIndex.groupIndex, tempIndex.partIndex, tempIndex.bodyPartIndex);
                    break;
                case 5:
                    shoesSprites = GetSprites(tempIndex.groupIndex, tempIndex.partIndex, tempIndex.bodyPartIndex);
                    break;
            }
        }
        AnimationParts temporalAnimGroup = new AnimationParts(hairSprites, headSprites, bodySprites, armsSprites, legsSprites, shoesSprites);

        return temporalAnimGroup;
    }

    List<Sprite> GetSprites(int _groupIndex, int _partIndex, int _bodyPartIndex)
    {
        AnimationParts tempAnimationParts = null;
        switch (_partIndex)
        {
            default:
            case 0:
                tempAnimationParts = assetsList.GetAnimGroup(_groupIndex).upSprites;
                break;
            case 1:
                tempAnimationParts = assetsList.GetAnimGroup(_groupIndex).downSprites;
                break;
            case 2:
                tempAnimationParts = assetsList.GetAnimGroup(_groupIndex).horizontalSprites;
                break;
        }
        return tempAnimationParts.GetAnimationPart(_bodyPartIndex);
    }

}
