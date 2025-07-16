Friend Class DigRangeGenerator
    Inherits StatisticBasedRangeGenerator
    Implements ICharacterWeightedGenerator

    Public Sub New()
        MyBase.New(StatisticType.DigSkill, 1)
    End Sub
End Class
