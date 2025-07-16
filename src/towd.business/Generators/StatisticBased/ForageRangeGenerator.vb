Friend Class ForageRangeGenerator
    Inherits StatisticBasedRangeGenerator
    Implements ICharacterWeightedGenerator

    Public Sub New()
        MyBase.New(StatisticType.ForagingSkill, 1)
    End Sub
End Class
