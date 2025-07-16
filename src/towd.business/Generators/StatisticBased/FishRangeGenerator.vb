Friend Class FishRangeGenerator
    Inherits StatisticBasedRangeGenerator
    Implements ICharacterWeightedGenerator

    Public Sub New()
        MyBase.New(StatisticType.FishSkill, 1)
    End Sub
End Class
