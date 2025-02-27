Imports towd.data

Friend Class Character
    Implements ICharacter

    Private ReadOnly worldData As data.WorldData
    Private ReadOnly characterId As Integer
    Private ReadOnly Property CharacterData As CharacterData
        Get
            Return worldData.Characters(characterId)
        End Get
    End Property

    Public Sub New(worldData As data.WorldData, characterId As Integer)
        Me.worldData = worldData
        Me.characterId = characterId
    End Sub

    Public ReadOnly Property Id As Integer Implements ICharacter.Id
        Get
            Return characterId
        End Get
    End Property

    Public ReadOnly Property Location As ILocation Implements ICharacter.Location
        Get
            Return New Location(worldData, CharacterData.LocationId)
        End Get
    End Property
End Class
