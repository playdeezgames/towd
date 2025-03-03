Imports towd.business
Imports towd.data

Friend Class ItemStackState
    Inherits ChildView
    Private ReadOnly itemStackListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)

        itemStackListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 2
            }
        AddHandler itemStackListView.OpenSelectedItem, AddressOf OnItemStackListViewOpenSelectedItem
        Add(itemStackListView)
        Dim goBackButton As New Button With
            {
                .Text = "Go Back",
                .Y = Pos.Bottom(itemStackListView) + 1
            }
        AddHandler goBackButton.Clicked, AddressOf OnGoBackButtonClicked
        Add(goBackButton)
    End Sub

    Private Sub OnItemStackListViewOpenSelectedItem(args As ListViewItemEventArgs)
        ShowState(GameState.Neutral)
    End Sub

    Private Sub OnGoBackButtonClicked()
        World.Avatar.CurrentItemType = Nothing
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim items = character.GetItemsOfType(character.CurrentItemType).ToList
        itemStackListView.SetSource(items)
    End Sub
End Class
