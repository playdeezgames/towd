Friend Class ChopRangeGenerator
    Implements ICharacterWeightedGenerator

    Public Function GetMinimum(character As ICharacter) As Integer Implements ICharacterWeightedGenerator.GetMinimum
        Return 1
    End Function

    Public Function GetMaximum(character As ICharacter) As Integer Implements ICharacterWeightedGenerator.GetMaximum
        Return character.GetStatistic(business.StatisticType.ChopSkill)
    End Function

    Public Function Generate(character As ICharacter) As Integer Implements ICharacterWeightedGenerator.Generate
        Return RNG.GenerateInclusiveRange(GetMinimum(character), GetMaximum(character))
    End Function

    Public Overrides Function ToString() As String
        Return $"1..(Chop Skill)"
    End Function
End Class
