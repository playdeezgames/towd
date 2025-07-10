Friend Class EatCarrotVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatCarrot, VerbCategoryType.Eat, ItemType.Carrot)
    End Sub
End Class
