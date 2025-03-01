Public Class CharacterData
    Public Property CharacterType As CharacterType
    Public Property LocationId As Integer
    Public Property Flags As New HashSet(Of FlagType)
End Class
