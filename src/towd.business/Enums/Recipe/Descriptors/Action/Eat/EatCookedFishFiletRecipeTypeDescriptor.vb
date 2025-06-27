Imports towd.data

Friend Class EatCookedFishFiletRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatCookedFishFilet, 0)
        SetDisplayName("Eat Cooked Fish Filet")
        SetItemTypeInput(ItemType.CookedFishFilet, 1)
        SetCharacterStatisticDelta(StatisticType.Satiety, ItemType.CookedFishFilet.ToDescriptor.Statistics(StatisticType.Satiety))
    End Sub
End Class
