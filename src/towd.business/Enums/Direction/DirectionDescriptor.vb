Public Class DirectionDescriptor
    Implements IDirection
    Private ReadOnly columnDelta As Integer
    Private ReadOnly rowDelta As Integer
    Sub New(
           direction As String,
           name As String,
           columnDelta As Integer,
           rowDelta As Integer)
        Me.Direction = direction
        Me.Name = name
        Me.columnDelta = columnDelta
        Me.rowDelta = rowDelta
    End Sub
    Public ReadOnly Property Direction As String Implements IDirection.Direction
    Public ReadOnly Property Name As String Implements IDirection.Name

    Public Function NextColumn(column As Integer, row As Integer) As Integer Implements IDirection.NextColumn
        Return column + columnDelta
    End Function

    Public Function NextRow(column As Integer, row As Integer) As Integer Implements IDirection.NextRow
        Return row + rowDelta
    End Function
End Class
