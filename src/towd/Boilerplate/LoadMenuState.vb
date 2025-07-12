Friend Class LoadMenuState
    Inherits ChildView
    Private ReadOnly saveSlotListView As ListView

    Public Sub New(mainView As MainView, context As IContext)
        MyBase.New(mainView, context)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Load Menu (Esc to cancel)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        saveSlotListView = New ListView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 3
            }
        AddHandler saveSlotListView.OpenSelectedItem, AddressOf OnSaveSlotListViewOpenSelectedItem
        Add(saveSlotListView)

        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(saveSlotListView) + 1
            }
        AddHandler closeButton.Clicked, AddressOf OnCloseButtonClicked
        Add(closeButton)
    End Sub

    Private Sub OnCloseButtonClicked()
        CloseWindow()
    End Sub

    Private Sub OnSaveSlotListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim listItem = CType(args.Value, ISaveSlot)
        If LoadGame(listItem.SaveSlot) Then
            ShowState(GameState.Neutral)
        End If
    End Sub

    Friend Overrides Sub UpdateView()
        saveSlotListView.SetSource(SaveSlots.Descriptors.Values.Where(Function(x) x.SaveExists).ToList)
        MyBase.UpdateView()
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            CloseWindow()
        End If
    End Sub

    Private Sub CloseWindow()
        ShowState(GameState.MainMenu)
    End Sub
End Class
