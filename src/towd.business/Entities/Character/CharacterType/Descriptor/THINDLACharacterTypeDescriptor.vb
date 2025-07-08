Friend Class THINDLACharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Public Sub New()
        MyBase.New(data.CharacterType.THINDLA, "THINDLA the Viking")
    End Sub

    Public Overrides Sub AdvanceTime(character As ICharacter, amount As Integer)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub Initialize(character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub Spawn(map As IMap)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function GetSpawnCount(map As IMap) As Integer
        Return 0
    End Function
End Class
