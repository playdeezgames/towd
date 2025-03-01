Public Class MapData
    Inherits EntityData
    Property MapType As MapType
    Property Columns As Integer
    Property Rows As Integer
    Property Locations As New List(Of Integer)
End Class
