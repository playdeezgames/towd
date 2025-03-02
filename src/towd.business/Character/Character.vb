Imports towd.data

Friend Class Character
    Inherits Entity(Of ICharacterType, CharacterData)
    Implements ICharacter

    Public Sub New(worldData As data.WorldData, characterId As Integer)
        MyBase.New(worldData, characterId)
    End Sub
    Public Function CanDoVerb(verbType As VerbType) As Boolean Implements ICharacter.CanDoVerb
        Return verbType.ToDescriptor.CanPerform(Me)
    End Function


    Public Property Location As ILocation Implements ICharacter.Location
        Get
            Return New Location(WorldData, EntityData.LocationId)
        End Get
        Set(value As ILocation)
            EntityData.LocationId = value.Id
        End Set
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

    Public Overrides Property EntityType As ICharacterType
        Get
            Return EntityData.CharacterType.ToDescriptor()
        End Get
        Set(value As ICharacterType)
            EntityData.CharacterType = value.CharacterType
            value.Initialize(Me)
        End Set
    End Property

    Protected Overrides ReadOnly Property EntityData As CharacterData
        Get
            Return WorldData.Characters(Id)
        End Get
    End Property

    Public ReadOnly Property IsDead As Boolean Implements ICharacter.IsDead
        Get
            Return GetStatistic(StatisticType.Health) = GetStatisticMinimum(StatisticType.Health)
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

    Public Overrides Sub AdvanceTime(amount As Integer)
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
End Class
