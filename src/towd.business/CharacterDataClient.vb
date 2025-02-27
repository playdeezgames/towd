Imports towd.data

Friend MustInherit Class CharacterDataClient
    Inherits WorldDataClient
    Protected ReadOnly Property CharacterId As Integer
    Protected ReadOnly Property CharacterData As CharacterData
        Get
            Return WorldData.Characters(CharacterId)
        End Get
    End Property
    Public Sub New(worldData As data.WorldData, characterId As Integer)
        MyBase.New(worldData)
        Me.CharacterId = characterId
    End Sub
End Class
