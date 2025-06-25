Friend Class ForageRockRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.ForageRock, 1)
        SetDisplayName("Forage(Rocks)")
        SetRequiredLocationType(data.LocationType.Rock)
        SetLocationStatisticMinimum(data.StatisticType.ForagingCounter, 1)
        SetLocationStatisticDelta(data.StatisticType.ForagingCounter, -1)
        SetCharacterStatisticDelta(data.StatisticType.ForagingCounter, 1)
        SetItemTypeOutputGenerator(data.ItemType.Rock, New ForageRangeGenerator)
    End Sub
End Class
