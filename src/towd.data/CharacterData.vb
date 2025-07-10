Public Class CharacterData
    Inherits EntityData
    Public Property CharacterType As String
    Public Property LocationId As Integer
    Public Property Items As New Dictionary(Of String, HashSet(Of Integer))
    Public Property Deeds As New HashSet(Of String)
    Public Property KnownLocations As New HashSet(Of Integer)
End Class
