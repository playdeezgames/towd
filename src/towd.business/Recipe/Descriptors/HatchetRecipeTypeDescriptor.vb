Imports towd.data

Friend Class HatchetRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Hatchet, 1)
        SetInput(ItemType.SharpRock, 1)
        SetInput(ItemType.Twine, 1)
        SetInput(ItemType.Stick, 1)
        SetOutput(ItemType.Hatchet, 1)
    End Sub
End Class
