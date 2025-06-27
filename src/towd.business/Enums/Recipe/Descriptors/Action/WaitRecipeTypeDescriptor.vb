Friend Class WaitRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Wait, 1)
        SetDisplayName("Wait")
    End Sub
End Class
