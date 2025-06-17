Imports towd.business
Imports towd.data

Friend Class ItemStackState
    Inherits ChildView
    Private ReadOnly itemStackListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)

        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Items (Esc to cancel)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        itemStackListView = New ListView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler itemStackListView.OpenSelectedItem, AddressOf OnItemStackListViewOpenSelectedItem
        Add(itemStackListView)
    End Sub

    Private Sub OnItemStackListViewOpenSelectedItem(args As ListViewItemEventArgs)
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim items = character.GetItemsOfType(character.CurrentItemType).ToList
        itemStackListView.SetSource(items)
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            World.Avatar.CurrentItemType = Nothing
            ShowState(GameState.Neutral)
        End If
    End Sub
End Class
