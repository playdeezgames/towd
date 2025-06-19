Imports towd.business
Imports towd.data

Friend Class CraftMenuState
    Inherits ChildView
    Private ReadOnly recipeListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Craft (Esc to cancel)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        recipeListView = New ListView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 3
            }
        AddHandler recipeListView.OpenSelectedItem, AddressOf OnRecipeListViewOpenSelectedItem
        Add(recipeListView)

        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(recipeListView) + 1
            }
        AddHandler closeButton.Clicked, AddressOf CloseWindow
        Add(closeButton)

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
            CloseWindow()
        End If
    End Sub

    Private Sub CloseWindow()
        World.Avatar.SetFlag(towd.data.FlagType.CraftMenu, False)
        ShowState(GameState.Neutral)
    End Sub
End Class
