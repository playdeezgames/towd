Friend Class ForageGrassRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.ForageGrass, 1)
        SetDisplayName("Forage(Plant Fibers)")
        SetRequiredLocationType(data.LocationType.Grass)
        SetLocationStatisticMinimum(data.StatisticType.ForagingCounter, 1)
        SetLocationStatisticDelta(data.StatisticType.ForagingCounter, -1)
        SetCharacterStatisticDelta(data.StatisticType.ForagingCounter, 1)
        SetItemTypeOutputGenerator(data.ItemType.PlantFiber, New ForageRangeGenerator)
    End Sub
End Class
