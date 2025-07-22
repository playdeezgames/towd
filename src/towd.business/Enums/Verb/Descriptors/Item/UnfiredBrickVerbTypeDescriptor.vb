Imports towd.data

Friend Class UnfiredBrickVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Public Sub New()
        MyBase.New(business.VerbType.UnfiredBrick, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.PlantFiber, 1)
        SetItemTypeInput(ItemType.Clay, 1)
        SetItemTypeOutputGenerator(ItemType.UnfiredBrick, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
        SetFlavorText("You can make clay into bricks, not just little snakes or poos.")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
