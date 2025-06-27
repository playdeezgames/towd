Imports towd.data

Friend Class TwineRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Twine, 1)
        SetItemTypeInput(data.ItemType.PlantFiber, 2)
        SetItemTypeOutputGenerator(data.ItemType.Twine, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
