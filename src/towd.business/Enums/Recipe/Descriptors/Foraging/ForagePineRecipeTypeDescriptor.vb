Friend Class ForagePineRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.ForagePine, 1)
        SetDisplayName("Forage(Sticks)")
        SetRequiredLocationType(data.LocationType.Pine)
        SetLocationStatisticMinimum(data.StatisticType.ForagingCounter, 1)
        SetLocationStatisticDelta(data.StatisticType.ForagingCounter, -1)
        SetCharacterStatisticDelta(data.StatisticType.ForagingCounter, 1)
        SetItemTypeOutputGenerator(data.ItemType.Stick, New ForageRangeGenerator)
    End Sub
End Class
