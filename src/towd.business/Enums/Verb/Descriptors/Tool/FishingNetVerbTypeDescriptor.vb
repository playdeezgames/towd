Imports towd.data

Friend Class FishingNetVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Public Sub New()
        MyBase.New(business.VerbType.FishingNet, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.Twine, 4)
        SetItemTypeOutputGenerator(ItemType.FishingNet, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
        SetFlavorText("Not to be worn as stockings, you perv.")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
