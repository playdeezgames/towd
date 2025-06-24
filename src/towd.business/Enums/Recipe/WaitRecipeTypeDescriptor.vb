Friend Class WaitRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Wait, 1)
        SetDisplayName("Wait")
    End Sub
End Class
