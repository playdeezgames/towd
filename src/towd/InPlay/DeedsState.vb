Imports towd.business

Friend Class DeedsState
    Inherits ChildView
    Private ReadOnly availableListView As ListView
    Private ReadOnly doneListView As ListView
    Private ReadOnly allListView As ListView
    Public Sub New(mainView As MainView, context As IContext)
        MyBase.New(mainView, context)
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
        allListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler allListView.OpenSelectedItem, AddressOf OnAllListViewOpenSelectedItem
        Dim tabView As New TabView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 1
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
        Dim allTab As New TabView.Tab With
            {
                .Text = "All",
                .View = allListView
            }
        tabView.AddTab(availableTab, False)
        tabView.AddTab(doneTab, False)
        tabView.AddTab(allTab, False)
        Add(tabView)

        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(tabView)
            }
        AddHandler closeButton.Clicked, AddressOf CloseWindow
        Add(closeButton)

    End Sub

    Private Sub OnAllListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim deed = CType(args.Value, IDeed)
        MessageBox.Query(deed.ToString, deed.Description, "Ok")
    End Sub

    Private Sub OnDoneListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim deed = CType(args.Value, IDeed)
        MessageBox.Query(deed.ToString, deed.Description, "Ok")
    End Sub

    Private Sub OnAvailableListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim deed = CType(args.Value, IDeed)
        MessageBox.Query(deed.ToString, deed.Description, "Ok")
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

    Friend Overrides Sub UpdateView()
        Dim available As New List(Of IDeed)
        Dim done As New List(Of IDeed)
        Dim all As New List(Of IDeed)
        Dim character = World.Avatar
        For Each descriptor In Deeds.Descriptors.Values.OrderBy(Function(x) x.Name)
            all.Add(descriptor)
            If character.HasDone(descriptor) Then
                done.Add(descriptor)
            ElseIf character.IsAvailable(descriptor) Then
                available.Add(descriptor)
            End If
        Next
        availableListView.SetSource(available)
        doneListView.SetSource(done)
        allListView.SetSource(all)
        MyBase.UpdateView()
    End Sub
End Class
