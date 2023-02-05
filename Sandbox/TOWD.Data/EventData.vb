Public Class EventData
    Public Property EventType As EventType
    Public Property LinkIndices As New Dictionary(Of LinkType, Integer)
    Public Property Strings As New Dictionary(Of EventString, String)
    Public Property Integers As New Dictionary(Of EventInteger, Integer)
End Class
