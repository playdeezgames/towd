Public Class LocationData
    Inherits EntityData
    Public Property LocationType As LocationType
    Public Property MapId As Integer
    Public Property Column As Integer
    Public Property Row As Integer
    Public Property CharacterIds As New HashSet(Of Integer)
End Class
