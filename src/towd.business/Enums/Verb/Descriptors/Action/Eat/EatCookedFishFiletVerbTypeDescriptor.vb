Imports towd.data

Friend Class EatCookedFishFiletVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatCookedFishFilet, VerbCategoryType.Eat, 0)
        SetDisplayName("Eat Cooked Fish Filet")
        SetItemTypeInput(ItemType.CookedFishFilet, 1)
        SetCharacterStatisticDelta(StatisticType.Satiety, ItemType.CookedFishFilet.ToItemTypeDescriptor.Statistics(StatisticType.Satiety))
    End Sub
End Class
