Imports towd.business

Friend Class DeedsState
    Inherits ChildView
    Private ReadOnly availableListView As ListView
    Private ReadOnly doneListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Deeds (Esc to cancel)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        availableListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler availableListView.OpenSelectedItem, AddressOf OnAvailableListViewOpenSelectedItem
        doneListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler doneListView.OpenSelectedItem, AddressOf OnDoneListViewOpenSelectedItem
        Dim tabView As New TabView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        Dim availableTab As New TabView.Tab With
            {
                .Text = "Available",
                .View = availableListView
            }
        Dim doneTab As New TabView.Tab With
            {
                .Text = "Done",
                .View = doneListView
            }
        tabView.AddTab(availableTab, False)
        tabView.AddTab(doneTab, False)
        Add(tabView)
    End Sub

    Private Sub OnDoneListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim deed = CType(args.Value, IDeed)
        MessageBox.Query(deed.Name, deed.Description, "Ok")
    End Sub

    Private Sub OnAvailableListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim deed = CType(args.Value, IDeed)
        MessageBox.Query(deed.Name, deed.Description, "Ok")
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            ShowState(GameState.Neutral)
        End If
    End Sub

    Friend Overrides Sub UpdateView()
        Dim available As New List(Of IDeed)
        Dim done As New List(Of IDeed)
        Dim character = World.Avatar
        For Each descriptor In Deeds.Descriptors.Values.OrderBy(Function(x) x.Name)
            If character.HasDone(descriptor) Then
                done.Add(descriptor)
            ElseIf character.IsAvailable(descriptor) Then
                available.Add(descriptor)
            End If
        Next
        availableListView.SetSource(available)
        doneListView.SetSource(done)
    End Sub
End Class
