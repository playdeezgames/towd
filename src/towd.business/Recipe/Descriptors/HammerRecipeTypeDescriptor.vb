Imports towd.data

Friend Class HammerRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Hammer)
        SetInput(ItemType.Rock, 1)
        SetInput(ItemType.Twine, 1)
        SetInput(ItemType.Stick, 1)
        SetOutput(ItemType.Hammer, 1)
    End Sub
End Class
