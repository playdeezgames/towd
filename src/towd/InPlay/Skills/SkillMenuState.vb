Imports towd.business

Friend Class SkillMenuState
    Inherits ChildView
    Private ReadOnly advanceableListView As ListView
    Private ReadOnly allListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Skills (Esc to cancel)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        advanceableListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler advanceableListView.OpenSelectedItem, AddressOf OnAdvanceableListViewOpenSelectedItem
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
                .Height = [Dim].Fill - 3
            }
        Dim advanceableTab As New TabView.Tab With
            {
                .Text = "Advanceable",
                .View = advanceableListView
            }
        Dim allTab As New TabView.Tab With
            {
                .Text = "All",
                .View = allListView
            }
        tabView.AddTab(allTab, False)
        tabView.AddTab(advanceableTab, False)
        Add(tabView)
        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(tabView) + 1
            }
        AddHandler closeButton.Clicked, AddressOf CloseWindow
        Add(closeButton)

    End Sub

    Private Sub OnAllListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim skillType = CType(args.Value, SkillMenuListViewItem).SkillType
        Dim character = World.Avatar
        skillType.Advance(character)
        CloseWindow()
    End Sub

    Private Sub OnAdvanceableListViewOpenSelectedItem(args As ListViewItemEventArgs)
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            CloseWindow()
        End If
    End Sub

    Private Sub CloseWindow()
        World.Avatar.SetFlag(towd.data.FlagType.SkillMenu, False)
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim advanceable As New List(Of SkillMenuListViewItem)
        Dim allSkills As New List(Of SkillMenuListViewItem)
        Dim character = World.Avatar
        For Each descriptor In SkillTypes.Descriptors.Values.OrderBy(Function(x) x.Name)
            allSkills.Add(New SkillMenuListViewItem(descriptor, character))
            If character.CanAdvance(descriptor) Then
                advanceable.Add(New SkillMenuListViewItem(descriptor, character))
            End If
        Next
        advanceableListView.SetSource(advanceable)
        allListView.SetSource(allSkills)
    End Sub
End Class
