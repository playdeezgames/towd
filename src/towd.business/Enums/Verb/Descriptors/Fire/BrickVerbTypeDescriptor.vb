Imports towd.data

Friend Class BrickVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.Brick, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(ItemType.UnfiredBrick, 1)
        SetItemTypeOutputGenerator(ItemType.Brick, New FixedCharacterWeightedGenerator(1))
        SetRequiredLocationType(LocationType.CookingFire)
        SetRequiredLocationType(LocationType.Furnace)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
        SetFlavorText("Cooking bricks. Almost as much fun as watch paint dry.")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
