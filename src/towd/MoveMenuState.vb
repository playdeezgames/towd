Friend Class MoveMenuState
    Inherits ChildView
    Private ReadOnly directionListView As ListView
    Const NORTH_TEXT = "North"
    Const EAST_TEXT = "East"
    Const SOUTH_TEXT = "South"
    Const WEST_TEXT = "West"
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)

        directionListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        directionListView.SetSource({NORTH_TEXT, EAST_TEXT, SOUTH_TEXT, WEST_TEXT}.ToList)
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
    End Sub

    Private Sub OnDirectionListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim command = CStr(args.Value)
        Select Case command
            Case NORTH_TEXT
                World.Avatar.Move(business.Direction.North)
            Case EAST_TEXT
                World.Avatar.Move(business.Direction.East)
            Case SOUTH_TEXT
                World.Avatar.Move(business.Direction.South)
            Case WEST_TEXT
                World.Avatar.Move(business.Direction.West)
        End Select
        World.Avatar.SetFlag(towd.data.FlagType.MoveMenu, False)
        ShowState(GameState.Neutral)
    End Sub
End Class
