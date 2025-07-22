Imports towd.data

Friend Class KnifeVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.Knife, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.Blade, 1)
        SetItemTypeInput(ItemType.Twine, 1)
        SetItemTypeInput(ItemType.Stick, 1)
        SetItemTypeOutputGenerator(ItemType.Knife, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
        SetFlavorText("You call that a noif? That's a noif!")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
