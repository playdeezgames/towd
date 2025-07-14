Imports System.Text
Imports towd.business

Friend Class MapUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Private ReadOnly cancelDialog As Func(Of IUIDialog)

    Public Sub New(context As IUIContext, cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
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
            Return {builder.ToString}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return {"Ok"}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Map"
        End Get
    End Property

    Public Function Choose(choice As String) As (String, IUIDialog) Implements IUIDialog.Choose
        Return (Nothing, cancelDialog())
    End Function
End Class
