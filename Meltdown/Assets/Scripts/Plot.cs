﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : InteractableObjectBase
{
    private bool hasPlant = false;
    public bool watered = false;
    public ItemTypes plotType;
    public TaskController controller;

    private void Start()
    {
        controller = FindObjectOfType<TaskController>();
    }

    public override ItemTypes OnInteract()
    {
        if(hasPlant == false)
        {
            hasPlant = true;
        }
        else if (!watered)
        {
            watered = true;
            TaskTypes task = TaskTypes.Tree;
            if(plotType == ItemTypes.CarrotSeeds)
            {
                task = TaskTypes.Carrot;
            }
            else if(plotType == ItemTypes.PotatoSeeds)
            {
                task = TaskTypes.Potato;
            }
            else if (plotType == ItemTypes.TomatoSeeds)
            {
                task = TaskTypes.Tomato;
            }
            controller.taskComplete(task);

        }
        

        return ItemTypes.NONE;
    }

    public override bool CanInteract(ItemTypes heldItem)
    {   
        if(heldItem == plotType)
        {
            return !hasPlant;
        }
        else if(heldItem == ItemTypes.WaterBucket && hasPlant)
        {
            return !watered;
        }
        return false;
    }

}
