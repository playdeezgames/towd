Imports towd.data

Friend MustInherit Class CharacterTypeDescriptor
    Implements ICharacterType
    Public ReadOnly Property CharacterType As CharacterType Implements ICharacterType.CharacterType

    Public ReadOnly Property Name As String Implements ICharacterType.Name
    Sub New(characterType As CharacterType, name As String)
        Me.CharacterType = characterType
        Me.Name = name
    End Sub

    Public MustOverride Sub AdvanceTime(character As ICharacter, amount As Integer) Implements ICharacterType.AdvanceTime
    Public MustOverride Sub Initialize(character As ICharacter) Implements ICharacterType.Initialize
    Public MustOverride Function GetSpawnCount(map As IMap) As Integer Implements ICharacterType.GetSpawnCount
    Public MustOverride Sub Spawn(map As IMap) Implements ICharacterType.Spawn
End Class
