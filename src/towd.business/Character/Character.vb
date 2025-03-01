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

    Public ReadOnly Property EntityType As ICharacterType Implements ICharacter.EntityType
        Get
            Return CharacterData.CharacterType.ToDescriptor()
        End Get
    End Property

    Public ReadOnly Property CanDoAnyVerb As Boolean Implements ICharacter.CanDoAnyVerb
        Get
            Return VerbTypes.Descriptors.Keys.Any(Function(x) CanDoVerb(x))
        End Get
    End Property

    Public ReadOnly Property IsAvatar As Boolean Implements ICharacter.IsAvatar
        Get
            Return WorldData.AvatarId.HasValue AndAlso Id = WorldData.AvatarId.Value
        End Get
    End Property

    Public ReadOnly Property HasMessages As Boolean Implements ICharacter.HasMessages
        Get
            Return IsAvatar AndAlso WorldData.Messages.Count <> 0
        End Get
    End Property

    Public ReadOnly Property CurrentMessage As String() Implements ICharacter.CurrentMessage
        Get
            If HasMessages Then
                Return WorldData.Messages(0).ToArray
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property World As IWorld Implements ICharacter.World
        Get
            Return New World(WorldData)
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

    Public Sub AddMessage(ParamArray lines() As String) Implements ICharacter.AddMessage
        If IsAvatar Then
            WorldData.Messages.Add(lines.ToList)
        End If
    End Sub

    Public Sub DismissMessage() Implements ICharacter.DismissMessage
        If HasMessages Then
            WorldData.Messages.RemoveAt(0)
        End If
    End Sub

    Public Sub AdvanceTime(amount As Integer) Implements ICharacter.AdvanceTime
        EntityType.AdvanceTime(Me, amount)
    End Sub

    Public Sub AppendMessage(ParamArray lines() As String) Implements ICharacter.AppendMessage
        If IsAvatar Then
            If HasMessages Then
                WorldData.Messages.Last.AddRange(lines)
            Else
                AddMessage(lines)
            End If
        End If

    End Sub

    Public Function HasFlag(flagType As FlagType) As Boolean Implements ICharacter.HasFlag
        Return CharacterData.Flags.Contains(flagType)
    End Function

    Public Function CanDoVerb(verbType As VerbType) As Boolean Implements ICharacter.CanDoVerb
        Return verbType.ToDescriptor.CanPerform(Me)
    End Function
End Class
