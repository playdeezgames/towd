Imports towd.data

Friend MustInherit Class CharacterDataClient
    Inherits WorldDataClient
    Protected ReadOnly Property EntityId As Integer
    Protected ReadOnly Property EntityData As CharacterData
        Get
            Return WorldData.Characters(EntityId)
        End Get
    End Property
    Public Sub New(worldData As data.WorldData, characterId As Integer)
        MyBase.New(worldData)
        Me.EntityId = characterId
    End Sub
End Class
