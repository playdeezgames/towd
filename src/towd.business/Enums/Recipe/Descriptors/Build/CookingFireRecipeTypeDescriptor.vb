Imports towd.data

Friend Class CookingFireRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CookingFire, 1)
        SetItemTypeInput(ItemType.Rock, 8)
        SetItemTypeInput(ItemType.Stick, 8)
        SetRequiredLocationType(LocationType.Dirt)
        SetRequiredLocationType(LocationType.Grass)
        SetBuildsLocationType(LocationType.CookingFire)
        SetCharacterStatisticDelta(StatisticType.BuildCounter, 1)
    End Sub
End Class
