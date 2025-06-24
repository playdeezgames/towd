Friend Class TwineRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Twine, 1)
        SetItemTypeInput(data.ItemType.PlantFiber, 2)
        SetItemTypeOutputGenerator(data.ItemType.Twine, New FixedGenerator(1))
    End Sub
End Class
