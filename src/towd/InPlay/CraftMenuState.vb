Imports towd.business
Imports towd.data

Friend Class CraftMenuState
    Inherits ChildView
    Private ReadOnly availableRecipeListView As ListView
    Private ReadOnly allRecipeListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Craft (Esc to cancel)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)

        availableRecipeListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler availableRecipeListView.OpenSelectedItem, AddressOf OnAvailableRecipeListViewOpenSelectedItem

        Dim availableRecipeListTab = New TabView.Tab With
            {
                .View = availableRecipeListView,
                .Text = "Available"
            }

        allRecipeListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        allRecipeListView.SetSource(RecipeTypes.Descriptors.Values.ToList)
        AddHandler allRecipeListView.OpenSelectedItem, AddressOf OnAllRecipeListViewOpenSelectedItem

        Dim allRecipeListTab = New TabView.Tab With
            {
                .View = allRecipeListView,
                .Text = "All"
            }

        Dim tabView As New TabView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 3
            }
        tabView.AddTab(availableRecipeListTab, True)
        tabView.AddTab(allRecipeListTab, False)
        Add(tabView)


        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(tabView) + 1
            }
        AddHandler closeButton.Clicked, AddressOf CloseWindow
        Add(closeButton)

    End Sub

    Private Sub OnAllRecipeListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim descriptor = CType(args.Value, IRecipeType)
        If descriptor.CanCraft(World.Avatar) Then
            descriptor.Craft(World.Avatar)
            ShowState(GameState.Neutral)
        Else
            MessageBox.ErrorQuery("Sorry Not Sorry!", "You cannot craft that.", "OK")
        End If
    End Sub

    Private Sub OnAvailableRecipeListViewOpenSelectedItem(args As ListViewItemEventArgs)
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
        availableRecipeListView.SetSource(recipes)
        Dim lastRecipe = character.LastRecipe
        Dim recipeIndex = If(lastRecipe.HasValue, recipes.FindIndex(Function(x) x.RecipeType = lastRecipe.Value), -1)
        If recipeIndex > -1 Then
            availableRecipeListView.SelectedItem = recipeIndex
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
