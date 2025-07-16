Friend Class ChopRangeGenerator
    Inherits StatisticBasedRangeGenerator
    Implements ICharacterWeightedGenerator

    Public Sub New()
        MyBase.New(StatisticType.ChopSkill, 1)
    End Sub
End Class
