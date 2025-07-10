Friend Class THINDLACharacterTypeDescriptor
    Inherits NPCCharacterTypeDescriptor

    Public Sub New()
        MyBase.New(business.CharacterType.THINDLA, "THINDLA the Viking")
    End Sub

    Public Overrides Sub AdvanceTime(character As ICharacter, amount As Integer)
    End Sub

    Public Overrides Sub Initialize(character As ICharacter)
    End Sub

    Public Overrides Sub Spawn(map As IMap)
        Dim candidate As ILocation
        Do
            Dim column = RNG.GenerateInclusiveRange(0, map.Columns - 1)
            Dim row = RNG.GenerateInclusiveRange(0, map.Rows - 1)
            candidate = map.GetLocation(column, row)
        Loop Until Not candidate.Characters.Any
        map.World.CreateCharacter(Me, candidate)
    End Sub

    Public Overrides Function GetSpawnCount(map As IMap) As Integer
        Return 1
    End Function
End Class
