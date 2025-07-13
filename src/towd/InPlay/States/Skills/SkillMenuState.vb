Imports towd.business

Friend Class SkillMenuState
    Inherits ChildView

    Private Const ADVANCEABLE_TEXT As String = "Advanceable"
    Private Const ALL_TEXT As String = "All"
    Private ReadOnly advanceableListView As ListView
    Private ReadOnly allListView As ListView
    Private ReadOnly tabView As TabView
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
        tabView = New TabView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 1
            }
        Dim advanceableTab As New TabView.Tab With
            {
                .Text = ADVANCEABLE_TEXT,
                .View = advanceableListView
            }
        Dim allTab As New TabView.Tab With
            {
                .Text = ALL_TEXT,
                .View = allListView
            }
        tabView.AddTab(advanceableTab, False)
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
        Dim skillType = CType(args.Value, SkillMenuListViewItem).SkillType
        Dim character = Context.World.Avatar
        If skillType.Advance(character) Then
            UpdateView()
        End If
    End Sub

    Private Sub OnAdvanceableListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim skillType = CType(args.Value, SkillMenuListViewItem).SkillType
        Dim character = Context.World.Avatar
        If skillType.Advance(character) Then
            UpdateView()
        End If
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            CloseWindow()
        ElseIf args.KeyEvent.Key = Key.F1 Then
            args.Handled = True
            Select Case TabView.SelectedTab.Text
                Case ALL_TEXT
                    TopicState.Topic = SkillTypeTopicTable(CType(allListView.Source.ToList(allListView.SelectedItem), SkillMenuListViewItem).SkillType.SkillType)
                Case ADVANCEABLE_TEXT
                    TopicState.Topic = SkillTypeTopicTable(CType(advanceableListView.Source.ToList(advanceableListView.SelectedItem), SkillMenuListViewItem).SkillType.SkillType)
            End Select
            ShowState(GameState.Topic)
        End If
    End Sub

    Private Sub CloseWindow()
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim advanceable As New List(Of SkillMenuListViewItem)
        Dim allSkills As New List(Of SkillMenuListViewItem)
        Dim character = Context.World.Avatar
        For Each descriptor In SkillTypes.Descriptors.Values.OrderBy(Function(x) x.Name)
            allSkills.Add(New SkillMenuListViewItem(descriptor, character))
            If character.CanAdvance(descriptor) Then
                advanceable.Add(New SkillMenuListViewItem(descriptor, character))
            End If
        Next
        advanceableListView.SetSource(advanceable)
        allListView.SetSource(allSkills)
        MyBase.UpdateView()
    End Sub
End Class
