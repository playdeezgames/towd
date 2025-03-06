Imports towd.business
Imports towd.data

Friend Class CraftMenuState
    Inherits ChildView
    Private ReadOnly recipeListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)

        recipeListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler recipeListView.OpenSelectedItem, AddressOf OnRecipeListViewOpenSelectedItem
        Add(recipeListView)
    End Sub

    Private Sub OnRecipeListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim descriptor = CType(args.Value, IRecipeType)
        descriptor.Craft(World.Avatar)
        ShowState(GameState.Neutral)
    End Sub

    Private Sub OnGoBackButtonClicked()
        World.Avatar.SetFlag(towd.data.FlagType.CraftMenu, False)
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim recipes = character.GetCraftableRecipes().ToList()
        recipeListView.SetSource(recipes)
        Dim lastRecipe = character.LastRecipe
        Dim recipeIndex = If(lastRecipe.HasValue, recipes.FindIndex(Function(x) x.RecipeType = lastRecipe.Value), -1)
        If recipeIndex > -1 Then
            recipeListView.SelectedItem = recipeIndex
        End If
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            World.Avatar.SetFlag(towd.data.FlagType.CraftMenu, False)
            ShowState(GameState.Neutral)
        End If
    End Sub
End Class
