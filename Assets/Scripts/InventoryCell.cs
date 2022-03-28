using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public event Action Ejecting;

    [SerializeField] private Text _nameField;
    [SerializeField] private Image _iconField;
    [SerializeField] private Image _background;

    private Transform _draggingParent;
    public Transform _originalParent;

    public void Init(Transform draggingParent)
    {
        _draggingParent = draggingParent;
        _originalParent = transform.parent;
    }

    public void Render(IItem item)
    {
        _nameField.text = item.Name;
        _iconField.sprite = item.UIIcon;
        _iconField.rectTransform.sizeDelta = new Vector2(item.UIIcon.rect.width, item.UIIcon.rect.height);
        float newScale = 1;
        if (_iconField.rectTransform.sizeDelta.x - _background.rectTransform.sizeDelta.x >
            _iconField.rectTransform.sizeDelta.y - _background.rectTransform.sizeDelta.y)
        {
            newScale = _background.rectTransform.sizeDelta.x / _iconField.rectTransform.sizeDelta.x;
        }
        else
        {
            newScale = _background.rectTransform.sizeDelta.y / _iconField.rectTransform.sizeDelta.y;
        }

        //_iconField.transform.localScale = new Vector3(newScale, newScale, 1);
        _iconField.rectTransform.sizeDelta *= newScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(_draggingParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (In((RectTransform)_originalParent))
        {
            InsertInGrid();
        }
        else
        {
            Eject();
        }
    }
    
    private bool In(RectTransform originalParent)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(originalParent, transform.position);
    }

    public void Eject()
    {
        Ejecting?.Invoke();
    }

    private void InsertInGrid()
    {
        int closestIndex = 0;
        GameObject filler = new GameObject("Filler", typeof(Image));
        filler.transform.SetParent(_originalParent);
        filler.transform.localScale = new Vector3(1, 1, 1);
        filler.transform.localPosition = new Vector3(0, 0, 0);
        for (int i = 0; i < _originalParent.childCount; ++i)
        {
            if (Vector3.Distance(transform.position, _originalParent.GetChild(i).position) <
                Vector3.Distance(transform.position, _originalParent.GetChild(closestIndex).position))
            {
                closestIndex = i;
            }
        }

        transform.SetParent(_originalParent);
        transform.SetSiblingIndex(closestIndex);
        Destroy(filler);
    }
}