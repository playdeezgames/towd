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

    Public Property Location As ILocation Implements ICharacter.Location
        Get
            Return New Location(WorldData, CharacterData.LocationId)
        End Get
        Set(value As ILocation)
            CharacterData.LocationId = value.Id
        End Set
    End Property

    Public ReadOnly Property CharacterType As ICharacterType Implements ICharacter.CharacterType
        Get
            Return CharacterData.CharacterType.ToDescriptor()
        End Get
    End Property

    Public ReadOnly Property CanDoAnyVerb As Boolean Implements ICharacter.CanDoAnyVerb
        Get
            Return VerbTypes.Descriptors.Keys.Any(Function(x) x.ToDescriptor.CanPerform(Me))
        End Get
    End Property

    Public Sub Move(direction As Direction) Implements ICharacter.Move
        Dim descriptor = direction.ToDescriptor
        Dim column = Location.Column
        Dim row = Location.Row
        Dim nextColumn = descriptor.NextColumn(column, row)
        Dim nextRow = descriptor.NextRow(column, row)
        Dim map = Location.Map
        Dim nextLocation = map.GetLocation(nextColumn, nextRow)
        If nextLocation IsNot Nothing Then
            Location = nextLocation
        End If
    End Sub

    Public Sub SetFlag(flagType As FlagType, flagValue As Boolean) Implements ICharacter.SetFlag
        If flagValue Then
            CharacterData.Flags.Add(flagType)
        Else
            CharacterData.Flags.Remove(flagType)
        End If
    End Sub
End Class
