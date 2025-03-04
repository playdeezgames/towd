Imports towd.business

Friend Class CraftMenuState
    Inherits ChildView
    Private ReadOnly recipeListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)

        recipeListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 2
            }
        AddHandler recipeListView.OpenSelectedItem, AddressOf OnRecipeListViewOpenSelectedItem
        Add(recipeListView)
        Dim goBackButton As New Button With
            {
                .Text = "Go Back",
                .Y = Pos.Bottom(recipeListView) + 1
            }
        AddHandler goBackButton.Clicked, AddressOf OnGoBackButtonClicked
        Add(goBackButton)
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
End Class
