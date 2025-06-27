Imports towd.data

Friend Class UnfiredBrickRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor
    Public Sub New()
        MyBase.New(VerbType.UnfiredBrick, 1)
        SetItemTypeInput(ItemType.PlantFiber, 1)
        SetItemTypeInput(ItemType.Clay, 1)
        SetItemTypeOutputGenerator(ItemType.UnfiredBrick, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
