﻿Imports towd.data

Public Interface ICharacterType
    ReadOnly Property CharacterType As String
    ReadOnly Property Name As String
    Sub AdvanceTime(character As ICharacter, amount As Integer)
    Sub Initialize(character As ICharacter)
    Function GetSpawnCount(map As IMap) As Integer
    Sub Spawn(map As IMap)
    Function CanDialog(character As ICharacter) As Boolean
    Function StartDialog(character As ICharacter, otherCharacter As ICharacter) As IDialog
    ReadOnly Property StatisticTypes As IEnumerable(Of String)
End Interface
