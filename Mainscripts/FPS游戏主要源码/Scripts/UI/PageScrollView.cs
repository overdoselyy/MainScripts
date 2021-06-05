using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 滚动视图
/// </summary>
public class PageScrollView : MonoBehaviour, IEndDragHandler
{
    protected ScrollRect rect;
    protected int pageCount;
    public RectTransform content;
    public float[] pages;
    public float moveTime = 0.3f;
    private float timer = 0;
    private float startMovePos;
    private int currentPage = 0;
    private bool isMoving = false;
    public bool IsAutoScroll;
    public float AutoScrollTime = 2;
    private float AutoScrodllTimer = 0;
    protected virtual void Start()
    {
        Init();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ListenerMove();
        ListenerAutoScroll();
    }
    //监听移动
    public void ListenerMove()
    {
        if (isMoving)
        {
            timer += Time.deltaTime * (1 / moveTime);
            rect.horizontalNormalizedPosition = Mathf.Lerp(startMovePos,pages[currentPage],timer);
            if (timer >= 1) {
                isMoving = false;
            }
        }
    }
    public void ScrollToPage(int page)
    {
        isMoving = true;
        this.currentPage = page;
        timer = 0;
        startMovePos = rect.horizontalNormalizedPosition;
    }
    //监听自动滚动
    public void ListenerAutoScroll()
    {
        if(IsAutoScroll)
        {
            AutoScrodllTimer += Time.deltaTime;
            if (AutoScrodllTimer >= AutoScrollTime) 
            {
                AutoScrodllTimer = 0;
                currentPage++;
                currentPage %= pageCount;
                ScrollToPage(currentPage);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
     
        this.ScrollToPage(CaculateMinDistancePage());
    }
    public int CaculateMinDistancePage() 
    {
        int minPage = 0;
        for (int i = 1; i < pages.Length; i++)
        {
            if (Mathf.Abs(pages[i] - rect.horizontalNormalizedPosition) < Mathf.Abs(pages[minPage] - rect.horizontalNormalizedPosition))
            {
                minPage = i;
            }
        }
        return minPage;
    }
    public void Init() 
    {
            rect = transform.GetComponent<ScrollRect>();
            if (rect == null)
            {
                throw new System.Exception("未查询到ScrollRect!");
            }
            content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
            pageCount = content.childCount;
            if (pageCount == 1) 
            {
                throw new System.Exception("只有一页不用分页");
            }
            pages = new float[pageCount];
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i] = i * (1.0f / (float)(pageCount - 1));
            }
    }
}
