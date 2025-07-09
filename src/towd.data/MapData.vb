Public Class MapData
    Inherits EntityData
    Property MapType As String
    Property Columns As Integer
    Property Rows As Integer
    Property Locations As New List(Of Integer)
End Class
