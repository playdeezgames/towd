Imports System.Text
Imports towd.business

Friend Class MapState
    Inherits ChildView
    ReadOnly mapLabel As Label
    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        mapLabel = New Label With
            {
                .X = 0,
                .Y = 0
            }
        Add(mapLabel)

        Dim legendLabel As New Label With
            {
                .X = Pos.Right(mapLabel) + 3,
                .Y = mapLabel.Y
            }
        Add(legendLabel)
        Dim builder As New StringBuilder
        builder.AppendLine("Legend:")
        builder.AppendLine("[x] - you are here!")
        builder.AppendLine("? - unexplored")
        For Each descriptor In LocationTypes.Descriptors.Values
            builder.AppendLine($"{descriptor.MapLegend} - {descriptor.Name}")
        Next
        legendLabel.Text = builder.ToString

        Dim closeButton As New Button("Close") With
            {
                .Y = Pos.Bottom(mapLabel) + 1
            }
        AddHandler closeButton.Clicked, AddressOf OnCloseButtonClicked
        Add(closeButton)
    End Sub

    Private Sub OnCloseButtonClicked()
        ShowState(GameState.Neutral)
    End Sub

    Friend Overrides Sub UpdateView()
        Dim character = World.Avatar
        Dim map = character.CurrentLocation.Map
        Dim builder As New StringBuilder
        For Each row In Enumerable.Range(0, map.Rows)
            For Each column In Enumerable.Range(0, map.Columns)
                Dim location = map.GetLocation(column, row)
                If location.Column = character.CurrentLocation.Column AndAlso location.Row = character.CurrentLocation.Row Then
                    builder.Append("["c)
                Else
                    builder.Append(" "c)
                End If
                If character.KnowsLocation(location) Then
                    builder.Append(location.EntityType.MapLegend)
                Else
                    builder.Append("?")
                End If
                If location.Column = character.CurrentLocation.Column AndAlso location.Row = character.CurrentLocation.Row Then
                    builder.Append("]"c)
                Else
                    builder.Append(" "c)
                End If
            Next
            builder.AppendLine()
        Next
        mapLabel.Text = builder.ToString
    End Sub
End Class
