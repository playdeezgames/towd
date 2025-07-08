Imports towd.data

Public Interface ICharacterType
    ReadOnly Property CharacterType As CharacterType
    ReadOnly Property Name As String
    Sub AdvanceTime(character As ICharacter, amount As Integer)
    Sub Initialize(character As ICharacter)
    Function GetSpawnCount(map As IMap) As Integer
    Sub Spawn(map As IMap)
End Interface
