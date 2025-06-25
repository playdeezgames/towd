Imports towd.data

Friend Class SharpRockRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.SharpRock, 1)
        SetItemTypeInput(data.ItemType.Rock, 1)
        SetItemTypeInput(data.ItemType.Hammer, 1)
        SetItemTypeOutputGenerator(data.ItemType.Hammer, New FixedGenerator(1))
        SetItemTypeOutputGenerator(data.ItemType.SharpRock, New FixedGenerator(1))
        SetItemTypeInputDurability(data.ItemType.Hammer, 1)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
