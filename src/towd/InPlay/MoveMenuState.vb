Friend Class MoveMenuState
    Inherits ChildView
    Private ReadOnly directionListView As ListView
    Const NORTH_TEXT = "North"
    Const EAST_TEXT = "East"
    Const SOUTH_TEXT = "South"
    Const WEST_TEXT = "West"
    ReadOnly directionTable As IReadOnlyDictionary(Of String, towd.business.Direction) =
        New Dictionary(Of String, business.Direction) From
        {
            {NORTH_TEXT, business.Direction.North},
            {EAST_TEXT, business.Direction.East},
            {SOUTH_TEXT, business.Direction.South},
            {WEST_TEXT, business.Direction.West}
        }
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Move (Esc to cancel)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        directionListView = New ListView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        AddHandler directionListView.OpenSelectedItem, AddressOf OnDirectionListViewOpenSelectedItem
        Add(directionListView)
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            World.Avatar.SetFlag(towd.data.FlagType.MoveMenu, False)
            ShowState(GameState.Neutral)
        End If
    End Sub

    Friend Overrides Sub UpdateView()
        directionListView.SetSource(directionTable.Where(Function(x) World.Avatar.CanMove(x.Value)).Select(Function(x) x.Key).ToList)
    End Sub

    Private Sub OnDirectionListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim command = CStr(args.Value)
        Dim direction = directionTable(command)
        World.Avatar.Move(direction)
        World.Avatar.SetFlag(towd.data.FlagType.MoveMenu, False)
        ShowState(GameState.Neutral)
    End Sub
End Class
