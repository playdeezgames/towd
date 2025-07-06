Imports towd.business
Imports towd.data

Friend Class ItemStackState
    Inherits ChildView
    Private ReadOnly itemStackListView As ListView
    Public Shared Property CurrentItemType As IItemType

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
                .Height = [Dim].Fill - 3
            }
        AddHandler itemStackListView.OpenSelectedItem, AddressOf OnItemStackListViewOpenSelectedItem
        Add(itemStackListView)

        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(itemStackListView) + 1
            }
        AddHandler closeButton.Clicked, AddressOf CloseWindow
        Add(closeButton)

    End Sub

    Private Sub OnItemStackListViewOpenSelectedItem(args As ListViewItemEventArgs)
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim items = character.GetItemsOfType(CurrentItemType).ToList
        itemStackListView.SetSource(items)
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            CloseWindow()
        End If
    End Sub

    Private Sub CloseWindow()
        ShowState(GameState.Neutral)
    End Sub
End Class
