Friend Class StatisticBasedRangeGenerator
    Implements ICharacterWeightedGenerator
    Private ReadOnly statisticType As String
    Private ReadOnly minimum As Integer

    Sub New(statisticType As String, minimum As Integer)
        Me.statisticType = statisticType
        Me.minimum = minimum
    End Sub

    Public Function GetMinimum(character As ICharacter) As Integer Implements ICharacterWeightedGenerator.GetMinimum
        Return minimum
    End Function

    Public Function GetMaximum(character As ICharacter) As Integer Implements ICharacterWeightedGenerator.GetMaximum
        Return character.GetStatistic(statisticType)
    End Function

    Public Function Generate(character As ICharacter) As Integer Implements ICharacterWeightedGenerator.Generate
        Return RNG.GenerateInclusiveRange(GetMinimum(character), GetMaximum(character))
    End Function

    Public Overrides Function ToString() As String
        Return $"{minimum}..({statisticType.ToStatisticTypeDescriptor.Name})"
    End Function
End Class
