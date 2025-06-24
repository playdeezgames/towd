Imports towd.data

Friend Class HammerRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Hammer, 1)
        SetItemTypeInput(ItemType.Rock, 1)
        SetItemTypeInput(ItemType.Twine, 1)
        SetItemTypeInput(ItemType.Stick, 1)
        SetItemTypeOutputGenerator(ItemType.Hammer, New FixedGenerator(1))
    End Sub
End Class
