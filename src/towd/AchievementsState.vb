Imports towd.business

Friend Class AchievementsState
    Inherits ChildView
    Private ReadOnly availableListView As ListView
    Private ReadOnly achievedListView As ListView
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Achievements (Esc to cancel)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        availableListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        achievedListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
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
        Dim achievedTab As New TabView.Tab With
            {
                .Text = "Achieved",
                .View = achievedListView
            }
        tabView.AddTab(availableTab, False)
        tabView.AddTab(achievedTab, False)
        Add(tabView)
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            ShowState(GameState.Neutral)
        End If
    End Sub

    Friend Overrides Sub UpdateView()
        Dim available As New List(Of IAchievementType)
        Dim achieved As New List(Of IAchievementType)
        Dim character = World.Avatar
        For Each descriptor In AchievementTypes.Descriptors.Values.OrderBy(Function(x) x.Name)
            If character.HasAchieved(descriptor) Then
                achieved.Add(descriptor)
            ElseIf character.IsAvailable(descriptor) Then
                available.Add(descriptor)
            End If
        Next
        availableListView.SetSource(available)
        achievedListView.SetSource(achieved)
    End Sub
End Class
