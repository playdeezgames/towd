Imports towd.data

Friend Class Character
    Inherits CharacterDataClient
    Implements ICharacter

    Public Sub New(worldData As data.WorldData, characterId As Integer)
        MyBase.New(worldData, characterId)
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

    Public ReadOnly Property CharacterType As ICharacterType Implements ICharacter.CharacterType
        Get
            Return CharacterData.CharacterType.ToDescriptor()
        End Get
    End Property
End Class
