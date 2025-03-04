Friend Class TwineRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Twine, 1)
        SetInput(data.ItemType.PlantFiber, 2)
        SetOutput(data.ItemType.Twine, 1)
    End Sub
End Class
