Imports towd.business

Friend Class StatisticsState
    Inherits ChildView

    Private ReadOnly statisticsListView As ListView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Statistics",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        statisticsListView = New ListView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill - 1
            }
        AddHandler statisticsListView.OpenSelectedItem, AddressOf OnStatisticsListViewOpenSelectedItem

        Add(statisticsListView)
        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(statisticsListView)
            }
        AddHandler closeButton.Clicked, AddressOf CloseWindow
        Add(closeButton)

    End Sub

    Private Sub CloseWindow()
        ShowState(GameState.Neutral)
    End Sub

    Private Sub OnStatisticsListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim statisticType = CType(args.Value, StatisticsListViewItem).StatisticType
        MessageBox.Query(statisticType.Name, statisticType.Description, "Ok")
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        statisticsListView.SetSource(
            character.EntityType.CharacterType.ToDescriptor.StatisticTypes.Select(
            Function(x) New StatisticsListViewItem(World.Avatar, x.ToStatisticTypeDescriptor)).ToList)
        MyBase.UpdateView()
    End Sub
End Class
