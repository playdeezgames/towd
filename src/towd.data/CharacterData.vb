Public Class CharacterData
    Inherits EntityData
    Public Property CharacterType As CharacterType
    Public Property LocationId As Integer
    Public Property Items As New Dictionary(Of ItemType, HashSet(Of Integer))
End Class
