Friend Class EatRawFishFiletVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.EatRawFishFilet,
            business.VerbCategoryType.Eat,
            ItemType.RawFishFilet,
            "Sushi, anyone?")
        SetCharacterStatisticDeltaGenerator(
            StatisticType.FoodPoisoning,
            New RangeCharacterWeightedGenerator(0, 1))
    End Sub
End Class
