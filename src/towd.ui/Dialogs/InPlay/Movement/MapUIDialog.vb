Imports System.Text
Imports towd.business

Friend Class MapUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Dim character = context.World.Avatar
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
        builder.AppendLine("Legend:")
        builder.AppendLine("[x]-you are here!")
        builder.Append("?-unexplored")
        For Each descriptor In LocationTypes.Descriptors.Values
            builder.Append($"|{descriptor.MapLegend}-{descriptor.Name}")
        Next
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))({(Mood.Normal, builder.ToString, True)})
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))({"Ok"})
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Map")
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Return cancelDialog()
    End Function
End Class
