Imports towd.data

Friend Class TwineVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Twine, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(business.ItemType.PlantFiber, 2)
        SetItemTypeOutputGenerator(business.ItemType.Twine, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
