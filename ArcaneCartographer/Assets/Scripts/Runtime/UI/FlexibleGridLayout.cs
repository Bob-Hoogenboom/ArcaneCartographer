using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// source: https://www.youtube.com/watch?v=CGsEJToeXmA
/// </summary>
public class FlexibleGridLayout : LayoutGroup
{
    public enum FitType
    {
        UNIFORM,
        WIDTH,
        HEIGHT,
        FIXEDROWS, 
        FIXEDCOLUMNS
    }

    public int rows;
    public int columns;

    public bool fitX;
    public bool fitY;

    public Vector2 cellSize;
    public Vector2 spacing;

    public FitType fitType;    


    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();


        if (fitType == FitType.WIDTH || fitType == FitType.HEIGHT || fitType == FitType.UNIFORM)
        {
            fitX = true;
            fitY = true;


            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);
        }

            if (fitType == FitType.WIDTH || fitType == FitType.FIXEDCOLUMNS)
            {
                rows = Mathf.CeilToInt(transform.childCount / (float)columns);
            }

            if (fitType == FitType.HEIGHT || fitType == FitType.FIXEDROWS)
            {
                columns = Mathf.CeilToInt(transform.childCount / (float)rows);
            }


            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            float celLWidth = parentWidth / (float)columns - ((spacing.x / (float)columns) * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
            float cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

            cellSize.x = fitX ? celLWidth : cellSize.x;
            cellSize.y = fitY ? cellHeight : cellSize.y;

            int columnCount = 0;
            int rowCount = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                rowCount = i / columns;
                columnCount = i % columns;

                var item = rectChildren[i];

                var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
                var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }
        
    }

    public override void CalculateLayoutInputVertical()
    {
        
    }

    public override void SetLayoutHorizontal()
    {
        
    }

    public override void SetLayoutVertical()
    {
        
    }
}
