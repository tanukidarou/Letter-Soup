﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterGridWidgetPresenter
{
    private readonly ILetterGridWidget view;

    public LetterGridWidgetPresenter(ILetterGridWidget view)
    {
        this.view = view;
    }

    public void Load()
    {
        view.SetGrid(ActionsProvider.GetLetterGridAction.Execute());
    }
}
