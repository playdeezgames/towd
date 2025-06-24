Imports towd.data

Friend Class HatchetRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Hatchet, 1)
        SetItemTypeInput(ItemType.SharpRock, 1)
        SetItemTypeInput(ItemType.Twine, 1)
        SetItemTypeInput(ItemType.Stick, 1)
        SetItemTypeOutputGenerator(ItemType.Hatchet, New FixedGenerator(1))
    End Sub
End Class
