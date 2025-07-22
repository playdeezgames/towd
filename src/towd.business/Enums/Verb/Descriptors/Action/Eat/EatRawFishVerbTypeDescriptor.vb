Friend Class EatRawFishVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.EatRawFish,
            business.VerbCategoryType.Eat,
            ItemType.RawFish,
            "We likes it RAW and WRIGGLING, doesn't we, precious?")
        SetCharacterStatisticDeltaGenerator(
            StatisticType.FoodPoisoning,
            New RangeCharacterWeightedGenerator(0, 1))
    End Sub
End Class
