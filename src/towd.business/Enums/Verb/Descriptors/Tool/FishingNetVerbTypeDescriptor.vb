Imports towd.data

Friend Class FishingNetVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Public Sub New()
        MyBase.New(VerbType.FishingNet, 1)
        SetItemTypeInput(ItemType.Twine, 4)
        SetItemTypeOutputGenerator(ItemType.FishingNet, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
