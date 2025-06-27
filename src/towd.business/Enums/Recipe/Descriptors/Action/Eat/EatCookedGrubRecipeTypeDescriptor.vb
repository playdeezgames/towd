Imports towd.data

Friend Class EatCookedGrubRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatCookedGrub, 0)
        SetDisplayName("Eat Cooked Grub")
        SetItemTypeInput(ItemType.CookedGrub, 1)
        SetCharacterStatisticDelta(StatisticType.Satiety, ItemType.CookedGrub.ToDescriptor.Statistics(StatisticType.Satiety))
    End Sub
End Class
