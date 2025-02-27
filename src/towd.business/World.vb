Imports towd.data

Public Class World
    Implements IWorld
    Private ReadOnly worldData As WorldData

    Sub New(worldData As WorldData)
        Me.worldData = worldData
    End Sub

    Public Property Avatar As ICharacter Implements IWorld.Avatar
        Get
            If worldData.AvatarId.HasValue Then
                Return New Character(worldData, worldData.AvatarId.Value)
            Else
                Return Nothing
            End If
        End Get
        Set(value As ICharacter)
            If value IsNot Nothing Then
                worldData.AvatarId = value.Id
            Else
                worldData.AvatarId = Nothing
            End If
        End Set
    End Property

    Public Sub Initialize() Implements IWorld.Initialize
        Avatar = CreateCharacter(CharacterType.N00b)
    End Sub

    Public Sub Abandon() Implements IWorld.Abandon
        With worldData
            .AvatarId = Nothing
            .Characters.Clear()
        End With
    End Sub

    Public Function CreateCharacter(characterType As CharacterType) As ICharacter Implements IWorld.CreateCharacter
        Dim characterId = worldData.Characters.Count
        worldData.Characters.Add(
            New CharacterData With
            {
                .CharacterType = characterType
            })
        Return New Character(worldData, characterId)
    End Function
End Class
