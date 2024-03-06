using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryCounter : Counter
{
    private string[] Ingredient = {"Bread", "CabbageSliced", "CheeseSlices", "MeatPattyCooked", "TomatoSlices" };
    private int[] Recipe = new int[5];

    public Sprite[] ingredientImage;
    public Image[] showImage;
    public Image completeImage;

    private float timer = 0f;
    private float showCompleteImageTime = 1f;
    public override void Start()
    {
        base.Start();
        Recipe[0] = 0;
    }

    public override void Update()
    {
        base.Update();
        if (Recipe[1] == 0)
        {
            GenerateRecipe(Random.Range(1,5));
            timer = 0f;
        }
        if (CheckDish())
        {
            Destroy(foodInCounter.gameObject);
            completeImage.gameObject.SetActive(true);
            while (timer < showCompleteImageTime)
            {
                timer += Time.deltaTime;
            }
            completeImage.gameObject.SetActive(false);
            for(int i = 0; i< showImage.Length; i++)
            {
                showImage[i].sprite = null;
            }
            Recipe = new int[Recipe.Length];
            Recipe[1] = 0;
        }
    }
    private void GenerateRecipe(int maxIngredientNum)
    {
        Recipe[0] = 0;
        showImage[0].sprite = ingredientImage[0];
        for (int i = 1; i < Recipe.Length; i++)
        {
            if (i < maxIngredientNum+1)
            {
                Recipe[i] = Random.Range(1, 5);
                showImage[i].sprite = ingredientImage[Recipe[i]];
            }
            else
            {
                Recipe[i] = -1;
            }
        }
    }
    private bool CheckDish()
    {
        bool check = false;
        if (foodInCounter != null)
        {
            check = true;
            bool[] checkIngredient = new bool[Recipe.Length];
            checkIngredient[0] = false;
            for (int i = 1; i < Recipe.Length; i++)
            {
                if (Recipe[i] == 0 || Recipe[i] == -1)
                {
                    checkIngredient[i] = true;
                }
                else
                {
                    checkIngredient[i] = false;
                }
            }
            foreach (Transform transform in foodInCounter.transform)
            {
                for (int i = 0; i < Recipe.Length; i++)
                {
                    if (!checkIngredient[i] && transform.gameObject.name.Contains(Ingredient[Recipe[i]]))
                    {
                        checkIngredient[i] = true;
                    }
                }
            }
            for (int i = 0; i < Recipe.Length; i++)
            {
                if (!checkIngredient[i])
                {
                    check = false;
                    break;
                }
            }
        }
        return check;
    }
}
